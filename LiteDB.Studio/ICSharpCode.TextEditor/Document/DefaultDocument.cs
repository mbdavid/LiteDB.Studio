// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.FoldingStrategy;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.FormattingStrategy;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.LineManager;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.TextBufferStrategy;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;
using LiteDB.Studio.ICSharpCode.TextEditor.Undo;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document
{
    /// <summary>
    ///     Describes the caret marker
    /// </summary>
    public enum LineViewerStyle
    {
        /// <summary>
        ///     No line viewer will be displayed
        /// </summary>
        None,

        /// <summary>
        ///     The row in which the caret is will be marked
        /// </summary>
        FullRow
    }

    /// <summary>
    ///     Describes the indent style
    /// </summary>
    public enum IndentStyle
    {
        /// <summary>
        ///     No indentation occurs
        /// </summary>
        None,

        /// <summary>
        ///     The indentation from the line above will be
        ///     taken to indent the curent line
        /// </summary>
        Auto,

        /// <summary>
        ///     Inteligent, context sensitive indentation will occur
        /// </summary>
        Smart
    }

    /// <summary>
    ///     Describes the bracket highlighting style
    /// </summary>
    public enum BracketHighlightingStyle
    {
        /// <summary>
        ///     Brackets won't be highlighted
        /// </summary>
        None,

        /// <summary>
        ///     Brackets will be highlighted if the caret is on the bracket
        /// </summary>
        OnBracket,

        /// <summary>
        ///     Brackets will be highlighted if the caret is after the bracket
        /// </summary>
        AfterBracket
    }

    /// <summary>
    ///     Describes the selection mode of the text area
    /// </summary>
    public enum DocumentSelectionMode
    {
        /// <summary>
        ///     The 'normal' selection mode.
        /// </summary>
        Normal,

        /// <summary>
        ///     Selections will be added to the current selection or new
        ///     ones will be created (multi-select mode)
        /// </summary>
        Additive
    }

    /// <summary>
    ///     The default <see cref="IDocument" /> implementation.
    /// </summary>
    internal sealed class DefaultDocument : IDocument
    {
        // UPDATE STUFF

        public LineManager.LineManager LineManager { get; set; }

        public event EventHandler<LineLengthChangeEventArgs> LineLengthChanged
        {
            add => LineManager.LineLengthChanged += value;
            remove => LineManager.LineLengthChanged -= value;
        }

        public event EventHandler<LineCountChangeEventArgs> LineCountChanged
        {
            add => LineManager.LineCountChanged += value;
            remove => LineManager.LineCountChanged -= value;
        }

        public event EventHandler<LineEventArgs> LineDeleted
        {
            add => LineManager.LineDeleted += value;
            remove => LineManager.LineDeleted -= value;
        }

        public MarkerStrategy.MarkerStrategy MarkerStrategy { get; set; }

        public ITextEditorProperties TextEditorProperties { get; set; } = new DefaultTextEditorProperties();

        public UndoStack UndoStack { get; } = new UndoStack();

        public IList<LineSegment> LineSegmentCollection => LineManager.LineSegmentCollection;

        public bool ReadOnly { get; set; } = false;

        public ITextBufferStrategy TextBufferStrategy { get; set; }

        public IFormattingStrategy FormattingStrategy { get; set; }

        public FoldingManager FoldingManager { get; set; }

        public IHighlightingStrategy HighlightingStrategy
        {
            get => LineManager.HighlightingStrategy;
            set => LineManager.HighlightingStrategy = value;
        }

        public int TextLength => TextBufferStrategy.Length;

        public BookmarkManager.BookmarkManager BookmarkManager { get; set; }


        public string TextContent
        {
            get => GetText(0, TextBufferStrategy.Length);
            set
            {
                Debug.Assert(TextBufferStrategy != null);
                Debug.Assert(LineManager != null);
                OnDocumentAboutToBeChanged(new DocumentEventArgs(this, 0, 0, value));
                TextBufferStrategy.SetContent(value);
                LineManager.SetContent(value);
                UndoStack.ClearAll();

                OnDocumentChanged(new DocumentEventArgs(this, 0, 0, value));
                OnTextContentChanged(EventArgs.Empty);
            }
        }

        public void Insert(int offset, string text)
        {
            //if (readOnly) {
            //	return;
            //}
            OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, -1, text));

            TextBufferStrategy.Insert(offset, text);
            LineManager.Insert(offset, text);

            UndoStack.Push(new UndoableInsert(this, offset, text));

            OnDocumentChanged(new DocumentEventArgs(this, offset, -1, text));
        }

        public void Remove(int offset, int length)
        {
            //if (readOnly) {
            //	return;
            //}
            OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, length));
            UndoStack.Push(new UndoableDelete(this, offset, GetText(offset, length)));

            TextBufferStrategy.Remove(offset, length);
            LineManager.Remove(offset, length);

            OnDocumentChanged(new DocumentEventArgs(this, offset, length));
        }

        public void Replace(int offset, int length, string text)
        {
            if (ReadOnly) return;
            OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, length, text));
            UndoStack.Push(new UndoableReplace(this, offset, GetText(offset, length), text));

            TextBufferStrategy.Replace(offset, length, text);
            LineManager.Replace(offset, length, text);

            OnDocumentChanged(new DocumentEventArgs(this, offset, length, text));
        }

        public char GetCharAt(int offset)
        {
            return TextBufferStrategy.GetCharAt(offset);
        }

        public string GetText(int offset, int length)
        {
#if DEBUG
            if (length < 0) throw new ArgumentOutOfRangeException("length", length, "length < 0");
#endif
            return TextBufferStrategy.GetText(offset, length);
        }

        public string GetText(ISegment segment)
        {
            return GetText(segment.Offset, segment.Length);
        }

        public int TotalNumberOfLines => LineManager.TotalNumberOfLines;

        public int GetLineNumberForOffset(int offset)
        {
            return LineManager.GetLineNumberForOffset(offset);
        }

        public LineSegment GetLineSegmentForOffset(int offset)
        {
            return LineManager.GetLineSegmentForOffset(offset);
        }

        public LineSegment GetLineSegment(int line)
        {
            return LineManager.GetLineSegment(line);
        }

        public int GetFirstLogicalLine(int lineNumber)
        {
            return LineManager.GetFirstLogicalLine(lineNumber);
        }

        public int GetLastLogicalLine(int lineNumber)
        {
            return LineManager.GetLastLogicalLine(lineNumber);
        }

        public int GetVisibleLine(int lineNumber)
        {
            return LineManager.GetVisibleLine(lineNumber);
        }

