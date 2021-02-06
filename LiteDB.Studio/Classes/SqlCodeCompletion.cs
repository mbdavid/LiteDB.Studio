// This code was based on the CSharp Editor Example with Code Completion created by Daniel Grunwald

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace LiteDB.Studio.Classes
{
    public class SqlCodeCompletion : ICompletionDataProvider
    {
        private readonly TextEditorControl _editor;
        private List<ICompletionData> _codeCompletionData = new List<ICompletionData>();
        private CodeCompletionWindow _codeCompletionWindow;

        public SqlCodeCompletion(TextEditorControl control, ImageList imageList)
        {
            _editor = control;
            ImageList = imageList;

            control.ActiveTextAreaControl.TextArea.KeyDown += (o, s) =>
            {
                // open via "ctrl+space"
                if (s.Control && s.KeyCode == Keys.Space)
                {
                    s.SuppressKeyPress = true;
                    ShowCodeCompleteWindow('\0');
                }
            };

            control.ActiveTextAreaControl.TextArea.KeyEventHandler += key =>
            {
                if (_codeCompletionWindow != null)
                    if (_codeCompletionWindow.ProcessKeyEvent(key))
                        return true;

                return false;
            };

            control.Disposed +=
                CloseCodeCompletionWindow; // When the editor is disposed, close the code completion window

            UpdateCodeCompletion(null);
        }

        private ICompletionDataProvider _completionDataProvider { get; set; }

        public ImageList ImageList { get; }

        public string PreSelection => FindExpression();

        public int DefaultIndex => -1;

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            return _codeCompletionData.ToArray();
        }

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);

            return data.InsertAction(textArea, key);
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            if (char.IsLetterOrDigit(key) || key == '_') return CompletionDataProviderKeyResult.NormalKey;

            // key triggers insertion of selected items
            return CompletionDataProviderKeyResult.InsertionKey;
        }

        private void ShowCodeCompleteWindow(char key)
        {
            try
            {
                _completionDataProvider = this;

                _codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(
                    _editor.ParentForm,
                    _editor,
                    "file.sql",
                    _completionDataProvider,
                    key
                );

                if (_codeCompletionWindow != null) _codeCompletionWindow.Closed += CloseCodeCompletionWindow;
            }
            catch (Exception)
            {
            }
        }

        private void CloseCodeCompletionWindow(object sender, EventArgs e)
        {
            if (_codeCompletionWindow != null)
            {
                _codeCompletionWindow.Closed -= CloseCodeCompletionWindow;
                _codeCompletionWindow.Dispose();
                _codeCompletionWindow = null;
            }
        }

        private string FindExpression()
        {
            var textArea = _editor.ActiveTextAreaControl.TextArea;

            try
            {
                var word = textArea.Document.GetLineSegment(textArea.Caret.Position.Line)
                    .GetWord(textArea.Caret.Position.Column - 1);

                return word?.Word ?? "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void UpdateCodeCompletion(LiteDatabase db)
        {
            _codeCompletionData = new List<ICompletionData>();

            var item = new DefaultCompletionData(DateTime.Now.Second.ToString(), "segundos", 4);
            item.Priority = double.MaxValue;

            _codeCompletionData.Add(item);


            // getting all BsonExpression methods
            foreach (var m in BsonExpression.Methods.OrderBy(x => x.Name))
            {
                var text = m.Name;
                var description = $"Method:\n-   {text}({string.Join(", ", m.GetParameters().Select(x => x.Name))})";
                var icon = 0; // METHOD

                _codeCompletionData.Add(new DefaultCompletionData(text, description, icon));
            }

            // get all keywords
            var words = new List<string>();

            using (var stream =
                typeof(SqlCodeCompletion).Assembly.GetManifestResourceStream(
                    "LiteDB.Studio.ICSharpCode.TextEditor.Resources.SQL-Mode.xshd"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    var xml = new XmlDocument();
                    xml.LoadXml(content);

                    var nodes = xml.DocumentElement.SelectNodes(
                        "/SyntaxDefinition/RuleSets/RuleSet/KeyWords[@name=\"SqlKeywordsNormal\"]/Key");

                    words.AddRange(nodes.Cast<XmlNode>().Select(x => x.Attributes["word"].Value));
                }
            }

            _codeCompletionData.AddRange(words.OrderBy(x => x).Select(x => new DefaultCompletionData(x, null, 3)));

            if (db == null) return;

            // collections
            var cols = db.GetCollection("$cols").Query().ToArray();

            _codeCompletionData.AddRange(cols.Select(x => new DefaultCompletionData(x["name"].AsString,
                (x["type"] == "user" ? "User collection:\n-   " : "System collection:\n-   ") +
                x["name"].AsString,
                x["type"] == "user" ? 1 :
                x["type"] == "system" ? 5 : 4)));
        }
    }
}