using System;
using System.ComponentModel;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Actions;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    public partial class ContextMenu : ContextMenuStrip
    {
        private readonly TextAreaControl parent;

        public ContextMenu(TextAreaControl parent)
        {
            this.parent = parent;
            InitializeComponent();

            undo.Click += OnClickUndo;
            cut.Click += OnClickCut;
            copy.Click += OnClickCopy;
            paste.Click += OnClickPaste;
            selectAll.Click += OnSelectAll;
        }

        private void OnClickCut(object sender, EventArgs e)
        {
            new Cut().Execute(parent.TextArea);
            parent.TextArea.Focus();
        }

        private void OnClickUndo(object sender, EventArgs e)
        {
            parent.Undo();
            parent.TextArea.Focus();
        }

        private void OnClickCopy(object sender, EventArgs e)
        {
            new Copy().Execute(parent.TextArea);
            parent.TextArea.Focus();
        }

        private void OnClickPaste(object sender, EventArgs e)
        {
            new Paste().Execute(parent.TextArea);
            parent.TextArea.Focus();
        }

        private void OnSelectAll(object sender, EventArgs e)
        {
            new SelectWholeDocument().Execute(parent.TextArea);
            parent.TextArea.Focus();
        }


        private void OnOpening(object sender, CancelEventArgs e)
        {
            undo.Enabled = parent.Document.UndoStack.CanUndo;
            cut.Enabled = copy.Enabled = delete.Enabled = parent.SelectionManager.HasSomethingSelected;
            paste.Enabled = parent.TextArea.ClipboardHandler.EnablePaste;
            selectAll.Enabled = !string.IsNullOrEmpty(parent.Document.TextContent);
        }
    }
}