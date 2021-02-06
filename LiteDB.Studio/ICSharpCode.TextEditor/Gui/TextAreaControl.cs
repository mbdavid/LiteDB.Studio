// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.Selection;
using LiteDB.Studio.ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    /// <summary>
    ///     This class paints the textarea.
    /// </summary>
    [ToolboxItem(false)]
    public class TextAreaControl : Panel
    {
        private const int LineLengthCacheAdditionalSize = 100;

        private readonly MouseWheelHandler mouseWheelHandler = new MouseWheelHandler();

        private readonly int scrollMarginHeight = 3;

        private bool adjustScrollBarsOnNextUpdate;
        private bool autoHideScrollbars = true;
        private bool disposed;

        private HRuler hRuler;

        private int[] lineLengthCache;
        private TextEditorControl motherTextEditorControl;
        private Point scrollToPosOnNextUpdate;

        public TextAreaControl(TextEditorControl motherTextEditorControl)
        {
            this.motherTextEditorControl = motherTextEditorControl;

            TextArea = new TextArea(motherTextEditorControl, this);
            Controls.Add(TextArea);

            VScrollBar.ValueChanged += VScrollBarValueChanged;
            Controls.Add(VScrollBar);

            HScrollBar.ValueChanged += HScrollBarValueChanged;
            Controls.Add(HScrollBar);
            ResizeRedraw = true;
            AutoHideScrollbars = true;

            Document.TextContentChanged += DocumentTextContentChanged;
            Document.DocumentChanged += AdjustScrollBarsOnDocumentChange;
            Document.UpdateCommited += DocumentUpdateCommitted;

            ContextMenuStrip = new ContextMenu(this);
        }

        public TextArea TextArea { get; }

        public SelectionManager SelectionManager => TextArea.SelectionManager;

        public Caret Caret => TextArea.Caret;

        [Browsable(false)]
        public IDocument Document
        {
            get
            {
                if (motherTextEditorControl != null)
                    return motherTextEditorControl.Document;
                return null;
            }
        }

        public ITextEditorProperties TextEditorProperties
        {
            get
            {
                if (motherTextEditorControl != null)
                    return motherTextEditorControl.TextEditorProperties;
                return null;
            }
        }

        public VScrollBar VScrollBar { get; private set; } = new VScrollBar();

        public HScrollBar HScrollBar { get; private set; } = new HScrollBar();

        public bool AutoHideScrollbars
        {
            get => autoHideScrollbars;
            set
            {
                autoHideScrollbars = value;
                AdjustScrollBars();
            }
        }

        public bool DoHandleMousewheel { get; set; } = true;

        public void Undo()
        {
            motherTextEditorControl.Undo();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (!disposed)
                {
                    disposed = true;
                    Document.TextContentChanged -= DocumentTextContentChanged;
                    Document.DocumentChanged -= AdjustScrollBarsOnDocumentChange;
                    Document.UpdateCommited -= DocumentUpdateCommitted;
                    motherTextEditorControl = null;
                    if (VScrollBar != null)
                    {
                        VScrollBar.Dispose();
                        VScrollBar = null;
                    }

                    if (HScrollBar != null)
                    {
                        HScrollBar.Dispose();
                        HScrollBar = null;
                    }

                    if (hRuler != null)
                    {
                        hRuler.Dispose();
                        hRuler = null;
                    }
                }

            base.Dispose(disposing);
        }

        private void DocumentTextContentChanged(object sender, EventArgs e)
        {
            // after the text content is changed abruptly, we need to validate the
            // caret position - otherwise the caret position is invalid for a short amount
            // of time, which can break client code that expects that the caret position is always valid
            Caret.ValidateCaretPos();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeTextArea();
        }

        public void ResizeTextArea()
        {
            var y = 0;
            var h = 0;
            if (hRuler != null)
            {
                hRuler.Bounds = new Rectangle(0,
                    0,
                    Width - SystemInformation.HorizontalScrollBarArrowWidth,
                    TextArea.TextView.FontHeight);

                y = hRuler.Bounds.Bottom;
                h = hRuler.Bounds.Height;
            }

            var vScrollBarWidth = VScrollBar.Visible ? SystemInformation.HorizontalScrollBarArrowWidth : 0;
            var hScrollBarHeight = HScrollBar.Visible ? SystemInformation.VerticalScrollBarArrowHeight : 0;
            TextArea.Bounds = new Rectangle(0, y,
                Width - vScrollBarWidth,
                Height - hScrollBarHeight - h);
            SetScrollBarBounds();
        }

        public void SetScrollBarBounds()
        {
            var vHeight = Height - (HScrollBar.Visible ? SystemInformation.VerticalScrollBarArrowHeight : 0);
            VScrollBar.Bounds = new Rectangle(TextArea.Bounds.Right, 0, SystemInformation.HorizontalScrollBarArrowWidth,
                vHeight);
            HScrollBar.Bounds = new Rectangle(0,
                TextArea.Bounds.Bottom,
                Width - SystemInformation.HorizontalScrollBarArrowWidth,
                SystemInformation.VerticalScrollBarArrowHeight);
        }

        private void AdjustScrollBarsOnDocumentChange(object sender, DocumentEventArgs e)
        {
            if (motherTextEditorControl.IsInUpdate == false)
            {
                AdjustScrollBarsClearCache();
                AdjustScrollBars();
            }
            else
            {
                adjustScrollBarsOnNextUpdate = true;
            }
        }

        private void DocumentUpdateCommitted(object sender, EventArgs e)
        {
            if (motherTextEditorControl.IsInUpdate == false)
            {
                Caret.ValidateCaretPos();

                // AdjustScrollBarsOnCommittedUpdate
                if (!scrollToPosOnNextUpdate.IsEmpty) ScrollTo(scrollToPosOnNextUpdate.Y, scrollToPosOnNextUpdate.X);
                if (adjustScrollBarsOnNextUpdate)
                {
                    AdjustScrollBarsClearCache();
                    AdjustScrollBars();
                }
            }
        }

        private void AdjustScrollBarsClearCache()
        {
            if (lineLengthCache != null)
            {
                if (lineLengthCache.Length < Document.TotalNumberOfLines + 2 * LineLengthCacheAdditionalSize)
                    lineLengthCache = null;
                else
                    Array.Clear(lineLengthCache, 0, lineLengthCache.Length);
            }
        }

        public void AdjustScrollBars()
        {
            if (TextArea == null)
                return;

            adjustScrollBarsOnNextUpdate = false;
            VScrollBar.Minimum = 0;
            // number of visible lines in document (folding!)
            VScrollBar.Maximum = TextArea.MaxVScrollValue;
            var max = 0;

            var firstLine = TextArea.TextView.FirstVisibleLine;
            var lastLine =
                Document.GetFirstLogicalLine(TextArea.TextView.FirstPhysicalLine + TextArea.TextView.VisibleLineCount);
            if (lastLine >= Document.TotalNumberOfLines)
                lastLine = Document.TotalNumberOfLines - 1;

            if (lineLengthCache == null || lineLengthCache.Length <= lastLine)
                lineLengthCache = new int[lastLine + LineLengthCacheAdditionalSize];

            for (var lineNumber = firstLine; lineNumber <= lastLine; lineNumber++)
            {
                var lineSegment = Document.GetLineSegment(lineNumber);
                if (Document.FoldingManager.IsLineVisible(lineNumber))
                {
                    if (lineLengthCache[lineNumber] > 0)
                    {
                        max = Math.Max(max, lineLengthCache[lineNumber]);
                    }
                    else
                    {
                        var visualLength = TextArea.TextView.GetVisualColumnFast(lineSegment, lineSegment.Length);
                        lineLengthCache[lineNumber] = Math.Max(1, visualLength);
                        max = Math.Max(max, visualLength);
                    }
                }
            }

            HScrollBar.Minimum = 0;
            HScrollBar.Maximum = Math.Max(max + 20, TextArea.TextView.VisibleColumnCount - 1);

            VScrollBar.LargeChange = Math.Max(0, TextArea.TextView.DrawingPosition.Height);
            VScrollBar.SmallChange = Math.Max(0, TextArea.TextView.FontHeight);

            HScrollBar.LargeChange = Math.Max(0, TextArea.TextView.VisibleColumnCount - 1);
            HScrollBar.SmallChange = Math.Max(0, TextArea.TextView.SpaceWidth);

            if (AutoHideScrollbars)
            {
                var vVisible = Document.TotalNumberOfLines > TextArea.TextView.VisibleLineCount - 1;
                var hVisible = max > TextArea.TextView.VisibleColumnCount;
                var changed = VScrollBar.Visible != vVisible || HScrollBar.Visible != hVisible;

                VScrollBar.Visible = vVisible;
                HScrollBar.Visible = hVisible;
                if (changed)
                    ResizeTextArea();
            }
            else
            {
                VScrollBar.Visible = true;
                HScrollBar.Visible = true;
            }
        }

        public void OptionsChanged()
        {
            TextArea.OptionsChanged();

            if (TextArea.TextEditorProperties.ShowHorizontalRuler)
            {
                if (hRuler == null)
                {
                    hRuler = new HRuler(TextArea);
                    Controls.Add(hRuler);
                    ResizeTextArea();
                }
                else
                {
                    hRuler.Invalidate();
                }
            }
            else
            {
                if (hRuler != null)
                {
                    Controls.Remove(hRuler);
                    hRuler.Dispose();
                    hRuler = null;
                    ResizeTextArea();
                }
            }

            AdjustScrollBars();
        }

        private void VScrollBarValueChanged(object sender, EventArgs e)
        {
            TextArea.VirtualTop = new Point(TextArea.VirtualTop.X, VScrollBar.Value);
            TextArea.Invalidate();
            AdjustScrollBars();
        }

        private void HScrollBarValueChanged(object sender, EventArgs e)
        {
            TextArea.VirtualTop = new Point(HScrollBar.Value * TextArea.TextView.WideSpaceWidth, TextArea.VirtualTop.Y);
            TextArea.Invalidate();
        }

        public void HandleMouseWheel(MouseEventArgs e)
        {
            var scrollDistance = mouseWheelHandler.GetScrollAmount(e);
            if (scrollDistance == 0)
                return;
            if ((ModifierKeys & Keys.Control) != 0 && TextEditorProperties.MouseWheelTextZoom)
            {
                if (scrollDistance > 0)
                    motherTextEditorControl.Font = new Font(motherTextEditorControl.Font.Name,
                        motherTextEditorControl.Font.Size + 1);
                else
                    motherTextEditorControl.Font = new Font(motherTextEditorControl.Font.Name,
                        Math.Max(6, motherTextEditorControl.Font.Size - 1));
            }
            else
            {
                if (TextEditorProperties.MouseWheelScrollDown)
                    scrollDistance = -scrollDistance;
                var newValue = VScrollBar.Value + VScrollBar.SmallChange * scrollDistance;
                VScrollBar.Value = Math.Max(VScrollBar.Minimum,
                    Math.Min(VScrollBar.Maximum - VScrollBar.LargeChange + 1, newValue));
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (DoHandleMousewheel) HandleMouseWheel(e);
        }

        public void ScrollToCaret()
        {
            ScrollTo(TextArea.Caret.Line, TextArea.Caret.Column);
        }

        public void ScrollTo(int line, int column)
        {
            if (motherTextEditorControl.IsInUpdate)
            {
                scrollToPosOnNextUpdate = new Point(column, line);
                return;
            }

            scrollToPosOnNextUpdate = Point.Empty;

            ScrollTo(line);

            var curCharMin = HScrollBar.Value - HScrollBar.Minimum;
            var curCharMax = curCharMin + TextArea.TextView.VisibleColumnCount;

            var pos = TextArea.TextView.GetVisualColumn(line, column);

            if (TextArea.TextView.VisibleColumnCount < 0)
            {
                HScrollBar.Value = 0;
            }
            else
            {
                if (pos < curCharMin)
                {
                    HScrollBar.Value = Math.Max(0, pos - scrollMarginHeight);
                }
                else
                {
                    if (pos > curCharMax)
                        HScrollBar.Value = Math.Max(0,
                            Math.Min(HScrollBar.Maximum,
                                pos - TextArea.TextView.VisibleColumnCount + scrollMarginHeight));
                }
            }
        }

        /// <summary>
        ///     Ensure that <paramref name="line" /> is visible.
        /// </summary>
        public void ScrollTo(int line)
        {
            line = Math.Max(0, Math.Min(Document.TotalNumberOfLines - 1, line));
            line = Document.GetVisibleLine(line);
            var curLineMin = TextArea.TextView.FirstPhysicalLine;
            if (TextArea.TextView.LineHeightRemainder > 0) curLineMin++;

            if (line - scrollMarginHeight + 3 < curLineMin)
            {
                VScrollBar.Value = Math.Max(0,
                    Math.Min(VScrollBar.Maximum, (line - scrollMarginHeight + 3) * TextArea.TextView.FontHeight));
                VScrollBarValueChanged(this, EventArgs.Empty);
            }
            else
            {
                var curLineMax = curLineMin + TextArea.TextView.VisibleLineCount;
                if (line + scrollMarginHeight - 1 > curLineMax)
                {
                    if (TextArea.TextView.VisibleLineCount == 1)
                        VScrollBar.Value = Math.Max(0,
                            Math.Min(VScrollBar.Maximum,
                                (line - scrollMarginHeight - 1) * TextArea.TextView.FontHeight));
                    else
                        VScrollBar.Value = Math.Min(VScrollBar.Maximum,
                            (line - TextArea.TextView.VisibleLineCount + scrollMarginHeight - 1) *
                            TextArea.TextView.FontHeight);
                    VScrollBarValueChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Scroll so that the specified line is centered.
        /// </summary>
        /// <param name="line">Line to center view on</param>
        /// <param name="treshold">
        ///     If this action would cause scrolling by less than or equal to
        ///     <paramref name="treshold" /> lines in any direction, don't scroll.
        ///     Use -1 to always center the view.
        /// </param>
        public void CenterViewOn(int line, int treshold)
        {
            line = Math.Max(0, Math.Min(Document.TotalNumberOfLines - 1, line));
            // convert line to visible line:
            line = Document.GetVisibleLine(line);
            // subtract half the visible line count
            line -= TextArea.TextView.VisibleLineCount / 2;

            var curLineMin = TextArea.TextView.FirstPhysicalLine;
            if (TextArea.TextView.LineHeightRemainder > 0) curLineMin++;
            if (Math.Abs(curLineMin - line) > treshold)
            {
                // scroll:
                VScrollBar.Value = Math.Max(0,
                    Math.Min(VScrollBar.Maximum, (line - scrollMarginHeight + 3) * TextArea.TextView.FontHeight));
                VScrollBarValueChanged(this, EventArgs.Empty);
            }
        }

        public void JumpTo(int line)
        {
            line = Math.Max(0, Math.Min(line, Document.TotalNumberOfLines - 1));
            var text = Document.GetText(Document.GetLineSegment(line));
            JumpTo(line, text.Length - text.TrimStart().Length);
        }

        public void JumpTo(int line, int column, bool dontFocus = false)
        {
            if (!dontFocus)
                TextArea.Focus();
            TextArea.SelectionManager.ClearSelection();
            TextArea.Caret.Position = new TextLocation(column, line);
            TextArea.SetDesiredColumn();
            ScrollToCaret();
        }

        public event MouseEventHandler ShowContextMenu;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x007B) // handle WM_CONTEXTMENU
                if (ShowContextMenu != null)
                {
                    var lParam = m.LParam.ToInt64();
                    int x = unchecked((short) (lParam & 0xffff));
                    int y = unchecked((short) ((lParam & 0xffff0000) >> 16));
                    if (x == -1 && y == -1)
                    {
                        var pos = Caret.ScreenPosition;
                        ShowContextMenu(this,
                            new MouseEventArgs(MouseButtons.None, 0, pos.X, pos.Y + TextArea.TextView.FontHeight, 0));
                    }
                    else
                    {
                        var pos = PointToClient(new Point(x, y));
                        ShowContextMenu(this, new MouseEventArgs(MouseButtons.Right, 1, pos.X, pos.Y, 0));
                    }
                }

            base.WndProc(ref m);
        }

        protected override void OnEnter(EventArgs e)
        {
            // SD2-1072 - Make sure the caret line is valid if anyone
            // has handlers for the Enter event.
            Caret.ValidateCaretPos();
            base.OnEnter(e);
        }
    }
}