namespace LiteDB.Studio
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tvwDatabase = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.splitRight = new System.Windows.Forms.SplitContainer();
            this.txtSql = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.tabText = new System.Windows.Forms.TabPage();
            this.txtResult = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabParameters = new System.Windows.Forms.TabPage();
            this.txtParameters = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabSql = new System.Windows.Forms.TabControl();
            this.stbStatus = new System.Windows.Forms.StatusStrip();
            this.lblCursor = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblResultCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgRunning = new System.Windows.Forms.ToolStripProgressBar();
            this.lblElapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.tlbSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBegin = new System.Windows.Forms.ToolStripButton();
            this.btnCommit = new System.Windows.Forms.ToolStripButton();
            this.btnRollback = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckpoint = new System.Windows.Forms.ToolStripButton();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuQueryAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQueryCount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExplanPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuIndexes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDropCollection = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRebuild = new System.Windows.Forms.ToolStripMenuItem();
            this.imgCodeCompletion = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.tabText.SuspendLayout();
            this.tabParameters.SuspendLayout();
            this.stbStatus.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.ctxMenuRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            resources.ApplyResources(this.splitMain, "splitMain");
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tvwDatabase);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitRight);
            this.splitMain.Panel2.Controls.Add(this.tabSql);
            this.splitMain.TabStop = false;
            // 
            // tvwDatabase
            // 
            resources.ApplyResources(this.tvwDatabase, "tvwDatabase");
            this.tvwDatabase.ImageList = this.imgList;
            this.tvwDatabase.Name = "tvwDatabase";
            this.tvwDatabase.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvwCols_NodeMouseDoubleClick);
            this.tvwDatabase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TvwCols_MouseUp);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "database");
            this.imgList.Images.SetKeyName(1, "folder");
            this.imgList.Images.SetKeyName(2, "table");
            this.imgList.Images.SetKeyName(3, "table_gear");
            // 
            // splitRight
            // 
            resources.ApplyResources(this.splitRight, "splitRight");
            this.splitRight.Name = "splitRight";
            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitRight.Panel1.Controls.Add(this.txtSql);
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.tabResult);
            // 
            // txtSql
            // 
            resources.ApplyResources(this.txtSql, "txtSql");
            this.txtSql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSql.ConvertTabsToSpaces = true;
            this.txtSql.Highlighting = "SQL";
            this.txtSql.Name = "txtSql";
            this.txtSql.ShowLineNumbers = false;
            this.txtSql.ShowVRuler = false;
            // 
            // tabResult
            // 
            resources.ApplyResources(this.tabResult, "tabResult");
            this.tabResult.Controls.Add(this.tabGrid);
            this.tabResult.Controls.Add(this.tabText);
            this.tabResult.Controls.Add(this.tabParameters);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.TabStop = false;
            this.tabResult.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabResult_Selected);
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.grdResult);
            resources.ApplyResources(this.tabGrid, "tabGrid");
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.grdResult, "grdResult");
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Name = "grdResult";
            this.grdResult.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GrdResult_CellBeginEdit);
            this.grdResult.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdResult_CellEndEdit);
            this.grdResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GrdResult_RowPostPaint);
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.txtResult);
            resources.ApplyResources(this.tabText, "tabText");
            this.tabText.Name = "tabText";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            resources.ApplyResources(this.txtResult, "txtResult");
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.Highlighting = "JSON";
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ShowLineNumbers = false;
            this.txtResult.ShowVRuler = false;
            // 
            // tabParameters
            // 
            this.tabParameters.Controls.Add(this.txtParameters);
            resources.ApplyResources(this.tabParameters, "tabParameters");
            this.tabParameters.Name = "tabParameters";
            this.tabParameters.UseVisualStyleBackColor = true;
            // 
            // txtParameters
            // 
            resources.ApplyResources(this.txtParameters, "txtParameters");
            this.txtParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParameters.Highlighting = "JSON";
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.ReadOnly = true;
            this.txtParameters.ShowLineNumbers = false;
            this.txtParameters.ShowVRuler = false;
            // 
            // tabSql
            // 
            resources.ApplyResources(this.tabSql, "tabSql");
            this.tabSql.Name = "tabSql";
            this.tabSql.SelectedIndex = 0;
            this.tabSql.TabStop = false;
            this.tabSql.SelectedIndexChanged += new System.EventHandler(this.TabSql_SelectedIndexChanged);
            this.tabSql.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabSql_Selected);
            this.tabSql.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabSql_MouseClick);
            // 
            // stbStatus
            // 
            this.stbStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCursor,
            this.lblResultCount,
            this.prgRunning,
            this.lblElapsed});
            resources.ApplyResources(this.stbStatus, "stbStatus");
            this.stbStatus.Name = "stbStatus";
            // 
            // lblCursor
            // 
            this.lblCursor.Name = "lblCursor";
            resources.ApplyResources(this.lblCursor, "lblCursor");
            this.lblCursor.Spring = true;
            // 
            // lblResultCount
            // 
            resources.ApplyResources(this.lblResultCount, "lblResultCount");
            this.lblResultCount.Name = "lblResultCount";
            // 
            // prgRunning
            // 
            this.prgRunning.Name = "prgRunning";
            resources.ApplyResources(this.prgRunning, "prgRunning");
            // 
            // lblElapsed
            // 
            resources.ApplyResources(this.lblElapsed, "lblElapsed");
            this.lblElapsed.Name = "lblElapsed";
            // 
            // tlbMain
            // 
            this.tlbMain.GripMargin = new System.Windows.Forms.Padding(3);
            this.tlbMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.tlbSep1,
            this.btnRefresh,
            this.tlbSep2,
            this.btnRun,
            this.toolStripSeparator1,
            this.btnBegin,
            this.btnCommit,
            this.btnRollback,
            this.toolStripSeparator2,
            this.btnCheckpoint});
            resources.ApplyResources(this.tlbMain, "tlbMain");
            this.tlbMain.Name = "tlbMain";
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::LiteDB.Studio.Properties.Resources.database_connect;
            resources.ApplyResources(this.btnConnect, "btnConnect");
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Padding = new System.Windows.Forms.Padding(3);
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // tlbSep1
            // 
            this.tlbSep1.Name = "tlbSep1";
            resources.ApplyResources(this.tlbSep1, "tlbSep1");
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::LiteDB.Studio.Properties.Resources.arrow_refresh;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(3);
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // tlbSep2
            // 
            this.tlbSep2.Name = "tlbSep2";
            resources.ApplyResources(this.tlbSep2, "tlbSep2");
            // 
            // btnRun
            // 
            this.btnRun.Image = global::LiteDB.Studio.Properties.Resources.resultset_next;
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.btnRun.Padding = new System.Windows.Forms.Padding(3);
            this.btnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnBegin
            // 
            this.btnBegin.Image = global::LiteDB.Studio.Properties.Resources.database;
            resources.ApplyResources(this.btnBegin, "btnBegin");
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Click += new System.EventHandler(this.BtnBegin_Click);
            // 
            // btnCommit
            // 
            this.btnCommit.Image = global::LiteDB.Studio.Properties.Resources.database_save;
            resources.ApplyResources(this.btnCommit, "btnCommit");
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Click += new System.EventHandler(this.BtnCommit_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.Image = global::LiteDB.Studio.Properties.Resources.database_delete;
            resources.ApplyResources(this.btnRollback, "btnRollback");
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Click += new System.EventHandler(this.BtnRollback_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnCheckpoint
            // 
            this.btnCheckpoint.Image = global::LiteDB.Studio.Properties.Resources.application_put;
            resources.ApplyResources(this.btnCheckpoint, "btnCheckpoint");
            this.btnCheckpoint.Name = "btnCheckpoint";
            this.btnCheckpoint.Click += new System.EventHandler(this.BtnCheckpoint_Click);
            // 
            // ctxMenu
            // 
            this.ctxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQueryAll,
            this.mnuQueryCount,
            this.mnuExplanPlan,
            this.mnuSep1,
            this.mnuIndexes,
            this.mnuSep2,
            this.mnuExport,
            this.mnuAnalyze,
            this.mnuRename,
            this.mnuDropCollection});
            this.ctxMenu.Name = "ctxMenu";
            resources.ApplyResources(this.ctxMenu, "ctxMenu");
            this.ctxMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CtxMenu_ItemClicked);
            // 
            // mnuQueryAll
            // 
            this.mnuQueryAll.Image = global::LiteDB.Studio.Properties.Resources.table_lightning;
            this.mnuQueryAll.Name = "mnuQueryAll";
            resources.ApplyResources(this.mnuQueryAll, "mnuQueryAll");
            this.mnuQueryAll.Tag = "SELECT $ FROM {0};";
            // 
            // mnuQueryCount
            // 
            this.mnuQueryCount.Image = global::LiteDB.Studio.Properties.Resources.table;
            this.mnuQueryCount.Name = "mnuQueryCount";
            resources.ApplyResources(this.mnuQueryCount, "mnuQueryCount");
            this.mnuQueryCount.Tag = "SELECT COUNT(*) FROM {0};";
            // 
            // mnuExplanPlan
            // 
            this.mnuExplanPlan.Image = global::LiteDB.Studio.Properties.Resources.table_sort;
            this.mnuExplanPlan.Name = "mnuExplanPlan";
            resources.ApplyResources(this.mnuExplanPlan, "mnuExplanPlan");
            this.mnuExplanPlan.Tag = "EXPLAIN SELECT $ FROM {0};";
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            resources.ApplyResources(this.mnuSep1, "mnuSep1");
            // 
            // mnuIndexes
            // 
            this.mnuIndexes.Image = global::LiteDB.Studio.Properties.Resources.key;
            this.mnuIndexes.Name = "mnuIndexes";
            resources.ApplyResources(this.mnuIndexes, "mnuIndexes");
            this.mnuIndexes.Tag = "SELECT $ FROM $indexes WHERE collection = \"{0}\";";
            // 
            // mnuSep2
            // 
            this.mnuSep2.Name = "mnuSep2";
            resources.ApplyResources(this.mnuSep2, "mnuSep2");
            // 
            // mnuExport
            // 
            this.mnuExport.Image = global::LiteDB.Studio.Properties.Resources.table_save;
            this.mnuExport.Name = "mnuExport";
            resources.ApplyResources(this.mnuExport, "mnuExport");
            this.mnuExport.Tag = "SELECT $\\n  INTO $file_json(\'C:/temp/{0}.json\')\\n  FROM {0};";
            // 
            // mnuAnalyze
            // 
            this.mnuAnalyze.Image = global::LiteDB.Studio.Properties.Resources.page_white_gear;
            this.mnuAnalyze.Name = "mnuAnalyze";
            resources.ApplyResources(this.mnuAnalyze, "mnuAnalyze");
            this.mnuAnalyze.Tag = "ANALYZE {0};";
            // 
            // mnuRename
            // 
            this.mnuRename.Image = global::LiteDB.Studio.Properties.Resources.textfield_rename;
            this.mnuRename.Name = "mnuRename";
            resources.ApplyResources(this.mnuRename, "mnuRename");
            this.mnuRename.Tag = "RENAME COLLECTION {0} TO new_name;";
            // 
            // mnuDropCollection
            // 
            this.mnuDropCollection.Image = global::LiteDB.Studio.Properties.Resources.table_delete;
            this.mnuDropCollection.Name = "mnuDropCollection";
            resources.ApplyResources(this.mnuDropCollection, "mnuDropCollection");
            this.mnuDropCollection.Tag = "DROP COLLECTION {0};";
            // 
            // ctxMenuRoot
            // 
            this.ctxMenuRoot.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenuRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInfo,
            this.toolStripSeparator3,
            this.mnuImport,
            this.mnuRebuild});
            this.ctxMenuRoot.Name = "ctxMenu";
            resources.ApplyResources(this.ctxMenuRoot, "ctxMenuRoot");
            this.ctxMenuRoot.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CtxMenuRoot_ItemClicked);
            // 
            // mnuInfo
            // 
            this.mnuInfo.Image = global::LiteDB.Studio.Properties.Resources.information;
            this.mnuInfo.Name = "mnuInfo";
            resources.ApplyResources(this.mnuInfo, "mnuInfo");
            this.mnuInfo.Tag = "SELECT $ FROM $database;";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // mnuImport
            // 
            this.mnuImport.Image = global::LiteDB.Studio.Properties.Resources.layout_add;
            this.mnuImport.Name = "mnuImport";
            resources.ApplyResources(this.mnuImport, "mnuImport");
            this.mnuImport.Tag = "SELECT $\\n  INTO new_col\\n  FROM $file_json(\'C:/temp/file.json\');";
            // 
            // mnuRebuild
            // 
            this.mnuRebuild.Image = global::LiteDB.Studio.Properties.Resources.compress;
            this.mnuRebuild.Name = "mnuRebuild";
            resources.ApplyResources(this.mnuRebuild, "mnuRebuild");
            this.mnuRebuild.Tag = "REBUILD { collation: \'en-US/IgnoreCase\',  password: \'newpassword\' };";
            // 
            // imgCodeCompletion
            // 
            this.imgCodeCompletion.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgCodeCompletion.ImageStream")));
            this.imgCodeCompletion.TransparentColor = System.Drawing.Color.Transparent;
            this.imgCodeCompletion.Images.SetKeyName(0, "METHOD");
            this.imgCodeCompletion.Images.SetKeyName(1, "COLLECTION");
            this.imgCodeCompletion.Images.SetKeyName(2, "FIELD");
            this.imgCodeCompletion.Images.SetKeyName(3, "KEYWORD");
            this.imgCodeCompletion.Images.SetKeyName(4, "SYSTEM");
            this.imgCodeCompletion.Images.SetKeyName(5, "SYSTEM_FN");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.splitMain);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.tabText.ResumeLayout(false);
            this.tabParameters.ResumeLayout(false);
            this.stbStatus.ResumeLayout(false);
            this.stbStatus.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ctxMenuRoot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView tvwDatabase;
        private System.Windows.Forms.StatusStrip stbStatus;
        private System.Windows.Forms.SplitContainer splitRight;
        private System.Windows.Forms.ToolStripStatusLabel lblElapsed;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.ToolStripStatusLabel lblResultCount;
        private System.Windows.Forms.ToolStrip tlbMain;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripSeparator tlbSep1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripSeparator tlbSep2;
        private System.Windows.Forms.ToolStripStatusLabel lblCursor;
        private System.Windows.Forms.ToolStripProgressBar prgRunning;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuIndexes;
        private ICSharpCode.TextEditor.TextEditorControl txtSql;
        private System.Windows.Forms.TabControl tabSql;
        private System.Windows.Forms.ToolStripMenuItem mnuDropCollection;
        private System.Windows.Forms.ToolStripMenuItem mnuAnalyze;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnBegin;
        private System.Windows.Forms.ToolStripButton btnCommit;
        private System.Windows.Forms.ToolStripButton btnRollback;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryAll;
        private System.Windows.Forms.ToolStripSeparator mnuSep2;
        private System.Windows.Forms.TabPage tabParameters;
        private System.Windows.Forms.ToolStripMenuItem mnuExplanPlan;
        private System.Windows.Forms.ContextMenuStrip ctxMenuRoot;
        private System.Windows.Forms.ToolStripMenuItem mnuInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryCount;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private ICSharpCode.TextEditor.TextEditorControl txtResult;
        private ICSharpCode.TextEditor.TextEditorControl txtParameters;
        private System.Windows.Forms.ImageList imgCodeCompletion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCheckpoint;
        private System.Windows.Forms.ToolStripMenuItem mnuRebuild;
    }
}