//		public int GetVisibleColumn(int logicalLine, int logicalColumn)
//		{
//			return lineTrackingStrategy.GetVisibleColumn(logicalLine, logicalColumn);
//		}
//
        public int GetNextVisibleLineAbove(int lineNumber, int lineCount)
        {
            return LineManager.GetNextVisibleLineAbove(lineNumber, lineCount);
        }

        public int GetNextVisibleLineBelow(int lineNumber, int lineCount)
        {
            return LineManager.GetNextVisibleLineBelow(lineNumber, lineCount);
        }

        public TextLocation OffsetToPosition(int offset)
        {
            var lineNr = GetLineNumberForOffset(offset);
            var line = GetLineSegment(lineNr);
            return new TextLocation(offset - line.Offset, lineNr);
        }

        public int PositionToOffset(TextLocation p)
        {
            if (p.Y >= TotalNumberOfLines) return 0;
            var line = GetLineSegment(p.Y);
            return Math.Min(TextLength, line.Offset + Math.Min(line.Length, p.X));
        }

        public void UpdateSegmentListOnDocumentChange<T>(List<T> list, DocumentEventArgs e) where T : ISegment
        {
            var removedCharacters = e.Length > 0 ? e.Length : 0;
            var insertedCharacters = e.Text != null ? e.Text.Length : 0;
            for (var i = 0; i < list.Count; ++i)
            {
                ISegment s = list[i];
                var segmentStart = s.Offset;
                var segmentEnd = s.Offset + s.Length;

                if (e.Offset <= segmentStart)
                {
                    segmentStart -= removedCharacters;
                    if (segmentStart < e.Offset)
                        segmentStart = e.Offset;
                }

                if (e.Offset < segmentEnd)
                {
                    segmentEnd -= removedCharacters;
                    if (segmentEnd < e.Offset)
                        segmentEnd = e.Offset;
                }

                Debug.Assert(segmentStart <= segmentEnd);

                if (segmentStart == segmentEnd)
                {
                    list.RemoveAt(i);
                    --i;
                    continue;
                }

                if (e.Offset <= segmentStart)
                    segmentStart += insertedCharacters;
                if (e.Offset < segmentEnd)
                    segmentEnd += insertedCharacters;

                Debug.Assert(segmentStart < segmentEnd);

                s.Offset = segmentStart;
                s.Length = segmentEnd - segmentStart;
            }
        }

        public event DocumentEventHandler DocumentAboutToBeChanged;
        public event DocumentEventHandler DocumentChanged;

        public List<TextAreaUpdate> UpdateQueue { get; } = new List<TextAreaUpdate>();

        public void RequestUpdate(TextAreaUpdate update)
        {
            if (UpdateQueue.Count == 1 && UpdateQueue[0].TextAreaUpdateType == TextAreaUpdateType.WholeTextArea
            ) // if we're going to update the whole text area, we don't need to store detail updates
                return;
            if (update.TextAreaUpdateType == TextAreaUpdateType.WholeTextArea
            ) // if we're going to update the whole text area, we don't need to store detail updates
                UpdateQueue.Clear();
            UpdateQueue.Add(update);
        }

        public void CommitUpdate()
        {
            if (UpdateCommited != null) UpdateCommited(this, EventArgs.Empty);
        }

        public event EventHandler UpdateCommited;
        public event EventHandler TextContentChanged;

        private void OnDocumentAboutToBeChanged(DocumentEventArgs e)
        {
            if (DocumentAboutToBeChanged != null) DocumentAboutToBeChanged(this, e);
        }

        private void OnDocumentChanged(DocumentEventArgs e)
        {
            if (DocumentChanged != null) DocumentChanged(this, e);
        }

        private void OnTextContentChanged(EventArgs e)
        {
            if (TextContentChanged != null) TextContentChanged(this, e);
        }

        [Conditional("DEBUG")]
        internal static void ValidatePosition(IDocument document, TextLocation position)
        {
            document.GetLineSegment(position.Line);
        }
    }
}