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
            this.recentDBsDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.recentListSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateRecentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.tlbSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoadSql = new System.Windows.Forms.ToolStripButton();
            this.btnSaveSql = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBegin = new System.Windows.Forms.ToolStripButton();
            this.btnCommit = new System.Windows.Forms.ToolStripButton();
            this.btnRollback = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckpoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDebug = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.load_last_db_now = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
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
            this.loadLastDb = new System.Windows.Forms.CheckBox();
            this.maxRecentItemsTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.dialogSqlOpen = new System.Windows.Forms.OpenFileDialog();
            this.dialogSqlSave = new System.Windows.Forms.SaveFileDialog();
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
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.Location = new System.Drawing.Point(5, 36);
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
            this.splitMain.Size = new System.Drawing.Size(1234, 730);
            this.splitMain.SplitterDistance = 267;
            this.splitMain.TabIndex = 10;
            this.splitMain.TabStop = false;
            // 
            // tvwDatabase
            // 
            this.tvwDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwDatabase.ImageIndex = 0;
            this.tvwDatabase.ImageList = this.imgList;
            this.tvwDatabase.Location = new System.Drawing.Point(0, 1);
            this.tvwDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.tvwDatabase.Name = "tvwDatabase";
            this.tvwDatabase.SelectedImageIndex = 0;
            this.tvwDatabase.Size = new System.Drawing.Size(267, 726);
            this.tvwDatabase.TabIndex = 9;
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
            this.splitRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitRight.Location = new System.Drawing.Point(3, 26);
            this.splitRight.Name = "splitRight";
            this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitRight.Panel1.Controls.Add(this.txtSql);
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.tabResult);
            this.splitRight.Size = new System.Drawing.Size(953, 701);
            this.splitRight.SplitterDistance = 213;
            this.splitRight.TabIndex = 8;
            // 
            // txtSql
            // 
            this.txtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSql.ConvertTabsToSpaces = true;
            this.txtSql.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSql.Highlighting = "SQL";
            this.txtSql.Location = new System.Drawing.Point(0, 0);
            this.txtSql.Name = "txtSql";
            this.txtSql.ShowLineNumbers = false;
            this.txtSql.ShowVRuler = false;
            this.txtSql.Size = new System.Drawing.Size(949, 210);
            this.txtSql.TabIndex = 2;
            // 
            // tabResult
            // 
            this.tabResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabResult.Controls.Add(this.tabGrid);
            this.tabResult.Controls.Add(this.tabText);
            this.tabResult.Controls.Add(this.tabParameters);
            this.tabResult.Location = new System.Drawing.Point(0, 3);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(953, 481);
            this.tabResult.TabIndex = 0;
            this.tabResult.TabStop = false;
            this.tabResult.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabResult_Selected);
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.grdResult);
            this.tabGrid.Location = new System.Drawing.Point(4, 29);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrid.Size = new System.Drawing.Size(945, 448);
            this.tabGrid.TabIndex = 0;
            this.tabGrid.Text = "Grid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            this.grdResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Location = new System.Drawing.Point(6, 5);
            this.grdResult.Name = "grdResult";
            this.grdResult.RowHeadersWidth = 51;
            this.grdResult.Size = new System.Drawing.Size(932, 429);
            this.grdResult.TabIndex = 0;
            this.grdResult.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GrdResult_CellBeginEdit);
            this.grdResult.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdResult_CellEndEdit);
            this.grdResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GrdResult_CellFormatting);
            this.grdResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GrdResult_RowPostPaint);
            this.grdResult.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.GrdResult_SortCompare);
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.txtResult);
            this.tabText.Location = new System.Drawing.Point(4, 25);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(3);
            this.tabText.Size = new System.Drawing.Size(945, 452);
            this.tabText.TabIndex = 3;
            this.tabText.Text = "Text";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Highlighting = "JSON";
            this.txtResult.Location = new System.Drawing.Point(5, 4);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ShowLineNumbers = false;
            this.txtResult.ShowVRuler = false;
            this.txtResult.Size = new System.Drawing.Size(812, 353);
            this.txtResult.TabIndex = 1;
            // 
            // tabParameters
            // 
            this.tabParameters.Controls.Add(this.txtParameters);
            this.tabParameters.Location = new System.Drawing.Point(4, 25);
            this.tabParameters.Name = "tabParameters";
            this.tabParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tabParameters.Size = new System.Drawing.Size(945, 452);
            this.tabParameters.TabIndex = 5;
            this.tabParameters.Text = "Parameters";
            this.tabParameters.UseVisualStyleBackColor = true;
            // 
            // txtParameters
            // 
            this.txtParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParameters.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParameters.Highlighting = "JSON";
            this.txtParameters.Location = new System.Drawing.Point(6, 5);
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.ReadOnly = true;
            this.txtParameters.ShowLineNumbers = false;
            this.txtParameters.ShowVRuler = false;
            this.txtParameters.Size = new System.Drawing.Size(811, 352);
            this.txtParameters.TabIndex = 2;
            // 
            // tabSql
            // 
            this.tabSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSql.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabSql.Location = new System.Drawing.Point(3, 0);
            this.tabSql.Margin = new System.Windows.Forms.Padding(0);
            this.tabSql.Name = "tabSql";
            this.tabSql.SelectedIndex = 0;
            this.tabSql.Size = new System.Drawing.Size(953, 24);
            this.tabSql.TabIndex = 9;
            this.tabSql.TabStop = false;
            this.tabSql.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabSql_DrawItem);
            this.tabSql.SelectedIndexChanged += new System.EventHandler(this.TabSql_SelectedIndexChanged);
            this.tabSql.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabSql_Selected);
            this.tabSql.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabSql_MouseClick);
            this.tabSql.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TabSql_MouseDown);
            // 
            // stbStatus
            // 
            this.stbStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCursor,
            this.lblResultCount,
            this.prgRunning,
            this.lblElapsed});
            this.stbStatus.Location = new System.Drawing.Point(0, 767);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Size = new System.Drawing.Size(1244, 24);
            this.stbStatus.TabIndex = 11;
            this.stbStatus.Text = "statusStrip1";
            // 
            // lblCursor
            // 
            this.lblCursor.Name = "lblCursor";
            this.lblCursor.Size = new System.Drawing.Size(867, 18);
            this.lblCursor.Spring = true;
            this.lblCursor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResultCount
            // 
            this.lblResultCount.AutoSize = false;
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(150, 18);
            this.lblResultCount.Text = "0 documents";
            // 
            // prgRunning
            // 
            this.prgRunning.Name = "prgRunning";
            this.prgRunning.Size = new System.Drawing.Size(100, 16);
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = false;
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(110, 18);
            this.lblElapsed.Text = "00:00:00.0000";
            this.lblElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tlbMain
            // 
            this.tlbMain.GripMargin = new System.Windows.Forms.Padding(3);
            this.tlbMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentDBsDropDownButton,
            this.btnConnect,
            this.tlbSep1,
            this.btnRefresh,
            this.tlbSep2,
            this.btnRun,
            this.toolStripSeparator7,
            this.btnLoadSql,
            this.btnSaveSql,
            this.toolStripSeparator1,
            this.btnBegin,
            this.btnCommit,
            this.btnRollback,
            this.toolStripSeparator2,
            this.btnCheckpoint,
            this.toolStripSeparator4,
            this.btnDebug,
            this.toolStripSeparator5,
            this.load_last_db_now,
            this.toolStripSeparator8});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tlbMain.Size = new System.Drawing.Size(1244, 37);
            this.tlbMain.TabIndex = 12;
            this.tlbMain.Text = "toolStrip";
            // 
            // recentDBsDropDownButton
            // 
            this.recentDBsDropDownButton.AutoToolTip = false;
            this.recentDBsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recentDBsDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentListSettings,
            this.toolStripSeparator6});
            this.recentDBsDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("recentDBsDropDownButton.Image")));
            this.recentDBsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recentDBsDropDownButton.Name = "recentDBsDropDownButton";
            this.recentDBsDropDownButton.Size = new System.Drawing.Size(34, 30);
            this.recentDBsDropDownButton.Text = "recentDBsDropDownButton";
            this.recentDBsDropDownButton.ToolTipText = "Recent Databases";
            // 
            // recentListSettings
            // 
            this.recentListSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearRecentListToolStripMenuItem,
            this.validateRecentListToolStripMenuItem});
            this.recentListSettings.Name = "recentListSettings";
            this.recentListSettings.Size = new System.Drawing.Size(145, 26);
            this.recentListSettings.Text = "Settings";
            // 
            // clearRecentListToolStripMenuItem
            // 
            this.clearRecentListToolStripMenuItem.Name = "clearRecentListToolStripMenuItem";
            this.clearRecentListToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.clearRecentListToolStripMenuItem.Text = "Clear Recent List";
            this.clearRecentListToolStripMenuItem.Click += new System.EventHandler(this.ClearAllToolStripMenuItem_Click);
            // 
            // validateRecentListToolStripMenuItem
            // 
            this.validateRecentListToolStripMenuItem.Name = "validateRecentListToolStripMenuItem";
            this.validateRecentListToolStripMenuItem.Size = new System.Drawing.Size(221, 26);
            this.validateRecentListToolStripMenuItem.Text = "Validate Recent List";
            this.validateRecentListToolStripMenuItem.ToolTipText = "Remove Any Not Existed Database";
            this.validateRecentListToolStripMenuItem.Click += new System.EventHandler(this.ValidateRecentListToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(142, 6);
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::LiteDB.Studio.Properties.Resources.database_connect;
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Padding = new System.Windows.Forms.Padding(3);
            this.btnConnect.Size = new System.Drawing.Size(93, 30);
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // tlbSep1
            // 
            this.tlbSep1.Name = "tlbSep1";
            this.tlbSep1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::LiteDB.Studio.Properties.Resources.arrow_refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(3);
            this.btnRefresh.Size = new System.Drawing.Size(88, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // tlbSep2
            // 
            this.tlbSep2.Name = "tlbSep2";
            this.tlbSep2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnRun
            // 
            this.btnRun.Image = global::LiteDB.Studio.Properties.Resources.resultset_next;
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Padding = new System.Windows.Forms.Padding(3);
            this.btnRun.Size = new System.Drawing.Size(64, 30);
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 33);
            // 
            // btnLoadSql
            // 
            this.btnLoadSql.Image = global::LiteDB.Studio.Properties.Resources.folder_page;
            this.btnLoadSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadSql.Name = "btnLoadSql";
            this.btnLoadSql.Size = new System.Drawing.Size(66, 30);
            this.btnLoadSql.Text = "Load";
            this.btnLoadSql.ToolTipText = "Load query from file";
            this.btnLoadSql.Click += new System.EventHandler(this.BtnLoadSql_Click);
            // 
            // btnSaveSql
            // 
            this.btnSaveSql.Image = global::LiteDB.Studio.Properties.Resources.disk;
            this.btnSaveSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveSql.Name = "btnSaveSql";
            this.btnSaveSql.Size = new System.Drawing.Size(64, 30);
            this.btnSaveSql.Text = "Save";
            this.btnSaveSql.ToolTipText = "Save query into file";
            this.btnSaveSql.Click += new System.EventHandler(this.BtnSaveSql_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnBegin
            // 
            this.btnBegin.Image = global::LiteDB.Studio.Properties.Resources.database;
            this.btnBegin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(71, 30);
            this.btnBegin.Text = "Begin";
            this.btnBegin.ToolTipText = "Begin Transaction";
            this.btnBegin.Click += new System.EventHandler(this.BtnBegin_Click);
            // 
            // btnCommit
            // 
            this.btnCommit.Image = global::LiteDB.Studio.Properties.Resources.database_save;
            this.btnCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(86, 30);
            this.btnCommit.Text = "Commit";
            this.btnCommit.Click += new System.EventHandler(this.BtnCommit_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.Image = global::LiteDB.Studio.Properties.Resources.database_delete;
            this.btnRollback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(90, 30);
            this.btnRollback.Text = "Rollback";
            this.btnRollback.Click += new System.EventHandler(this.BtnRollback_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnCheckpoint
            // 
            this.btnCheckpoint.Image = global::LiteDB.Studio.Properties.Resources.application_put;
            this.btnCheckpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckpoint.Name = "btnCheckpoint";
            this.btnCheckpoint.Size = new System.Drawing.Size(107, 30);
            this.btnCheckpoint.Text = "Checkpoint";
            this.btnCheckpoint.Click += new System.EventHandler(this.BtnCheckpoint_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // btnDebug
            // 
            this.btnDebug.Image = global::LiteDB.Studio.Properties.Resources.bug_link;
            this.btnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(78, 30);
            this.btnDebug.Text = "Debug";
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // load_last_db_now
            // 
            this.load_last_db_now.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.load_last_db_now.Image = global::LiteDB.Studio.Properties.Resources.load_last_db;
            this.load_last_db_now.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.load_last_db_now.Name = "load_last_db_now";
            this.load_last_db_now.Size = new System.Drawing.Size(29, 30);
            this.load_last_db_now.Text = "toolStripButton1";
            this.load_last_db_now.ToolTipText = "Load Last Db";
            this.load_last_db_now.Click += new System.EventHandler(this.LoadLastDbNow_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 33);
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
            this.ctxMenu.Size = new System.Drawing.Size(186, 224);
            this.ctxMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CtxMenu_ItemClicked);
            // 
            // mnuQueryAll
            // 
            this.mnuQueryAll.Image = global::LiteDB.Studio.Properties.Resources.table_lightning;
            this.mnuQueryAll.Name = "mnuQueryAll";
            this.mnuQueryAll.Size = new System.Drawing.Size(185, 26);
            this.mnuQueryAll.Tag = "SELECT $ FROM {0};";
            this.mnuQueryAll.Text = "Query";
            // 
            // mnuQueryCount
            // 
            this.mnuQueryCount.Image = global::LiteDB.Studio.Properties.Resources.table;
            this.mnuQueryCount.Name = "mnuQueryCount";
            this.mnuQueryCount.Size = new System.Drawing.Size(185, 26);
            this.mnuQueryCount.Tag = "SELECT COUNT(*) FROM {0};";
            this.mnuQueryCount.Text = "Count";
            // 
            // mnuExplanPlan
            // 
            this.mnuExplanPlan.Image = global::LiteDB.Studio.Properties.Resources.table_sort;
            this.mnuExplanPlan.Name = "mnuExplanPlan";
            this.mnuExplanPlan.Size = new System.Drawing.Size(185, 26);
            this.mnuExplanPlan.Tag = "EXPLAIN SELECT $ FROM {0};";
            this.mnuExplanPlan.Text = "Explain plan";
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(182, 6);
            // 
            // mnuIndexes
            // 
            this.mnuIndexes.Image = global::LiteDB.Studio.Properties.Resources.key;
            this.mnuIndexes.Name = "mnuIndexes";
            this.mnuIndexes.Size = new System.Drawing.Size(185, 26);
            this.mnuIndexes.Tag = "SELECT $ FROM $indexes WHERE collection = \"{0}\";";
            this.mnuIndexes.Text = "Indexes";
            // 
            // mnuSep2
            // 
            this.mnuSep2.Name = "mnuSep2";
            this.mnuSep2.Size = new System.Drawing.Size(182, 6);
            // 
            // mnuExport
            // 
            this.mnuExport.Image = global::LiteDB.Studio.Properties.Resources.table_save;
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(185, 26);
            this.mnuExport.Tag = "SELECT $\\n  INTO $file(\'C:/temp/{0}.json\')\\n  FROM {0};";
            this.mnuExport.Text = "Export to JSON";
            // 
            // mnuAnalyze
            // 
            this.mnuAnalyze.Image = global::LiteDB.Studio.Properties.Resources.page_white_gear;
            this.mnuAnalyze.Name = "mnuAnalyze";
            this.mnuAnalyze.Size = new System.Drawing.Size(185, 26);
            this.mnuAnalyze.Tag = "ANALYZE {0};";
            this.mnuAnalyze.Text = "Analyze";
            // 
            // mnuRename
            // 
            this.mnuRename.Image = global::LiteDB.Studio.Properties.Resources.textfield_rename;
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.Size = new System.Drawing.Size(185, 26);
            this.mnuRename.Tag = "RENAME COLLECTION {0} TO new_name;";
            this.mnuRename.Text = "Rename";
            // 
            // mnuDropCollection
            // 
            this.mnuDropCollection.Image = global::LiteDB.Studio.Properties.Resources.table_delete;
            this.mnuDropCollection.Name = "mnuDropCollection";
            this.mnuDropCollection.Size = new System.Drawing.Size(185, 26);
            this.mnuDropCollection.Tag = "DROP COLLECTION {0};";
            this.mnuDropCollection.Text = "Drop collection";
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
            this.ctxMenuRoot.Size = new System.Drawing.Size(203, 88);
            this.ctxMenuRoot.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CtxMenuRoot_ItemClicked);
            // 
            // mnuInfo
            // 
            this.mnuInfo.Image = global::LiteDB.Studio.Properties.Resources.information;
            this.mnuInfo.Name = "mnuInfo";
            this.mnuInfo.Size = new System.Drawing.Size(202, 26);
            this.mnuInfo.Tag = "SELECT $ FROM $database;";
            this.mnuInfo.Text = "Database Info";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(199, 6);
            // 
            // mnuImport
            // 
            this.mnuImport.Image = global::LiteDB.Studio.Properties.Resources.layout_add;
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(202, 26);
            this.mnuImport.Tag = "SELECT $\\n  INTO new_col\\n  FROM $file(\'C:/temp/file.json\');";
            this.mnuImport.Text = "Import from JSON";
            // 
            // mnuRebuild
            // 
            this.mnuRebuild.Image = global::LiteDB.Studio.Properties.Resources.compress;
            this.mnuRebuild.Name = "mnuRebuild";
            this.mnuRebuild.Size = new System.Drawing.Size(202, 26);
            this.mnuRebuild.Tag = "REBUILD { collation: \'en-US/IgnoreCase\',  password: \'newpassword\' };";
            this.mnuRebuild.Text = "Rebuild";
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
            // loadLastDb
            // 
            this.loadLastDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadLastDb.AutoSize = true;
            this.loadLastDb.BackColor = System.Drawing.Color.Transparent;
            this.loadLastDb.Location = new System.Drawing.Point(1005, 6);
            this.loadLastDb.Name = "loadLastDb";
            this.loadLastDb.Size = new System.Drawing.Size(227, 24);
            this.loadLastDb.TabIndex = 13;
            this.loadLastDb.Text = "Load last database on startup";
            this.loadLastDb.UseVisualStyleBackColor = false;
            this.loadLastDb.CheckedChanged += new System.EventHandler(this.LoadLastDbChecked_Changed);
            // 
            // dialogSqlOpen
            // 
            this.dialogSqlOpen.Filter = "SQL Files|*.sql|All files|*.*";
            // 
            // dialogSqlSave
            // 
            this.dialogSqlSave.Filter = "SQL Files|*.sql|All files|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 791);
            this.Controls.Add(this.loadLastDb);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiteDB Studio";
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

        private System.Windows.Forms.ToolStripButton btnBegin;
        private System.Windows.Forms.ToolStripButton btnCheckpoint;
        private System.Windows.Forms.ToolStripButton btnCommit;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripButton btnDebug;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnRollback;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripMenuItem recentListSettings;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ContextMenuStrip ctxMenuRoot;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.ImageList imgCodeCompletion;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ToolStripStatusLabel lblCursor;
        private System.Windows.Forms.ToolStripStatusLabel lblElapsed;
        private System.Windows.Forms.ToolStripStatusLabel lblResultCount;
        private System.Windows.Forms.ToolStripButton load_last_db_now;
        private System.Windows.Forms.CheckBox loadLastDb;
        private System.Windows.Forms.ToolStripMenuItem mnuAnalyze;
        private System.Windows.Forms.ToolStripMenuItem mnuDropCollection;
        private System.Windows.Forms.ToolStripMenuItem mnuExplanPlan;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuIndexes;
        private System.Windows.Forms.ToolStripMenuItem mnuInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryAll;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryCount;
        private System.Windows.Forms.ToolStripMenuItem mnuRebuild;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripSeparator mnuSep2;
        private System.Windows.Forms.ToolStripProgressBar prgRunning;
        private System.Windows.Forms.ToolStripDropDownButton recentDBsDropDownButton;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitRight;
        private System.Windows.Forms.StatusStrip stbStatus;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.TabPage tabParameters;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabControl tabSql;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.ToolStrip tlbMain;
        private System.Windows.Forms.ToolStripSeparator tlbSep1;
        private System.Windows.Forms.ToolStripSeparator tlbSep2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TreeView tvwDatabase;
        private ICSharpCode.TextEditor.TextEditorControl txtParameters;
        private ICSharpCode.TextEditor.TextEditorControl txtResult;
        private ICSharpCode.TextEditor.TextEditorControl txtSql;

        #endregion

        private System.Windows.Forms.ToolStripMenuItem clearRecentListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateRecentListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolTip maxRecentItemsTooltip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnLoadSql;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnSaveSql;
        private System.Windows.Forms.OpenFileDialog dialogSqlOpen;
        private System.Windows.Forms.SaveFileDialog dialogSqlSave;
    }
}

