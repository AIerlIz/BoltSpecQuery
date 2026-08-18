using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public enum FaceFilter
    {
        All,
        RF,
        RTJ
    }

    /// <summary>横表一行（每行一条「类别+磅级+尺寸」记录）。</summary>
    public class HorizontalRow
    {
        public string Standard;
        public string Cls;        // 如 "Class 150"
        public string Nps;
        public int No;
        public string StudDiam;
        public double? Rf;        // RF/Valve 长度
        public double? Rtj;       // RTJ/Bolt 长度
        public string Groove;
    }

    /// <summary>螺栓查询核心纯逻辑（与 UI 解耦）。</summary>
    public static class BoltEngine
    {
        public const string AllCls = "全部";
        public const string AllNps = "全部";
        public const string AllCat = "全部";

        public static readonly string[] ClassOptions =
            { "全部", "150", "300", "600", "900", "1500", "2500" };
        public static readonly string[] FaceOptions = { "全部", "RF", "RTJ" };

        // 各压力等级可用类别（>=900 用 ANSI，其余用 ASME）——与 Python 版一致
        private static readonly string[] CatsLow =
            { "ASME", "COOEC", "Check Valve", "Butterfly Valve" };
        private static readonly string[] CatsHigh =
            { "ANSI", "COOEC", "Check Valve" };
        private static readonly string[] CatsAll =
            { "ANSI", "ASME", "COOEC", "Check Valve", "Butterfly Valve" };

        // NPS（英寸数值）-> DN（mm）
        public static readonly Dictionary<double, int> DnMap = new Dictionary<double, int>
        {
            {0.5,15},{0.75,20},{1.0,25},{1.25,32},{1.5,40},
            {2.0,50},{2.5,65},{3.0,80},{3.5,90},{4.0,100},
            {5.0,125},{6.0,150},{8.0,200},{10.0,250},{12.0,300},
            {14.0,350},{16.0,400},{18.0,450},{20.0,500},{22.0,550},
            {24.0,600},{26.0,650},{28.0,700},{30.0,750},{32.0,800},
            {34.0,850},{36.0,900},{38.0,950},{40.0,1000},{42.0,1050},
            {44.0,1100},{46.0,1150},{48.0,1200},
        };

        // 默认模板：初始为空，由用户通过快捷复制区的字段按钮拼装占位符；
        // 自定义模板会持久化到 ui_config.ini，下次启动恢复。
        public const string DefaultTemplate = "";

        /// <summary>某压力等级可选的类别（「全部」返回全部）。</summary>
        public static string[] CategoriesFor(string cls)
        {
            if (cls == AllCls || cls == null) return CatsAll;
            return int.Parse(cls) >= 900 ? CatsHigh : CatsLow;
        }

        /// <summary>查询：cls 为压力等级（null/全部 表示全部），nps 为法兰尺寸原文（null 表示全部）。</summary>
        public static List<BoltRow> Query(string cls = null, string nps = null)
        {
            IEnumerable<BoltRow> rows = BoltData.Rows;
            if (cls != null && cls != AllCls)
                rows = rows.Where(r => r.Cls == cls);
            if (!string.IsNullOrEmpty(nps) && nps != AllNps)
                rows = rows.Where(r => r.Nps == nps);
            // 按 cls(数字序、字符串序兼容)、NpsNum 排序，与旧版一致
            return rows
                .OrderBy(r => ClassRank(r.Cls))
                .ThenBy(r => r.NpsNum)
                .ToList();
        }

        private static int ClassRank(string cls)
        {
            int v = int.Parse(cls);
            return v; // 150..2500 数字序即期望序
        }

        /// <summary>某压力等级的去重尺寸（按 NpsNum 排序）。</summary>
        public static List<BoltRow> DistinctNps(string cls)
        {
            return Query(cls)
                .GroupBy(r => r.Nps)
                .Select(g => g.First())
                .OrderBy(r => r.NpsNum)
                .ToList();
        }

        public static string NpsDisplay(BoltRow r)
        {
            int dn;
            string d = DnMap.TryGetValue(r.NpsNum, out dn)
                ? "(" + dn + ")" : "";
            return r.Nps + "\"" + d;
        }

        /// <summary>把 BoltRow 列表展开为横表行（每行一个类别）。</summary>
        public static List<HorizontalRow> Expand(IEnumerable<BoltRow> rows, string category)
        {
            var list = new List<HorizontalRow>();
            foreach (var r in rows)
            {
                foreach (var cat in CategoriesFor(r.Cls))
                {
                    if (category != AllCat && category != cat) continue;
                    double? rf = null, rtj = null;
                    string groove = null;
                    switch (cat)
                    {
                        case "ASME":
                        case "ANSI":
                            rf = r.AsmeRf; rtj = r.AsmeRtj; groove = r.Groove; break;
                        case "COOEC":
                            rf = r.CooecRf; rtj = r.CooecRtj; break;
                        case "Check Valve":
                            rf = r.CvValveLen; rtj = r.CvBoltLen; break;
                        default: // Butterfly Valve
                            rf = r.BfvValveLen; rtj = r.BfvBoltLen; break;
                    }
                    if (rf == null && rtj == null) continue;
                    list.Add(new HorizontalRow
                    {
                        Standard = cat,
                        Cls = "Class " + r.Cls,
                        Nps = r.Nps,
                        No = r.No,
                        StudDiam = r.StudDiam,
                        Rf = rf,
                        Rtj = rtj,
                        Groove = groove,
                    });
                }
            }
            return list;
        }

        /// <summary>FF 法兰：ASME/ANSI/COOEC 的 RF 与 RTJ 都减 0.25；止回/蝶阀只螺栓长(Rtj)减。</summary>
        public static void ApplyFf(ref double? rf, ref double? rtj, string standard, bool ff)
        {
            if (!ff) return;
            if (standard == "ASME" || standard == "ANSI" || standard == "COOEC")
            {
                if (rf.HasValue) rf = rf.Value - 0.25;
                if (rtj.HasValue) rtj = rtj.Value - 0.25;
            }
            else
            {
                if (rtj.HasValue) rtj = rtj.Value - 0.25;
            }
        }

        public static string FormatNum(double? v)
        {
            if (!v.HasValue) return "";
            return v.Value.ToString("0.######", CultureInfo.InvariantCulture);
        }

        // 快捷复制区的字段占位符按钮：按钮文字 -> 插入模板的占位符
        public static readonly (string Label, string Token)[] FieldButtons = new[]
        {
            ("Type", "{standard}"),
            ("Class", "{cls}"),
            ("NPS", "{nps}"),
            ("No.", "{no}"),
            ("Diam", "{stud_diam}"),
            ("RF", "{rf}"),
            ("RTJ", "{rtj}"),
            ("Groove", "{groove}"),
            ("Len", "{len}"),
            ("Face", "{face}"),
            ("Tab", "\t"),
        };

        /// <summary>横表列方案：按端面筛选决定是否包含 RF/RTJ 列。返回 (key, header)。</summary>
        public static List<Tuple<string, string>> ColumnPlan(string face)
        {
            var cols = new List<Tuple<string, string>>
            {
                Tuple.Create("standard", "Type"),
                Tuple.Create("cls", "Class"),
                Tuple.Create("nps", "NPS"),
                Tuple.Create("no", "No."),
                Tuple.Create("stud_diam", "Diam"),
            };
            // 端面(RF/RTJ)筛选不隐藏列：RF 与 RTJ 两列始终显示（face 仅影响 {len} 复制判断/预览）
            cols.Add(Tuple.Create("rf", "RF"));
            cols.Add(Tuple.Create("rtj", "RTJ"));
            cols.Add(Tuple.Create("groove", "Groove"));
            return cols;
        }

        /// <summary>按模板把一行格式化为输出文本。{len} 按端面自动判断：RF->RF 长度，RTJ->RTJ 长度，全部->空。</summary>
        public static string FormatResult(HorizontalRow row, string face, bool ff, string template)
        {
            if (string.IsNullOrEmpty(template)) template = DefaultTemplate;
            double? rf = row.Rf, rtj = row.Rtj;
            ApplyFf(ref rf, ref rtj, row.Standard, ff);
            double? len = face == "RF" ? rf : (face == "RTJ" ? rtj : (double?)null);
            var map = new Dictionary<string, string>
            {
                { "standard", row.Standard ?? "" },
                { "cls", row.Cls ?? "" },
                { "nps", row.Nps ?? "" },
                { "no", row.No.ToString(CultureInfo.InvariantCulture) },
                { "stud_diam", row.StudDiam ?? "" },
                { "rf", FormatNum(rf) },
                { "rtj", FormatNum(rtj) },
                { "len", FormatNum(len) },
                { "face", (face == "全部") ? "" : face },
                { "groove", row.Groove ?? "" },
            };
            try
            {
                return Regex.Replace(template, @"\{([^{}]+)\}",
                    m => map.ContainsKey(m.Groups[1].Value) ? map[m.Groups[1].Value] : "");
            }
            catch (Exception)
            {
                return template;
            }
        }

        /// <summary>中文「长度计算原则（Notes）」逻辑（与 Python notes_zh.py 一致）。</summary>
        public static string NotesZh(string cls)
        {
            var items = new List<string>
            {
                "双头螺柱长度不含两端牙尖高度。",
                "双头螺柱长度不含垫片厚度。",
                "尺寸 26\"~48\" 采用 ASME B16.47 Series A（若法兰接头含盲板法兰，应单独计算）；尺寸 22\" 采用 MSS SP 44。",
            };
            if (cls == null || int.Parse(cls) <= 600)
                items.Add("蝶阀阀体尺寸按 API 609 Category B。");
            items.Add("螺柱长度应符合 ASME B16.5 或 B16.47（不含两端牙尖）：直径小于 1-1/4\" 的螺柱加长 0.25 in；直径 ≥ 1-1/4\" 的螺柱加长 1.5 倍螺栓直径，以便拧紧或拉伸；所有螺柱长度向上取整至最接近的 0.25 in。");
            items.Add("本表仅适用于符合 ASME B16.5 与 MSS-SP-44 的金属法兰。");
            if (cls == null || cls == "150")
                items.Add("FF（全平面）法兰的螺栓长度应减 0.25 in。");

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0) sb.AppendLine();
                sb.Append(i + 1).Append(". ").Append(items[i]);
            }
            if (cls == null) sb.Append("（仅 Class 150）");
            return sb.ToString();
        }
    }
}
