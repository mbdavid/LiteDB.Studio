// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Actions;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    public class TextAreaClipboardHandler
    {
        public delegate bool ClipboardContainsTextDelegate();

        private const string
            LineSelectedType =
                "MSDEVLineSelect"; // This is the type VS 2003 and 2005 use for flagging a whole line copy

        /// <summary>
        ///     Is called when CachedClipboardContainsText should be updated.
        ///     If this property is null (the default value), the text editor uses
        ///     System.Windows.Forms.Clipboard.ContainsText.
        /// </summary>
        /// <remarks>
        ///     This property is useful if you want to prevent the default Clipboard.ContainsText
        ///     behaviour that waits for the clipboard to be available - the clipboard might
        ///     never become available if it is owned by a process that is paused by the debugger.
        /// </remarks>
        public static ClipboardContainsTextDelegate GetClipboardContainsText;

        // Code duplication: TextAreaClipboardHandler.cs also has SafeSetClipboard
        [ThreadStatic] private static int SafeSetClipboardDataVersion;
        private readonly TextArea textArea;

        public TextAreaClipboardHandler(TextArea textArea)
        {
            this.textArea = textArea;
            textArea.SelectionManager.SelectionChanged += DocumentSelectionChanged;
        }

        public bool EnableCut => textArea.EnableCutOrPaste; //textArea.SelectionManager.HasSomethingSelected;

        public bool EnableCopy => true; //textArea.SelectionManager.HasSomethingSelected;

        public bool EnablePaste
        {
            get
            {
                if (!textArea.EnableCutOrPaste)
                    return false;
                var d = GetClipboardContainsText;
                if (d != null)
                    return d();
                try
                {
                    return Clipboard.ContainsText();
                }
                catch (ExternalException)
                {
                    return false;
                }
            }
        }

        public bool EnableDelete => textArea.SelectionManager.HasSomethingSelected &&
                                    !textArea.SelectionManager.SelectionIsReadonly;

        public bool EnableSelectAll => true;

        private void DocumentSelectionChanged(object sender, EventArgs e)
        {
//			((DefaultWorkbench)WorkbenchSingleton.Workbench).UpdateToolbars();
        }

        private bool CopyTextToClipboard(string stringToCopy, bool asLine)
        {
            if (stringToCopy.Length > 0)
            {
                var dataObject = new DataObject();
                dataObject.SetData(DataFormats.UnicodeText, true, stringToCopy);
                if (asLine)
                {
                    var lineSelected = new MemoryStream(1);
                    lineSelected.WriteByte(1);
                    dataObject.SetData(LineSelectedType, false, lineSelected);
                }

                // Default has no highlighting, therefore we don't need RTF output
                if (textArea.Document.HighlightingStrategy.Name != "Default")
                    dataObject.SetData(DataFormats.Rtf, RtfWriter.GenerateRtf(textArea));
                OnCopyText(new CopyTextEventArgs(stringToCopy));

                SafeSetClipboard(dataObject);
                return true;
            }

            return false;
        }

        private static void SafeSetClipboard(object dataObject)
        {
            // Work around ExternalException bug. (SD2-426)
            // Best reproducable inside Virtual PC.
            var version = unchecked(++SafeSetClipboardDataVersion);
            try
            {
                Clipboard.SetDataObject(dataObject, true);
            }
            catch (ExternalException)
            {
                var timer = new Timer();
                timer.Interval = 100;
                timer.Tick += delegate
                {
                    timer.Stop();
                    timer.Dispose();
                    if (SafeSetClipboardDataVersion == version)
                        try
                        {
                            Clipboard.SetDataObject(dataObject, true, 10, 50);
                        }
                        catch (ExternalException)
                        {
                        }
                };
                timer.Start();
            }
        }

        private bool CopyTextToClipboard(string stringToCopy)
        {
            return CopyTextToClipboard(stringToCopy, false);
        }

        public void Cut(object sender, EventArgs e)
        {
            if (textArea.SelectionManager.HasSomethingSelected)
            {
                if (CopyTextToClipboard(textArea.SelectionManager.SelectedText))
                {
                    if (textArea.SelectionManager.SelectionIsReadonly)
                        return;
                    // Remove text
                    textArea.BeginUpdate();
                    textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
                    textArea.SelectionManager.RemoveSelectedText();
                    textArea.EndUpdate();
                }
            }
            else if (textArea.Document.TextEditorProperties.CutCopyWholeLine)
            {
                // No text was selected, select and cut the entire line
                var curLineNr = textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset);
                var lineWhereCaretIs = textArea.Document.GetLineSegment(curLineNr);
                var caretLineText = textArea.Document.GetText(lineWhereCaretIs.Offset, lineWhereCaretIs.TotalLength);
                textArea.SelectionManager.SetSelection(textArea.Document.OffsetToPosition(lineWhereCaretIs.Offset),
                    textArea.Document.OffsetToPosition(lineWhereCaretIs.Offset + lineWhereCaretIs.TotalLength));
                if (CopyTextToClipboard(caretLineText, true))
                {
                    if (textArea.SelectionManager.SelectionIsReadonly)
                        return;
                    // remove line
                    textArea.BeginUpdate();
                    textArea.Caret.Position = textArea.Document.OffsetToPosition(lineWhereCaretIs.Offset);
                    textArea.SelectionManager.RemoveSelectedText();
                    textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd,
                        new TextLocation(0, curLineNr)));
                    textArea.EndUpdate();
                }
            }
        }

        public void Copy(object sender, EventArgs e)
        {
            if (!CopyTextToClipboard(textArea.SelectionManager.SelectedText) &&
                textArea.Document.TextEditorProperties.CutCopyWholeLine)
            {
                // No text was selected, select the entire line, copy it, and then deselect
                var curLineNr = textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset);
                var lineWhereCaretIs = textArea.Document.GetLineSegment(curLineNr);
                var caretLineText = textArea.Document.GetText(lineWhereCaretIs.Offset, lineWhereCaretIs.TotalLength);
                CopyTextToClipboard(caretLineText, true);
            }
        }

        public void Paste(object sender, EventArgs e)
        {
            if (!textArea.EnableCutOrPaste) return;
            // Clipboard.GetDataObject may throw an exception...
            for (var i = 0;; i++)
                try
                {
                    var data = Clipboard.GetDataObject();
                    if (data == null)
                        return;
                    var fullLine = data.GetDataPresent(LineSelectedType);
                    if (data.GetDataPresent(DataFormats.UnicodeText))
                    {
                        var text = (string) data.GetData(DataFormats.UnicodeText);
                        // we got NullReferenceExceptions here, apparently the clipboard can contain null strings
                        if (!string.IsNullOrEmpty(text))
                        {
                            textArea.Document.UndoStack.StartUndoGroup();
                            try
                            {
                                if (textArea.SelectionManager.HasSomethingSelected)
                                {
                                    textArea.Caret.Position =
                                        textArea.SelectionManager.SelectionCollection[0].StartPosition;
                                    textArea.SelectionManager.RemoveSelectedText();
                                }

                                if (fullLine)
                                {
                                    var col = textArea.Caret.Column;
                                    textArea.Caret.Column = 0;
                                    if (!textArea.IsReadOnly(textArea.Caret.Offset))
                                        textArea.InsertString(text);
                                    textArea.Caret.Column = col;
                                }
                                else
                                {
                                    // textArea.EnableCutOrPaste already checked readonly for this case
                                    textArea.InsertString(text);
                                }
                            }
                            finally
                            {
                                textArea.Document.UndoStack.EndUndoGroup();
                            }
                        }
                    }

                    return;
                }
                catch (ExternalException)
                {
                    // GetDataObject does not provide RetryTimes parameter
                    if (i > 5) throw;
                }
        }

        public void Delete(object sender, EventArgs e)
        {
            new Delete().Execute(textArea);
        }

        public void SelectAll(object sender, EventArgs e)
        {
            new SelectWholeDocument().Execute(textArea);
        }

        protected virtual void OnCopyText(CopyTextEventArgs e)
        {
            if (CopyText != null) CopyText(this, e);
        }

        public event CopyTextEventHandler CopyText;
    }

    public delegate void CopyTextEventHandler(object sender, CopyTextEventArgs e);

    public class CopyTextEventArgs : EventArgs
    {
        public CopyTextEventArgs(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}