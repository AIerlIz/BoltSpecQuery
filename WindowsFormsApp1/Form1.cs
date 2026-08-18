using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // 筛选状态
        private string _cat = BoltEngine.AllCat;      // 类别
        private string _cls = BoltEngine.AllCls;      // 磅级
        private string _face = "全部";                 // 端面
        private string _nps = BoltEngine.AllNps;      // NPS
        private bool _ff = false;                     // FF 法兰

        private string _template = BoltEngine.DefaultTemplate;
        private AppConfig _cfg;                          // 配置持久化

        private Dictionary<string, string> _npsRawMap = new Dictionary<string, string>();
        private List<HorizontalRow> _current = new List<HorizontalRow>();

        public Form1()
        {
            InitializeComponent();
            // 设计器只初始化控件，不执行运行时的数据加载/填充/IO。
            if (DesignMode)
                return;

            _cfg = new AppConfig(ConfigPath);
            _template = string.IsNullOrEmpty(_cfg.Template) ? BoltEngine.DefaultTemplate : _cfg.Template;
            _cat = _cfg.Category;
            _cls = _cfg.Cls;
            _face = _cfg.Face;
            _nps = _cfg.Nps;
            ApplyInitialState();
        }

        // ---------- 配置持久化（exe 旁 ui_config.ini） ----------
        private string ConfigPath
        {
            get
            {
                string dir;
                try { dir = Path.GetDirectoryName(Application.ExecutablePath); }
                catch (Exception) { dir = "."; }
                return Path.Combine(dir, "ui_config.ini");
            }
        }

        private void SaveConfig()
        {
            _cfg.Template = _template;
            _cfg.Category = _cat;
            _cfg.Cls = _cls;
            _cfg.Face = _face;
            _cfg.Nps = _nps;
            var widths = new List<string>();
            foreach (DataGridViewColumn c in _grid.Columns)
                widths.Add(c.Width.ToString(CultureInfo.InvariantCulture));
            _cfg.ColWidths = string.Join(",", widths.ToArray());
            _cfg.Save();
        }

        // ---------- 初始状态 ----------
        private void ApplyInitialState()
        {
            BuildQuickButtons();
            foreach (var c in BoltEngine.ClassOptions) _clsList.Items.Add(c);
            foreach (var f in BoltEngine.FaceOptions) _faceList.Items.Add(f);

            int ic = Array.IndexOf(BoltEngine.ClassOptions, _cls);
            _clsList.SelectedIndex = ic >= 0 ? ic : 0;
            RefreshCategoryOptions();
            RefreshNpsList();
            int ifc = Array.IndexOf(BoltEngine.FaceOptions, _face);
            _faceList.SelectedIndex = ifc >= 0 ? ifc : 0;
            _ffCb.Checked = _ff;
            _tplBox.Text = _template;
            Query();
            ApplySavedWidths();
        }

        private void ApplySavedWidths()
        {
            var parts = _cfg.ColWidths.Split(',');
            for (int i = 0; i < parts.Length && i < _grid.Columns.Count; i++)
            {
                int w;
                if (int.TryParse(parts[i], out w) && w > 30)
                    _grid.Columns[i].Width = w;
            }
        }

        private void RefreshCategoryOptions()
        {
            _catCombo.Items.Clear();
            _catCombo.Items.Add(BoltEngine.AllCat);
            foreach (var c in BoltEngine.CategoriesFor(_cls)) _catCombo.Items.Add(c);
            if (!_catCombo.Items.Contains(_cat)) _cat = BoltEngine.AllCat;
            _catCombo.SelectedItem = _cat;
        }

        private void RefreshNpsList()
        {
            _npsList.Items.Clear();
            _npsRawMap.Clear();
            _npsList.Items.Add(BoltEngine.AllNps);
            _npsRawMap[BoltEngine.AllNps] = BoltEngine.AllNps;
            foreach (var r in BoltEngine.DistinctNps(_cls))
            {
                string disp = BoltEngine.NpsDisplay(r);
                _npsRawMap[disp] = r.Nps;
                _npsList.Items.Add(disp);
            }
            if (_nps != BoltEngine.AllNps)
            {
                string found = null;
                foreach (var kv in _npsRawMap)
                    if (kv.Value == _nps) { found = kv.Key; break; }
                _npsList.SelectedItem = found;
            }
            if (_npsList.SelectedIndex < 0) { _npsList.SelectedIndex = 0; _nps = BoltEngine.AllNps; }
        }

        // ---------- 查询 ----------
        private void Query()
        {
            string cls = _cls == BoltEngine.AllCls ? null : _cls;
            string nps = _nps == BoltEngine.AllNps ? null : _nps;
            var rows = BoltEngine.Query(cls, nps);
            var flat = BoltEngine.Expand(rows, _cat);
            _current = flat;

            _grid.SuspendLayout();
            _grid.Columns.Clear();
            var plan = BoltEngine.ColumnPlan(_face);
            foreach (var kv in plan)
            {
                _grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = kv.Item1,
                    HeaderText = kv.Item2,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                });
            }
            _grid.Rows.Clear();
            foreach (var row in flat)
            {
                double? rf = row.Rf, rtj = row.Rtj;
                BoltEngine.ApplyFf(ref rf, ref rtj, row.Standard, _ff);
                var vals = new List<string>();
                foreach (var kv in plan)
                {
                    switch (kv.Item1)
                    {
                        case "standard": vals.Add(row.Standard); break;
                        case "cls": vals.Add(row.Cls); break;
                        case "nps": vals.Add(row.Nps); break;
                        case "no": vals.Add(row.No.ToString(CultureInfo.InvariantCulture)); break;
                        case "stud_diam": vals.Add(row.StudDiam); break;
                        case "rf": vals.Add(BoltEngine.FormatNum(rf)); break;
                        case "rtj": vals.Add(BoltEngine.FormatNum(rtj)); break;
                        case "groove": vals.Add(row.Groove ?? ""); break;
                        default: vals.Add(""); break;
                    }
                }
                _grid.Rows.Add(vals.ToArray());
            }
            _grid.ResumeLayout();

            string where = (cls != null ? "Class " + cls + " · " : "全部 Class · ")
                + (nps != null ? "NPS " + nps : "全部尺寸");
            string cat = _cat == BoltEngine.AllCat ? "全部规格" : _cat;
            _status.Text = string.Format("{0} · {1}：共 {2} 条结果", cat, where, flat.Count);
            UpdatePreview();
        }

        private void CopySelected()
        {
            if (_grid.SelectedRows.Count == 0 || _grid.SelectedRows[0].Index >= _current.Count)
                return;
            var row = _current[_grid.SelectedRows[0].Index];
            string text = BoltEngine.FormatResult(row, _face, _ff, _template);
            try { Clipboard.SetText(text); } catch (Exception) { return; }
            string preview = text.Length <= 70 ? text : text.Substring(0, 67) + "…";
            _status.Text = "已复制：" + preview;
        }

        // ---------- 事件 ----------
        private void _catCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cat = (string)_catCombo.SelectedItem ?? BoltEngine.AllCat;
            Query();
        }

        private void _clsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cls = _clsList.SelectedItem as string ?? BoltEngine.AllCls;
            RefreshCategoryOptions();
            RefreshNpsList();
            Query();
        }

        private void _faceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _face = _faceList.SelectedItem as string ?? "全部";
            Query();
        }

        private void _npsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var disp = _npsList.SelectedItem as string ?? "";
            _nps = _npsRawMap.ContainsKey(disp) ? _npsRawMap[disp] : disp;
            Query();
        }

        private void _ffCb_CheckedChanged(object sender, EventArgs e)
        {
            _ff = _ffCb.Checked;
            Query();
        }

        private void _grid_SelectionChanged(object sender, EventArgs e)
        {
            CopySelected();
            UpdatePreview();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        // ---------- 快捷复制 ----------
        private HorizontalRow CurrentSelected()
        {
            if (_grid.SelectedRows.Count == 0) return null;
            int idx = _grid.SelectedRows[0].Index;
            return (idx >= 0 && idx < _current.Count) ? _current[idx] : null;
        }

        /// <summary>从 BoltEngine.FieldButtons 批量生成快捷占位符按钮，加到 _quickFlow。</summary>
        private void BuildQuickButtons()
        {
            _quickFlow.Controls.Clear();
            foreach (var fb in BoltEngine.FieldButtons)
            {
                string token = fb.Token;
                var b = new Button
                {
                    Text = fb.Label,
                    AutoSize = true,
                    Margin = new Padding(4, 2, 0, 2),
                };
                b.Click += (s, e) => InsertToken(token);
                _quickFlow.Controls.Add(b);
            }
        }

        private void InsertToken(string token)
        {
            // 在模板文本框当前光标处插入占位符
            int pos = _tplBox.SelectionStart;
            if (pos < 0) pos = _tplBox.TextLength;
            _tplBox.Text = _tplBox.Text.Insert(pos, token);
            _tplBox.SelectionStart = pos + token.Length;
            _tplBox.Focus();
        }

        // ---------- 预览（模板结果，随选中/模板/端面/FF 变化） ----------
        private void UpdatePreview()
        {
            var r = CurrentSelected();
            if (r == null) { _preview.Text = "（未选择结果行）"; return; }
            _preview.Text = BoltEngine.FormatResult(r, _face, _ff, _template);
        }

        private void _tplBox_TextChanged(object sender, EventArgs e)
        {
            _template = _tplBox.Text;      // 主界面直接编辑，即时生效
            UpdatePreview();
        }

        private void _tplBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_tplBox.Text))
            {
                _template = BoltEngine.DefaultTemplate;
                _tplBox.Text = _template;
            }
        }
    }
}
