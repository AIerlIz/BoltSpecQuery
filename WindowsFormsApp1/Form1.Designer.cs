namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._tplBox = new System.Windows.Forms.TextBox();
            this._quickFlow = new System.Windows.Forms.FlowLayoutPanel();
            this._preview = new System.Windows.Forms.Label();
            this._left = new System.Windows.Forms.Panel();
            this._grpNps = new System.Windows.Forms.GroupBox();
            this._npsList = new System.Windows.Forms.ListBox();
            this._grpFlange = new System.Windows.Forms.GroupBox();
            this._ffCb = new System.Windows.Forms.CheckBox();
            this._grpFace = new System.Windows.Forms.GroupBox();
            this._faceList = new System.Windows.Forms.ListBox();
            this._grpCls = new System.Windows.Forms.GroupBox();
            this._clsList = new System.Windows.Forms.ListBox();
            this._grpCat = new System.Windows.Forms.GroupBox();
            this._catCombo = new System.Windows.Forms.ComboBox();
            this._right = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._grid = new System.Windows.Forms.DataGridView();
            this._grpNotes = new System.Windows.Forms.GroupBox();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._status = new System.Windows.Forms.ToolStripStatusLabel();
            this._left.SuspendLayout();
            this._grpNps.SuspendLayout();
            this._grpFlange.SuspendLayout();
            this._grpFace.SuspendLayout();
            this._grpCls.SuspendLayout();
            this._grpCat.SuspendLayout();
            this._right.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._grpNotes.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tplBox
            // 
            this._tplBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._tplBox.Location = new System.Drawing.Point(7, 121);
            this._tplBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this._tplBox.Name = "_tplBox";
            this._tplBox.Size = new System.Drawing.Size(837, 25);
            this._tplBox.TabIndex = 0;
            this._tplBox.TextChanged += new System.EventHandler(this._tplBox_TextChanged);
            // 
            // _quickFlow
            // 
            this._quickFlow.AutoScroll = true;
            this._quickFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._quickFlow.Location = new System.Drawing.Point(7, 22);
            this._quickFlow.Name = "_quickFlow";
            this._quickFlow.Size = new System.Drawing.Size(837, 99);
            this._quickFlow.TabIndex = 1;
            // 
            // _preview
            // 
            this._preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._preview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._preview.Location = new System.Drawing.Point(7, 146);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(837, 30);
            this._preview.TabIndex = 2;
            this._preview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _left
            // 
            this._left.AutoScroll = true;
            this._left.Controls.Add(this._grpNps);
            this._left.Controls.Add(this._grpFlange);
            this._left.Controls.Add(this._grpFace);
            this._left.Controls.Add(this._grpCls);
            this._left.Controls.Add(this._grpCat);
            this._left.Dock = System.Windows.Forms.DockStyle.Left;
            this._left.Location = new System.Drawing.Point(0, 0);
            this._left.Name = "_left";
            this._left.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this._left.Size = new System.Drawing.Size(180, 668);
            this._left.TabIndex = 1;
            // 
            // _grpNps
            // 
            this._grpNps.Controls.Add(this._npsList);
            this._grpNps.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grpNps.Location = new System.Drawing.Point(7, 339);
            this._grpNps.Name = "_grpNps";
            this._grpNps.Padding = new System.Windows.Forms.Padding(7, 4, 7, 5);
            this._grpNps.Size = new System.Drawing.Size(166, 324);
            this._grpNps.TabIndex = 0;
            this._grpNps.TabStop = false;
            this._grpNps.Text = "NPS";
            // 
            // _npsList
            // 
            this._npsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._npsList.IntegralHeight = false;
            this._npsList.ItemHeight = 15;
            this._npsList.Location = new System.Drawing.Point(7, 22);
            this._npsList.Name = "_npsList";
            this._npsList.Size = new System.Drawing.Size(152, 297);
            this._npsList.TabIndex = 0;
            this._npsList.SelectedIndexChanged += new System.EventHandler(this._npsList_SelectedIndexChanged);
            // 
            // _grpFlange
            // 
            this._grpFlange.Controls.Add(this._ffCb);
            this._grpFlange.Dock = System.Windows.Forms.DockStyle.Top;
            this._grpFlange.Location = new System.Drawing.Point(7, 295);
            this._grpFlange.Name = "_grpFlange";
            this._grpFlange.Padding = new System.Windows.Forms.Padding(7, 4, 7, 5);
            this._grpFlange.Size = new System.Drawing.Size(166, 44);
            this._grpFlange.TabIndex = 4;
            this._grpFlange.TabStop = false;
            this._grpFlange.Text = "法兰";
            // 
            // _ffCb
            // 
            this._ffCb.AutoSize = true;
            this._ffCb.Location = new System.Drawing.Point(10, 22);
            this._ffCb.Name = "_ffCb";
            this._ffCb.Size = new System.Drawing.Size(83, 19);
            this._ffCb.TabIndex = 0;
            this._ffCb.Text = "FF 法兰";
            this._ffCb.CheckedChanged += new System.EventHandler(this._ffCb_CheckedChanged);
            // 
            // _grpFace
            // 
            this._grpFace.Controls.Add(this._faceList);
            this._grpFace.Dock = System.Windows.Forms.DockStyle.Top;
            this._grpFace.Location = new System.Drawing.Point(7, 217);
            this._grpFace.Name = "_grpFace";
            this._grpFace.Padding = new System.Windows.Forms.Padding(7, 4, 7, 5);
            this._grpFace.Size = new System.Drawing.Size(166, 78);
            this._grpFace.TabIndex = 1;
            this._grpFace.TabStop = false;
            this._grpFace.Text = "端面";
            // 
            // _faceList
            // 
            this._faceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._faceList.IntegralHeight = false;
            this._faceList.ItemHeight = 15;
            this._faceList.Location = new System.Drawing.Point(7, 22);
            this._faceList.Name = "_faceList";
            this._faceList.Size = new System.Drawing.Size(152, 51);
            this._faceList.TabIndex = 0;
            this._faceList.SelectedIndexChanged += new System.EventHandler(this._faceList_SelectedIndexChanged);
            // 
            // _grpCls
            // 
            this._grpCls.Controls.Add(this._clsList);
            this._grpCls.Dock = System.Windows.Forms.DockStyle.Top;
            this._grpCls.Location = new System.Drawing.Point(7, 69);
            this._grpCls.Name = "_grpCls";
            this._grpCls.Padding = new System.Windows.Forms.Padding(7, 4, 7, 5);
            this._grpCls.Size = new System.Drawing.Size(166, 148);
            this._grpCls.TabIndex = 2;
            this._grpCls.TabStop = false;
            this._grpCls.Text = "磅级";
            // 
            // _clsList
            // 
            this._clsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clsList.IntegralHeight = false;
            this._clsList.ItemHeight = 15;
            this._clsList.Location = new System.Drawing.Point(7, 22);
            this._clsList.Name = "_clsList";
            this._clsList.Size = new System.Drawing.Size(152, 121);
            this._clsList.TabIndex = 0;
            this._clsList.SelectedIndexChanged += new System.EventHandler(this._clsList_SelectedIndexChanged);
            // 
            // _grpCat
            // 
            this._grpCat.Controls.Add(this._catCombo);
            this._grpCat.Dock = System.Windows.Forms.DockStyle.Top;
            this._grpCat.Location = new System.Drawing.Point(7, 5);
            this._grpCat.Name = "_grpCat";
            this._grpCat.Padding = new System.Windows.Forms.Padding(7, 4, 7, 5);
            this._grpCat.Size = new System.Drawing.Size(166, 64);
            this._grpCat.TabIndex = 3;
            this._grpCat.TabStop = false;
            this._grpCat.Text = "规格";
            // 
            // _catCombo
            // 
            this._catCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._catCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._catCombo.Location = new System.Drawing.Point(7, 22);
            this._catCombo.Name = "_catCombo";
            this._catCombo.Size = new System.Drawing.Size(152, 23);
            this._catCombo.TabIndex = 0;
            this._catCombo.SelectedIndexChanged += new System.EventHandler(this._catCombo_SelectedIndexChanged);
            // 
            // _right
            // 
            this._right.Controls.Add(this.groupBox1);
            this._right.Controls.Add(this._grpNotes);
            this._right.Dock = System.Windows.Forms.DockStyle.Fill;
            this._right.Location = new System.Drawing.Point(180, 0);
            this._right.Name = "_right";
            this._right.Padding = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this._right.Size = new System.Drawing.Size(865, 668);
            this._right.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._grid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 479);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "规格表";
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(3, 21);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(845, 455);
            this._grid.TabIndex = 0;
            this._grid.SelectionChanged += new System.EventHandler(this._grid_SelectionChanged);
            // 
            // _grpNotes
            // 
            this._grpNotes.Controls.Add(this._quickFlow);
            this._grpNotes.Controls.Add(this._tplBox);
            this._grpNotes.Controls.Add(this._preview);
            this._grpNotes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._grpNotes.Location = new System.Drawing.Point(7, 483);
            this._grpNotes.Name = "_grpNotes";
            this._grpNotes.Padding = new System.Windows.Forms.Padding(7, 4, 7, 5);
            this._grpNotes.Size = new System.Drawing.Size(851, 181);
            this._grpNotes.TabIndex = 1;
            this._grpNotes.TabStop = false;
            this._grpNotes.Text = "快捷复制";
            // 
            // _statusStrip
            // 
            this._statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._status});
            this._statusStrip.Location = new System.Drawing.Point(0, 668);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this._statusStrip.Size = new System.Drawing.Size(1045, 26);
            this._statusStrip.TabIndex = 3;
            // 
            // _status
            // 
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(39, 20);
            this._status.Text = "就绪";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 694);
            this.Controls.Add(this._right);
            this.Controls.Add(this._left);
            this.Controls.Add(this._statusStrip);
            this.MinimumSize = new System.Drawing.Size(900, 620);
            this.Name = "Form1";
            this.Text = "螺栓规格查询工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this._left.ResumeLayout(false);
            this._grpNps.ResumeLayout(false);
            this._grpFlange.ResumeLayout(false);
            this._grpFlange.PerformLayout();
            this._grpFace.ResumeLayout(false);
            this._grpCls.ResumeLayout(false);
            this._grpCat.ResumeLayout(false);
            this._right.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._grpNotes.ResumeLayout(false);
            this._grpNotes.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tplBox;
        private System.Windows.Forms.FlowLayoutPanel _quickFlow;
        private System.Windows.Forms.Label _preview;
        private System.Windows.Forms.Panel _left;
        private System.Windows.Forms.GroupBox _grpFlange;
        private System.Windows.Forms.CheckBox _ffCb;
        private System.Windows.Forms.GroupBox _grpNps;
        private System.Windows.Forms.ListBox _npsList;
        private System.Windows.Forms.GroupBox _grpFace;
        private System.Windows.Forms.ListBox _faceList;
        private System.Windows.Forms.GroupBox _grpCls;
        private System.Windows.Forms.ListBox _clsList;
        private System.Windows.Forms.GroupBox _grpCat;
        private System.Windows.Forms.ComboBox _catCombo;
        private System.Windows.Forms.Panel _right;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.GroupBox _grpNotes;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _status;
    }
}
