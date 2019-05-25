namespace FotoShrinker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbThreads = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chWriteTag = new System.Windows.Forms.CheckBox();
            this.trbThreads = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btRunAll = new System.Windows.Forms.Button();
            this.btSaveSettings = new System.Windows.Forms.Button();
            this.gridTAGs = new System.Windows.Forms.DataGridView();
            this.ColumnTAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.gridTasks = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusPanel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPanelModify = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPanelTaskComplited = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileTask = new System.Windows.Forms.OpenFileDialog();
            this.toolStripFields = new System.Windows.Forms.ToolStripMenuItem();
            this.ColumnEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnInFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOutFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaxWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQualety = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTagAll = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnTAGResized = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnExecute = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbThreads)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTAGs)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbThreads);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chWriteTag);
            this.panel1.Controls.Add(this.trbThreads);
            this.panel1.Location = new System.Drawing.Point(2, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 202);
            this.panel1.TabIndex = 10;
            // 
            // lbThreads
            // 
            this.lbThreads.AutoSize = true;
            this.lbThreads.Location = new System.Drawing.Point(61, 148);
            this.lbThreads.Name = "lbThreads";
            this.lbThreads.Size = new System.Drawing.Size(13, 13);
            this.lbThreads.TabIndex = 8;
            this.lbThreads.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Потоков";
            // 
            // chWriteTag
            // 
            this.chWriteTag.AutoSize = true;
            this.chWriteTag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chWriteTag.Checked = true;
            this.chWriteTag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chWriteTag.Location = new System.Drawing.Point(8, 176);
            this.chWriteTag.Name = "chWriteTag";
            this.chWriteTag.Size = new System.Drawing.Size(48, 17);
            this.chWriteTag.TabIndex = 6;
            this.chWriteTag.Text = "TAG";
            this.chWriteTag.UseVisualStyleBackColor = true;
            // 
            // trbThreads
            // 
            this.trbThreads.Location = new System.Drawing.Point(10, 3);
            this.trbThreads.Maximum = 8;
            this.trbThreads.Minimum = 1;
            this.trbThreads.Name = "trbThreads";
            this.trbThreads.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbThreads.Size = new System.Drawing.Size(45, 142);
            this.trbThreads.TabIndex = 5;
            this.trbThreads.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbThreads.Value = 1;
            this.trbThreads.Scroll += new System.EventHandler(this.trbThreads_Scroll);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.gridTAGs);
            this.panel3.Controls.Add(this.gridTasks);
            this.panel3.Controls.Add(this.menuStrip1);
            this.panel3.Location = new System.Drawing.Point(107, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(579, 275);
            this.panel3.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.btRunAll);
            this.panel4.Controls.Add(this.btSaveSettings);
            this.panel4.Location = new System.Drawing.Point(3, 241);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(572, 29);
            this.panel4.TabIndex = 14;
            // 
            // btRunAll
            // 
            this.btRunAll.Location = new System.Drawing.Point(152, 3);
            this.btRunAll.Name = "btRunAll";
            this.btRunAll.Size = new System.Drawing.Size(122, 23);
            this.btRunAll.TabIndex = 15;
            this.btRunAll.Text = "Запустить все";
            this.btRunAll.UseVisualStyleBackColor = true;
            this.btRunAll.Click += new System.EventHandler(this.btRunAll_Click);
            // 
            // btSaveSettings
            // 
            this.btSaveSettings.Location = new System.Drawing.Point(12, 3);
            this.btSaveSettings.Name = "btSaveSettings";
            this.btSaveSettings.Size = new System.Drawing.Size(122, 23);
            this.btSaveSettings.TabIndex = 14;
            this.btSaveSettings.Text = "Сохранить задания";
            this.btSaveSettings.UseVisualStyleBackColor = true;
            this.btSaveSettings.Click += new System.EventHandler(this.btSaveSettings_Click);
            // 
            // gridTAGs
            // 
            this.gridTAGs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTAGs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTAG});
            this.gridTAGs.ContextMenuStrip = this.contextMenuStrip2;
            this.gridTAGs.Location = new System.Drawing.Point(172, 57);
            this.gridTAGs.Name = "gridTAGs";
            this.gridTAGs.Size = new System.Drawing.Size(289, 145);
            this.gridTAGs.TabIndex = 10;
            this.gridTAGs.Visible = false;
            this.gridTAGs.VisibleChanged += new System.EventHandler(this.gridTAGs_VisibleChanged);
            this.gridTAGs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridTAGs_KeyUp);
            // 
            // ColumnTAG
            // 
            this.ColumnTAG.FillWeight = 220F;
            this.ColumnTAG.HeaderText = "TAG";
            this.ColumnTAG.Name = "ColumnTAG";
            this.ColumnTAG.Width = 220;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripFields});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(119, 48);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = "Удалить";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // gridTasks
            // 
            this.gridTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEnabled,
            this.ColumnInFolder,
            this.ColumnOutFolder,
            this.ColumnMaxWidth,
            this.ColumnQualety,
            this.ColumnTagAll,
            this.ColumnTAGResized,
            this.ColumnExecute});
            this.gridTasks.ContextMenuStrip = this.contextMenuStrip1;
            this.gridTasks.Location = new System.Drawing.Point(7, 27);
            this.gridTasks.Name = "gridTasks";
            this.gridTasks.Size = new System.Drawing.Size(568, 209);
            this.gridTasks.TabIndex = 9;
            this.gridTasks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTasks_CellDoubleClick);
            this.gridTasks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTasks_CellEndEdit);
            this.gridTasks.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridTasks_CellMouseClick);
            this.gridTasks.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridTasks_UserAddedRow);
            this.gridTasks.Click += new System.EventHandler(this.gridTasks_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 26);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItem2.Text = "Удалить";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(579, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(64, 20);
            this.toolStripMenuItem1.Text = "Задания";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.открытьToolStripMenuItem.Text = "Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusPanel1,
            this.statusPanelModify,
            this.statusPanelTaskComplited});
            this.statusStrip1.Location = new System.Drawing.Point(0, 280);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(686, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusPanel1
            // 
            this.statusPanel1.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.statusPanel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusPanel1.Name = "statusPanel1";
            this.statusPanel1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusPanelModify
            // 
            this.statusPanelModify.Name = "statusPanelModify";
            this.statusPanelModify.Size = new System.Drawing.Size(12, 17);
            this.statusPanelModify.Text = "*";
            // 
            // statusPanelTaskComplited
            // 
            this.statusPanelTaskComplited.Name = "statusPanelTaskComplited";
            this.statusPanelTaskComplited.Size = new System.Drawing.Size(13, 17);
            this.statusPanelTaskComplited.Text = "0";
            // 
            // openFileTask
            // 
            this.openFileTask.DefaultExt = "txt";
            this.openFileTask.FileName = "openFileDialog1";
            this.openFileTask.Filter = "FS Tasks|*.txt";
            this.openFileTask.RestoreDirectory = true;
            // 
            // toolStripFields
            // 
            this.toolStripFields.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolStripFields.Name = "toolStripFields";
            this.toolStripFields.Size = new System.Drawing.Size(180, 22);
            this.toolStripFields.Text = "Поля";
            // 
            // ColumnEnabled
            // 
            this.ColumnEnabled.FillWeight = 35F;
            this.ColumnEnabled.HeaderText = "Вкл.";
            this.ColumnEnabled.Name = "ColumnEnabled";
            this.ColumnEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnEnabled.Width = 35;
            // 
            // ColumnInFolder
            // 
            this.ColumnInFolder.HeaderText = "Источник";
            this.ColumnInFolder.Name = "ColumnInFolder";
            this.ColumnInFolder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnOutFolder
            // 
            this.ColumnOutFolder.HeaderText = "Назначение";
            this.ColumnOutFolder.Name = "ColumnOutFolder";
            this.ColumnOutFolder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnMaxWidth
            // 
            this.ColumnMaxWidth.HeaderText = "Размер по шир. стороне";
            this.ColumnMaxWidth.Name = "ColumnMaxWidth";
            this.ColumnMaxWidth.Width = 90;
            // 
            // ColumnQualety
            // 
            this.ColumnQualety.HeaderText = "Качество";
            this.ColumnQualety.Name = "ColumnQualety";
            this.ColumnQualety.Width = 40;
            // 
            // ColumnTagAll
            // 
            this.ColumnTagAll.HeaderText = "TAG для всех";
            this.ColumnTagAll.Name = "ColumnTagAll";
            this.ColumnTagAll.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnTagAll.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnTagAll.Width = 40;
            // 
            // ColumnTAGResized
            // 
            this.ColumnTAGResized.HeaderText = "TAG изм.";
            this.ColumnTAGResized.Name = "ColumnTAGResized";
            this.ColumnTAGResized.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnTAGResized.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnTAGResized.Width = 40;
            // 
            // ColumnExecute
            // 
            this.ColumnExecute.HeaderText = "Запустить";
            this.ColumnExecute.Name = "ColumnExecute";
            this.ColumnExecute.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnExecute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnExecute.Text = ">>";
            this.ColumnExecute.Width = 65;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 302);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foto Shrinker";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbThreads)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTAGs)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chWriteTag;
        private System.Windows.Forms.TrackBar trbThreads;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gridTAGs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTAG;
        private System.Windows.Forms.DataGridView gridTasks;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btRunAll;
        private System.Windows.Forms.Button btSaveSettings;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileTask;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripStatusLabel statusPanel1;
        private System.Windows.Forms.ToolStripStatusLabel statusPanelModify;
        private System.Windows.Forms.ToolStripStatusLabel statusPanelTaskComplited;
        private System.Windows.Forms.Label lbThreads;
        private System.Windows.Forms.ToolStripMenuItem toolStripFields;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOutFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaxWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQualety;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnTagAll;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnTAGResized;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnExecute;
    }
}

