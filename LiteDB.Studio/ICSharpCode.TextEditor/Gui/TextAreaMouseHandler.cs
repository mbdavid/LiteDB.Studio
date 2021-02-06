// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.Selection;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    /// <summary>
    ///     This class handles all mouse stuff for a textArea.
    /// </summary>
    public class TextAreaMouseHandler
    {
        private static readonly Point nilPoint = new Point(-1, -1);
        private readonly TextArea textArea;

        private MouseButtons button;
        private bool clickedOnSelectedText;
        private bool dodragdrop;
        private bool doubleclick;

        private bool gotmousedown;
        private Point lastmousedownpos = nilPoint;
        private TextLocation maxSelection = TextLocation.Empty;
        private TextLocation minSelection = TextLocation.Empty;
        private Point mousedownpos = nilPoint;

        public TextAreaMouseHandler(TextArea ttextArea)
        {
            textArea = ttextArea;
        }

        public void Attach()
        {
            textArea.Click += TextAreaClick;
            textArea.MouseMove += TextAreaMouseMove;

            textArea.MouseDown += OnMouseDown;
            textArea.DoubleClick += OnDoubleClick;
            textArea.MouseLeave += OnMouseLeave;
            textArea.MouseUp += OnMouseUp;
            textArea.LostFocus += TextAreaLostFocus;
            textArea.ToolTipRequest += OnToolTipRequest;
        }

        private void OnToolTipRequest(object sender, ToolTipRequestEventArgs e)
        {
            if (e.ToolTipShown)
                return;
            var mousepos = e.MousePosition;
            var marker = textArea.TextView.GetFoldMarkerFromPosition(mousepos.X - textArea.TextView.DrawingPosition.X,
                mousepos.Y - textArea.TextView.DrawingPosition.Y);
            if (marker != null && marker.IsFolded)
            {
                var sb = new StringBuilder(marker.InnerText);

                // max 10 lines
                var endLines = 0;
                for (var i = 0; i < sb.Length; ++i)
                    if (sb[i] == '\n')
                    {
                        ++endLines;
                        if (endLines >= 10)
                        {
                            sb.Remove(i + 1, sb.Length - i - 1);
                            sb.Append(Environment.NewLine);
                            sb.Append("...");
                            break;
                        }
                    }

                sb.Replace("\t", "    ");
                e.ShowToolTip(sb.ToString());
                return;
            }

            var markers = textArea.Document.MarkerStrategy.GetMarkers(e.LogicalPosition);
            foreach (var tm in markers)
                if (tm.ToolTip != null)
                {
                    e.ShowToolTip(tm.ToolTip.Replace("\t", "    "));
                    return;
                }
        }

        private void ShowHiddenCursorIfMovedOrLeft()
        {
            textArea.ShowHiddenCursor(!textArea.Focused ||
                                      !textArea.ClientRectangle.Contains(textArea.PointToClient(Cursor.Position)));
        }

        private void TextAreaLostFocus(object sender, EventArgs e)
        {
            // The call to ShowHiddenCursorIfMovedOrLeft is delayed
            // until pending messages have been processed
            // so that it can properly detect whether the TextArea
            // has really lost focus.
            // For example, the CodeCompletionWindow gets focus when it is shown,
            // but immediately gives back focus to the TextArea.
            textArea.BeginInvoke(new MethodInvoker(ShowHiddenCursorIfMovedOrLeft));
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            ShowHiddenCursorIfMovedOrLeft();
            gotmousedown = false;
            mousedownpos = nilPoint;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            textArea.SelectionManager.selectFrom.where = WhereFrom.None;
            gotmousedown = false;
            mousedownpos = nilPoint;
        }

        private void TextAreaClick(object sender, EventArgs e)
        {
            Point mousepos;
            mousepos = textArea.mousepos;

            if (dodragdrop) return;

            if (clickedOnSelectedText && textArea.TextView.DrawingPosition.Contains(mousepos.X, mousepos.Y))
            {
                textArea.SelectionManager.ClearSelection();

                var clickPosition = textArea.TextView.GetLogicalPosition(
                    mousepos.X - textArea.TextView.DrawingPosition.X,
                    mousepos.Y - textArea.TextView.DrawingPosition.Y);
                textArea.Caret.Position = clickPosition;
                textArea.SetDesiredColumn();
            }
        }


        private void TextAreaMouseMove(object sender, MouseEventArgs e)
        {
            textArea.mousepos = e.Location;

            // honour the starting selection strategy
            switch (textArea.SelectionManager.selectFrom.where)
            {
                case WhereFrom.Gutter:
                    ExtendSelectionToMouse();
                    return;

                case WhereFrom.TArea:
                    break;
            }

            textArea.ShowHiddenCursor(false);
            if (dodragdrop)
            {
                dodragdrop = false;
                return;
            }

            doubleclick = false;
            textArea.mousepos = new Point(e.X, e.Y);

            if (clickedOnSelectedText)
            {
                if (Math.Abs(mousedownpos.X - e.X) >= SystemInformation.DragSize.Width / 2 ||
                    Math.Abs(mousedownpos.Y - e.Y) >= SystemInformation.DragSize.Height / 2)
                {
                    clickedOnSelectedText = false;
                    var selection = textArea.SelectionManager.GetSelectionAt(textArea.Caret.Offset);
                    if (selection != null)
                    {
                        var text = selection.SelectedText;
                        var isReadOnly = SelectionManager.SelectionIsReadOnly(textArea.Document, selection);
                        if (text != null && text.Length > 0)
                        {
                            var dataObject = new DataObject();
                            dataObject.SetData(DataFormats.UnicodeText, true, text);
                            dataObject.SetData(selection);
                            dodragdrop = true;
                            textArea.DoDragDrop(dataObject,
                                isReadOnly ? DragDropEffects.All & ~DragDropEffects.Move : DragDropEffects.All);
                        }
                    }
                }

                return;
            }

            if (e.Button == MouseButtons.Left)
                if (gotmousedown && textArea.SelectionManager.selectFrom.where == WhereFrom.TArea)
                    ExtendSelectionToMouse();
        }

        private void ExtendSelectionToMouse()
        {
            Point mousepos;
            mousepos = textArea.mousepos;
            var realmousepos = textArea.TextView.GetLogicalPosition(
                Math.Max(0, mousepos.X - textArea.TextView.DrawingPosition.X),
                mousepos.Y - textArea.TextView.DrawingPosition.Y);
            var y = realmousepos.Y;
            realmousepos = textArea.Caret.ValidatePosition(realmousepos);
            var oldPos = textArea.Caret.Position;
            if (oldPos == realmousepos && textArea.SelectionManager.selectFrom.where != WhereFrom.Gutter) return;

            // the selection is from the gutter
            if (textArea.SelectionManager.selectFrom.where == WhereFrom.Gutter)
            {
                if (realmousepos.Y < textArea.SelectionManager.SelectionStart.Y
                ) // the selection has moved above the startpoint
                    textArea.Caret.Position = new TextLocation(0, realmousepos.Y);
                else // the selection has moved below the startpoint
                    textArea.Caret.Position = textArea.SelectionManager.NextValidPosition(realmousepos.Y);
            }
            else
            {
                textArea.Caret.Position = realmousepos;
            }

            // moves selection across whole words for double-click initiated selection
            if (!minSelection.IsEmpty && textArea.SelectionManager.SelectionCollection.Count > 0 &&
                textArea.SelectionManager.selectFrom.where == WhereFrom.TArea)
            {
                // Extend selection when selection was started with double-click
                var selection = textArea.SelectionManager.SelectionCollection[0];
                var min = textArea.SelectionManager.GreaterEqPos(minSelection, maxSelection)
                    ? maxSelection
                    : minSelection;
                var max = textArea.SelectionManager.GreaterEqPos(minSelection, maxSelection)
                    ? minSelection
                    : maxSelection;
                if (textArea.SelectionManager.GreaterEqPos(max, realmousepos) &&
                    textArea.SelectionManager.GreaterEqPos(realmousepos, min))
                {
                    textArea.SelectionManager.SetSelection(min, max);
                }
                else if (textArea.SelectionManager.GreaterEqPos(max, realmousepos))
                {
                    var moff = textArea.Document.PositionToOffset(realmousepos);
                    min = textArea.Document.OffsetToPosition(FindWordStart(textArea.Document, moff));
                    textArea.SelectionManager.SetSelection(min, max);
                }
                else
                {
                    var moff = textArea.Document.PositionToOffset(realmousepos);
                    max = textArea.Document.OffsetToPosition(FindWordEnd(textArea.Document, moff));
                    textArea.SelectionManager.SetSelection(min, max);
                }
            }
            else
            {
                textArea.SelectionManager.ExtendSelection(oldPos, textArea.Caret.Position);
            }

            textArea.SetDesiredColumn();
        }

        private void DoubleClickSelectionExtend()
        {
            Point mousepos;
            mousepos = textArea.mousepos;

            textArea.SelectionManager.ClearSelection();
            if (textArea.TextView.DrawingPosition.Contains(mousepos.X, mousepos.Y))
            {
                var marker = textArea.TextView.GetFoldMarkerFromPosition(
                    mousepos.X - textArea.TextView.DrawingPosition.X,
                    mousepos.Y - textArea.TextView.DrawingPosition.Y);
                if (marker != null && marker.IsFolded)
                {
                    marker.IsFolded = false;
                    textArea.MotherTextAreaControl.AdjustScrollBars();
                }

                if (textArea.Caret.Offset < textArea.Document.TextLength)
                {
                    switch (textArea.Document.GetCharAt(textArea.Caret.Offset))
                    {
                        case '"':
                            if (textArea.Caret.Offset < textArea.Document.TextLength)
                            {
                                var next = FindNext(textArea.Document, textArea.Caret.Offset + 1, '"');
                                minSelection = textArea.Caret.Position;
                                if (next > textArea.Caret.Offset && next < textArea.Document.TextLength)
                                    next += 1;
                                maxSelection = textArea.Document.OffsetToPosition(next);
                            }

                            break;
                        default:
                            minSelection =
                                textArea.Document.OffsetToPosition(FindWordStart(textArea.Document,
                                    textArea.Caret.Offset));
                            maxSelection =
                                textArea.Document.OffsetToPosition(
                                    FindWordEnd(textArea.Document, textArea.Caret.Offset));
                            break;
                    }

                    textArea.Caret.Position = maxSelection;
                    textArea.SelectionManager.ExtendSelection(minSelection, maxSelection);
                }

                if (textArea.SelectionManager.selectionCollection.Count > 0)
                {
                    var selection = textArea.SelectionManager.selectionCollection[0];

                    selection.StartPosition = minSelection;
                    selection.EndPosition = maxSelection;
                    textArea.SelectionManager.SelectionStart = minSelection;
                }

                // after a double-click selection, the caret is placed correctly,
                // but it is not positioned internally.  The effect is when the cursor
                // is moved up or down a line, the caret will take on the column first
                // clicked on for the double-click
                textArea.SetDesiredColumn();

                // HACK WARNING !!!
                // must refresh here, because when a error tooltip is showed and the underlined
                // code is double clicked the textArea don't update corrctly, updateline doesn't
                // work ... but the refresh does.
                // Mike
                textArea.Refresh();
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Point mousepos;
            textArea.mousepos = e.Location;
            mousepos = e.Location;

            if (dodragdrop) return;

            if (doubleclick)
            {
                doubleclick = false;
                return;
            }

            if (textArea.TextView.DrawingPosition.Contains(mousepos.X, mousepos.Y))
            {
                gotmousedown = true;
                textArea.SelectionManager.selectFrom.where = WhereFrom.TArea;
                button = e.Button;

                // double-click
                if (button == MouseButtons.Left && e.Clicks == 2)
                {
                    var deltaX = Math.Abs(lastmousedownpos.X - e.X);
                    var deltaY = Math.Abs(lastmousedownpos.Y - e.Y);
                    if (deltaX <= SystemInformation.DoubleClickSize.Width &&
                        deltaY <= SystemInformation.DoubleClickSize.Height)
                    {
                        DoubleClickSelectionExtend();
                        lastmousedownpos = new Point(e.X, e.Y);

                        if (textArea.SelectionManager.selectFrom.where == WhereFrom.Gutter)
                            if (!minSelection.IsEmpty && !maxSelection.IsEmpty &&
                                textArea.SelectionManager.SelectionCollection.Count > 0)
                            {
                                textArea.SelectionManager.SelectionCollection[0].StartPosition = minSelection;
                                textArea.SelectionManager.SelectionCollection[0].EndPosition = maxSelection;
                                textArea.SelectionManager.SelectionStart = minSelection;

                                minSelection = TextLocation.Empty;
                                maxSelection = TextLocation.Empty;
                            }

                        return;
                    }
                }

                minSelection = TextLocation.Empty;
                maxSelection = TextLocation.Empty;

                lastmousedownpos = mousedownpos = new Point(e.X, e.Y);

                if (button == MouseButtons.Left)
                {
                    var marker = textArea.TextView.GetFoldMarkerFromPosition(
                        mousepos.X - textArea.TextView.DrawingPosition.X,
                        mousepos.Y - textArea.TextView.DrawingPosition.Y);
                    if (marker != null && marker.IsFolded)
                    {
                        if (textArea.SelectionManager.HasSomethingSelected) clickedOnSelectedText = true;

                        var startLocation = new TextLocation(marker.StartColumn, marker.StartLine);
                        var endLocation = new TextLocation(marker.EndColumn, marker.EndLine);
                        textArea.SelectionManager.SetSelection(new DefaultSelection(textArea.TextView.Document,
                            startLocation, endLocation));
                        textArea.Caret.Position = startLocation;
                        textArea.SetDesiredColumn();
                        textArea.Focus();
                        return;
                    }

                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        ExtendSelectionToMouse();
                    }
                    else
                    {
                        var realmousepos = textArea.TextView.GetLogicalPosition(
                            mousepos.X - textArea.TextView.DrawingPosition.X,
                            mousepos.Y - textArea.TextView.DrawingPosition.Y);
                        clickedOnSelectedText = false;

                        var offset = textArea.Document.PositionToOffset(realmousepos);

                        if (textArea.SelectionManager.HasSomethingSelected &&
                            textArea.SelectionManager.IsSelected(offset))
                        {
                            clickedOnSelectedText = true;
                        }
                        else
                        {
                            textArea.SelectionManager.ClearSelection();
                            if (mousepos.Y > 0 && mousepos.Y < textArea.TextView.DrawingPosition.Height)
                            {
                                var pos = new TextLocation();
                                pos.Y = Math.Min(textArea.Document.TotalNumberOfLines - 1, realmousepos.Y);
                                pos.X = realmousepos.X;
                                textArea.Caret.Position = pos;
                                textArea.SetDesiredColumn();
                            }
                        }
                    }
                }
                else if (button == MouseButtons.Right)
                {
                    // Rightclick sets the cursor to the click position unless
                    // the previous selection was clicked
                    var realmousepos = textArea.TextView.GetLogicalPosition(
                        mousepos.X - textArea.TextView.DrawingPosition.X,
                        mousepos.Y - textArea.TextView.DrawingPosition.Y);
                    var offset = textArea.Document.PositionToOffset(realmousepos);
                    if (!textArea.SelectionManager.HasSomethingSelected ||
                        !textArea.SelectionManager.IsSelected(offset))
                    {
                        textArea.SelectionManager.ClearSelection();
                        if (mousepos.Y > 0 && mousepos.Y < textArea.TextView.DrawingPosition.Height)
                        {
                            var pos = new TextLocation();
                            pos.Y = Math.Min(textArea.Document.TotalNumberOfLines - 1, realmousepos.Y);
                            pos.X = realmousepos.X;
                            textArea.Caret.Position = pos;
                            textArea.SetDesiredColumn();
                        }
                    }
                }
            }

            textArea.Focus();
        }

        private int FindNext(IDocument document, int offset, char ch)
        {
            var line = document.GetLineSegmentForOffset(offset);
            var endPos = line.Offset + line.Length;

            while (offset < endPos && document.GetCharAt(offset) != ch) ++offset;
            return offset;
        }

        private bool IsSelectableChar(char ch)
        {
            return char.IsLetterOrDigit(ch) || ch == '_';
        }

        private int FindWordStart(IDocument document, int offset)
        {
            var line = document.GetLineSegmentForOffset(offset);

            if (offset > 0 && char.IsWhiteSpace(document.GetCharAt(offset - 1)) &&
                char.IsWhiteSpace(document.GetCharAt(offset)))
            {
                while (offset > line.Offset && char.IsWhiteSpace(document.GetCharAt(offset - 1))) --offset;
            }
            else if (IsSelectableChar(document.GetCharAt(offset)) || offset > 0 &&
                char.IsWhiteSpace(document.GetCharAt(offset)) && IsSelectableChar(document.GetCharAt(offset - 1)))
            {
                while (offset > line.Offset && IsSelectableChar(document.GetCharAt(offset - 1))) --offset;
            }
            else
            {
                if (offset > 0 && !char.IsWhiteSpace(document.GetCharAt(offset - 1)) &&
                    !IsSelectableChar(document.GetCharAt(offset - 1))) return Math.Max(0, offset - 1);
            }

            return offset;
        }

        private int FindWordEnd(IDocument document, int offset)
        {
            var line = document.GetLineSegmentForOffset(offset);
            if (line.Length == 0)
                return offset;
            var endPos = line.Offset + line.Length;
            offset = Math.Min(offset, endPos - 1);

            if (IsSelectableChar(document.GetCharAt(offset)))
            {
                while (offset < endPos && IsSelectableChar(document.GetCharAt(offset))) ++offset;
            }
            else if (char.IsWhiteSpace(document.GetCharAt(offset)))
            {
                if (offset > 0 && char.IsWhiteSpace(document.GetCharAt(offset - 1)))
                    while (offset < endPos && char.IsWhiteSpace(document.GetCharAt(offset)))
                        ++offset;
            }
            else
            {
                return Math.Max(0, offset + 1);
            }

            return offset;
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (dodragdrop) return;

            textArea.SelectionManager.selectFrom.where = WhereFrom.TArea;
            doubleclick = true;
        }
    }
}