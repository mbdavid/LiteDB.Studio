﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDB.Studio.Classes;
using LiteDB.Studio.Classes.Debugger;
using LiteDB.Studio.ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio.Forms
{
    public partial class MainForm : Form
    {
        private readonly SynchronizationContext _synchronizationContext;
        private readonly SqlCodeCompletion _codeCompletion;
        private ConnectionString _connectionString;

        private LiteDatabase _db;
        private DatabaseDebugger _debugger;

        public MainForm(string filename)
        {
            InitializeComponent();

            // For performance https://stackoverflow.com/questions/4255148/how-to-improve-painting-performance-of-datagridview
            grdResult.DoubleBuffered(true);

            _synchronizationContext = SynchronizationContext.Current;

            _codeCompletion = new SqlCodeCompletion(txtSql, imgCodeCompletion);

            if (string.IsNullOrWhiteSpace(filename))
                Disconnect();
            else
                Connect(new ConnectionString(filename));

            txtSql.ActiveTextAreaControl.TextArea.Caret.PositionChanged += (s, e) =>
            {
                if (ActiveTask == null) return;

                ActiveTask.EditorContent = txtSql.Text;
                ActiveTask.SelectedTab = tabResult.SelectedTab.Name;
                ActiveTask.Position = new Tuple<int, int>(txtSql.ActiveTextAreaControl.TextArea.Caret.Line,
                    txtSql.ActiveTextAreaControl.TextArea.Caret.Column);

                lblCursor.Text =
                    $"Line: {txtSql.ActiveTextAreaControl.Caret.Line + 1} - Column: {txtSql.ActiveTextAreaControl.Caret.Column + 1}";
            };

            // stop all threads
            FormClosing += (s, e) =>
            {
                if (_db != null) Disconnect();
            };

            // set assembly version on window title
            Text += $" (v{typeof(MainForm).Assembly.GetName().Version})";

            // load last db

            if (AppSettingsManager.ApplicationSettings.LoadLastDbOnStartup && AppSettingsManager.IsLastDbExist())
                Connect(AppSettingsManager.ApplicationSettings.LastConnectionStrings);


            // validate recent list
            AppSettingsManager.ValidateRecentList();


            // populate recent db list
            PopulateRecentList();
        }

        private TaskData ActiveTask => tabSql.SelectedTab?.Tag as TaskData;

        private async Task<LiteDatabase> AsyncConnect(ConnectionString connectionString)
        {
            return await Task.Run(() => { return new LiteDatabase(connectionString); });
        }

        public async void Connect(ConnectionString connectionString)
        {
            lblCursor.Text = "Opening " + connectionString.Filename;
            lblElapsed.Text = "Reading...";
            prgRunning.Style = ProgressBarStyle.Marquee;
            btnConnect.Enabled = false;

            try
            {
                _db = await AsyncConnect(connectionString);

                // force open database
                var uv = _db.UserVersion;
            }
            catch (Exception ex)
            {
                _db?.Dispose();
                _db = null;

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            finally
            {
                lblCursor.Text = "";
                lblElapsed.Text = "";
                prgRunning.Style = ProgressBarStyle.Blocks;
                btnConnect.Enabled = true;
            }

            btnConnect.Enabled = true;
            lblCursor.Text = "";
            prgRunning.Style = ProgressBarStyle.Blocks;

            _connectionString = connectionString;

            _codeCompletion.UpdateCodeCompletion(_db);

            btnConnect.Text = "Disconnect";

            UIState(true);

            tabSql.TabPages.Add("+", "+");
            LoadTreeView();
            AddNewTab("");

            txtSql.Focus();
        }

        private void Disconnect()
        {
            foreach (var tab in tabSql.TabPages.Cast<TabPage>().Where(x => x.Name != "+").ToArray())
            {
                var task = tab.Tag as TaskData;
                task.ThreadRunning = false;
                task.WaitHandle.Set();
            }

            // clear all tabs and controls
            tabSql.TabPages.Clear();

            txtSql.Clear();
            grdResult.Clear();
            txtResult.Clear();
            txtParameters.Clear();

            tvwDatabase.Nodes.Clear();

            btnConnect.Text = "Connect";

            UIState(false);

            tvwDatabase.Focus();

            tlbMain.Enabled = false;
            lblCursor.Text = "Closing...";

            try
            {
                _debugger?.Dispose();
                _debugger = null;

                _db?.Dispose();
                _db = null;

                lblCursor.Text = "";
                tlbMain.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void UIState(bool enabled)
        {
            splitRight.Visible = enabled;
            tabSql.Visible = enabled;
            tvwDatabase.Visible = enabled;

            btnRefresh.Enabled = enabled;
            tabSql.Enabled = enabled;
            btnRun.Enabled = enabled;

            btnBegin.Enabled = enabled;
            btnCommit.Enabled = enabled;
            btnRollback.Enabled = enabled;
            btnCheckpoint.Enabled = enabled;
            btnDebug.Enabled = enabled;
        }

        private void AddNewTab(string content)
        {
            // find + tab
            var tab = tabSql.TabPages.Cast<TabPage>().Where(x => x.Text == "+").Single();

            var task = new TaskData {EditorContent = content};

            task.Thread = new Thread(() => CreateThread(task));
            task.Thread.Start();

            task.Id = task.Thread.ManagedThreadId;

            tab.Text = tab.Name = task.Id.ToString();
            tab.Tag = task;

            if (tabSql.SelectedTab != tab) tabSql.SelectTab(tab);

            // adding new + tab at end
            tabSql.TabPages.Add("+", "+");

            tabResult.SelectTab("tabGrid");
        }

        private void ExecuteSql(string sql)
        {
            if (ActiveTask?.Executing == false)
            {
                ActiveTask.Sql = sql;
                ActiveTask.WaitHandle.Set();
            }
        }

        private void LoadTreeView()
        {
            tvwDatabase.Nodes.Clear();

            var root = tvwDatabase.Nodes.Add(Path.GetFileName(_connectionString.Filename));
            var system = root.Nodes.Add("System");

            root.ImageKey = "database";
            root.ContextMenuStrip = ctxMenuRoot;

            system.ImageKey = system.SelectedImageKey = "folder";

            var sc = _db.GetCollection("$cols")
                .Query()
                .Where("type = 'system'")
                .OrderBy("name")
                .ToDocuments();

            foreach (var doc in sc)
            {
                var col = system.Nodes.Add(doc["name"].AsString);
                col.Tag = $"SELECT $ FROM {doc["name"].AsString}";
                col.ImageKey = col.SelectedImageKey = "table_gear";
            }

            root.ExpandAll();
            system.Toggle();

            foreach (var key in _db.GetCollectionNames().OrderBy(x => x))
            {
                var col = root.Nodes.Add(key);
                col.Tag = $"SELECT $ FROM {key};";
                col.ImageKey = col.SelectedImageKey = "table";
                col.ContextMenuStrip = ctxMenu;
            }
        }

        private void CreateThread(TaskData task)
        {
            while (true)
            {
                task.WaitHandle.Wait();

                if (task.ThreadRunning == false) break;

                if (task.Sql.Trim() == "")
                {
                    task.WaitHandle.Reset();
                    continue;
                }

                var sw = new Stopwatch();
                sw.Start();

                try
                {
                    task.Executing = true;
                    task.IsGridLoaded = task.IsTextLoaded = task.IsParametersLoaded = false;

                    _synchronizationContext.Post(o => { LoadResult(task); }, task);

                    task.Parameters = new BsonDocument();

                    var sql = new StringReader(task.Sql.Trim());

                    while (sql.Peek() >= 0 && _db != null)
                        using (var reader = _db.Execute(sql, task.Parameters))
                        {
                            task.ReadResult(reader);
                        }

                    task.Elapsed = sw.Elapsed;
                    task.Exception = null;
                    task.Executing = false;

                    // update form button selected
                    _synchronizationContext.Post(o =>
                    {
                        var t = o as TaskData;

                        if (ActiveTask?.Id == t.Id) LoadResult(o as TaskData);
                    }, task);
                }
                catch (Exception ex)
                {
                    task.Executing = false;
                    task.Result = null;
                    task.Elapsed = sw.Elapsed;
                    task.Exception = ex;

                    _synchronizationContext.Post(o =>
                    {
                        var t = o as TaskData;

                        if (ActiveTask?.Id == t.Id)
                        {
                            tabResult.SelectedTab = tabText;
                            LoadResult(o as TaskData);
                        }
                    }, task);
                }

                // put thread in wait mode
                task.WaitHandle.Reset();
            }

            task.WaitHandle.Dispose();
        }

        private void LoadResult(TaskData data)
        {
            if (data == null) return;

            btnRun.Enabled = !data.Executing;

            if (data.Executing)
            {
                grdResult.Clear();
                txtResult.Clear();
                txtParameters.Clear();

                lblResultCount.Visible = false;
                lblElapsed.Text = "Running";
                prgRunning.Style = ProgressBarStyle.Marquee;
                txtParameters.Clear();
            }
            else
            {
                lblResultCount.Visible = true;
                lblElapsed.Text = data.Elapsed.ToString();
                prgRunning.Style = ProgressBarStyle.Blocks;
                lblResultCount.Text =
                    data.Result == null ? "" :
                    data.Result.Count == 0 ? "no documents" :
                    data.Result.Count == 1 ? "1 document" :
                    data.Result.Count + (data.LimitExceeded ? "+" : "") + " documents";

                if (data.Exception != null)
                {
                    txtResult.BindErrorMessage(data.Sql, data.Exception);
                    txtParameters.BindErrorMessage(data.Sql, data.Exception);
                    grdResult.BindErrorMessage(data.Exception);
                }
                else if (data.Result != null)
                {
                    if (tabResult.SelectedTab == tabGrid && data.IsGridLoaded == false)
                    {
                        grdResult.BindBsonData(data);
                        data.IsGridLoaded = true;
                    }
                    else if (tabResult.SelectedTab == tabText && data.IsTextLoaded == false)
                    {
                        txtResult.BindBsonData(data);
                        data.IsTextLoaded = true;
                    }
                    else if (tabResult.SelectedTab == tabParameters && data.IsParametersLoaded == false)
                    {
                        txtParameters.BindParameter(data);
                        data.IsParametersLoaded = true;
                    }
                }
            }
        }

        private void AddSqlSnippet(string sql)
        {
            if (txtSql.Text.Trim().Length == 0)
                txtSql.Text = sql.Replace("\\n", "\n");
            else
                AddNewTab(sql.Replace("\\n", "\n"));
        }


        /// <summary>
        ///     Return string as c:\windows.....\fileName.ext
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startCounter"></param>
        /// <returns></returns>
        private string BalanceString(string path, int startCounter = 20)
        {
            var fileName = Path.GetFileName(path);
            var direName = Directory.GetParent(path).FullName;
            return startCounter > direName.Length + 3 ? path : $@"{direName.Substring(0, startCounter)}...\{fileName}";
        }

        private void PopulateRecentList()
        {
            var dbs = AppSettingsManager.ApplicationSettings.RecentConnectionStrings;

            var length = dbs.Count;
            var bts = new ToolStripItem[length];

            void HandleClick(object sender, EventArgs eventArgs)
            {
                if (!(sender is ToolStripMenuItem but)) return;
                var index = int.Parse(but.Name);
                var db = dbs[index];
                Disconnect();
                if (!AppSettingsManager.IsDbExist(db.Filename)) return;
                Disconnect();
                Connect(db);
            }

            // clear the old list to prevent memory leaks

            var oldLength = recentDBsDropDownButton.DropDownItems.Count;

            for (var i = 0; i < oldLength - 1; i++)
            {
                // unsubscribe
                if (recentDBsDropDownButton.DropDownItems[1] is ToolStripMenuItem item) item.Click -= HandleClick;
                recentDBsDropDownButton.DropDownItems.RemoveAt(1);
            }

            // populate the list again
            for (var i = 0; i < length; i++)
            {
                var db = dbs[i];
                var t = new ToolStripMenuItem(BalanceString(db.Filename), null, null, i.ToString())
                {
                    ToolTipText = db.Filename
                };
                t.Click += HandleClick;
                bts[i] = t;
            }

            recentDBsDropDownButton.DropDownItems.AddRange(bts);
        }

        private void GrdResult_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            var value1 = e.CellValue1 as BsonValue ?? BsonValue.Null;
            var value2 = e.CellValue2 as BsonValue ?? BsonValue.Null;
            e.SortResult = value1.CompareTo(value2);
            e.Handled = true;
        }

        private void GrdResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var value = e.Value as BsonValue;

            if (value == null)
            {
                e.Value = "";
                return;
            }

            switch (value.Type)
            {
                case BsonType.MinValue:
                    e.Value = "-∞";
                    break;
                case BsonType.MaxValue:
                    e.Value = "+∞";
                    break;
                case BsonType.Boolean:
                    e.Value = value.AsBoolean.ToString().ToLower();
                    break;
                case BsonType.DateTime:
                    e.Value = value.AsDateTime.ToString();
                    break;
                case BsonType.Null:
                    e.Value = "(null)";
                    e.CellStyle.ForeColor = Color.Silver;
                    break;
                case BsonType.Binary:
                    e.Value = Convert.ToBase64String(value.AsBinary);
                    break;
                case BsonType.Int32:
                case BsonType.Int64:
                case BsonType.Double:
                case BsonType.Decimal:
                    e.Value = value.RawValue.ToString();
                    break;
                case BsonType.String:
                case BsonType.ObjectId:
                case BsonType.Guid:
                    e.Value = value.ToString();
                    break;
                default:
                    e.Value = JsonSerializer.Serialize(value);
                    break;
            }
        }


        private void Settings_Click(object sender, EventArgs e)
        {
            new SettingsForm(AppSettingsManager.ApplicationSettings).ShowDialog();
        }

        private void ClearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear UI
            var count = recentDBsDropDownButton.DropDownItems.Count;
            for (var i = 1; i < count; i++) recentDBsDropDownButton.DropDownItems.RemoveAt(1);
            // clear the list
            AppSettingsManager.ClearRecentList();
        }

        private void ValidateRecentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettingsManager.ValidateRecentList();
            PopulateRecentList();
        }

        #region Grid Edit

        private void GrdResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var field = grdResult.Columns[e.ColumnIndex].HeaderText;
            var id = grdResult.Rows[e.RowIndex].Cells["_id"].Tag as BsonValue;
            var cell = grdResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var current = cell.Tag as BsonValue;
            var text = cell.Value.ToString();

            // try run update collection using current/new value
            var value = UpdateCellGrid(id, field, current, text);

            if (value != current) cell.Style.BackColor = Color.LightGreen;

            cell.Value = value;
            cell.Tag = value;
        }

        private void GrdResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds =
                new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        public BsonValue UpdateCellGrid(BsonValue id, string field, BsonValue current, string json)
        {
            try
            {
                var value = JsonSerializer.Deserialize(json);

                if (current == value) return current;

                var r = _db.Execute($"UPDATE {ActiveTask.Collection} SET {field} = @0 WHERE _id = @1 AND {field} = @2",
                    new BsonDocument
                    {
                        ["0"] = value,
                        ["1"] = id,
                        ["2"] = current
                    });

                if (r.Current == 1) return value;

                throw new Exception("Current document was not found. Try run your query again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return current;
            }
        }

        #endregion

        #region Toolbar

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadTreeView();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && btnRun.Enabled) BtnRun_Click(btnRun, EventArgs.Empty);
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            var sql = txtSql.ActiveTextAreaControl.SelectionManager.SelectedText.Length > 0
                ? txtSql.ActiveTextAreaControl.SelectionManager.SelectedText
                : txtSql.Text;

            ExecuteSql(sql);
        }

        private void BtnBegin_Click(object sender, EventArgs e)
        {
            ExecuteSql("BEGIN");
        }

        private void BtnCommit_Click(object sender, EventArgs e)
        {
            ExecuteSql("COMMIT");
        }

        private void BtnRollback_Click(object sender, EventArgs e)
        {
            ExecuteSql("ROLLBACK");
        }

        private void BtnCheckpoint_Click(object sender, EventArgs e)
        {
            ExecuteSql("CHECKPOINT");
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (_db == null)
            {
                var dialog = new ConnectionForm(_connectionString ?? new ConnectionString());

                dialog.ShowDialog();

                if (dialog.DialogResult != DialogResult.OK) return;

                Connect(dialog.ConnectionString);
                // re populate the list
                PopulateRecentList();
            }
            else
            {
                Disconnect();
            }
        }

        private void BtnDebug_Click(object sender, EventArgs e)
        {
            if (_debugger == null)
            {
                _debugger = new DatabaseDebugger(_db, new Random().Next(8000, 9000));

                _debugger.Start();
            }

            Process.Start("http://localhost:" + _debugger.Port);
        }

        #endregion

        #region ContextMenu

        private void CtxMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var colname = tvwDatabase.SelectedNode.Text;

            var sql = string.Format(e.ClickedItem.Tag.ToString(), colname);
            AddSqlSnippet(sql);
        }

        private void CtxMenuRoot_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var sql = e.ClickedItem.Tag.ToString();
            AddSqlSnippet(sql);
        }

        #endregion

        #region TreeView

        private void TvwCols_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) tvwDatabase.SelectedNode = tvwDatabase.GetNodeAt(e.X, e.Y);
        }

        private void TvwCols_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is string cmd) AddSqlSnippet(cmd);
        }

        #endregion

        #region Editor Tabs

        private void TabResult_Selected(object sender, TabControlEventArgs e)
        {
            if (tabSql.TabPages.Count == 0) return;

            LoadResult(ActiveTask);

            // set focus to result
            ActiveControl =
                tabResult.SelectedTab == tabGrid ? grdResult :
                tabResult.SelectedTab == tabText ? txtResult : (Control) txtParameters;
        }

        private void TabSql_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            var tabs = tabControl.TabPages;

            if (tabs.Count <= 1) return;

            if (e.Button == MouseButtons.Middle)
            {
                var tab = tabs.Cast<TabPage>()
                    .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
                    .First();

                if (tab.Tag is TaskData task)
                {
                    task.ThreadRunning = false;
                    task.WaitHandle.Set();
                    tabs.Remove(tab);
                }
            }
        }

        private void TabSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this event occurs after tab already selected
            Application.DoEvents();

            txtSql.ActiveTextAreaControl.TextArea.Focus();
        }

        private void TabSql_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null) return;

            txtSql.Clear();
            grdResult.Clear();
            txtResult.Clear();
            txtParameters.Clear();

            lblResultCount.Visible = false;
            lblElapsed.Text = "";
            prgRunning.Style = ProgressBarStyle.Blocks;
            lblResultCount.Text = "";

            Application.DoEvents();

            if (e.TabPage.Name == "+")
            {
                AddNewTab("");
            }
            else
            {
                // restore data
                ActiveTask.IsGridLoaded = ActiveTask.IsTextLoaded = ActiveTask.IsParametersLoaded = false;

                txtSql.Text = ActiveTask.EditorContent;

                if (ActiveTask.Position != null)
                {
                    txtSql.ActiveTextAreaControl.TextArea.Caret.Line = ActiveTask.Position.Item1;
                    txtSql.ActiveTextAreaControl.TextArea.Caret.Column = ActiveTask.Position.Item2;
                }

                if (tabResult.SelectedTab.Name != ActiveTask.SelectedTab && ActiveTask.SelectedTab != "")
                    tabResult.SelectTab(ActiveTask.SelectedTab); // fire LoadResult from TabResult_IndexChanged
                else
                    LoadResult(ActiveTask);
            }
        }

        #endregion
    }
}