// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    /// <summary>
    ///     In this enumeration are all caret modes listed.
    /// </summary>
    public enum CaretMode
    {
        /// <summary>
        ///     If the caret is in insert mode typed characters will be
        ///     inserted at the caret position
        /// </summary>
        InsertMode,

        /// <summary>
        ///     If the caret is in overwirte mode typed characters will
        ///     overwrite the character at the caret position
        /// </summary>
        OverwriteMode
    }


    public class Caret : IDisposable
    {
        private static bool caretCreated;
        private readonly CaretImplementation caretImplementation;
        private CaretMode caretMode;
        private int column;
        private Point currentPos = new Point(-1, -1);

        private bool firePositionChangedAfterUpdateEnd;
        private bool hidden = true;
        private Ime ime;
        private int line;
        private int oldLine = -1;
        private bool outstandingUpdate;
        private TextArea textArea;

        public Caret(TextArea textArea)
        {
            this.textArea = textArea;
            textArea.GotFocus += GotFocus;
            textArea.LostFocus += LostFocus;
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                caretImplementation = new ManagedCaret(this);
            else
                caretImplementation = new Win32Caret(this);
        }

        /// <value>
        ///     The 'prefered' xPos in which the caret moves, when it is moved
        ///     up/down. Measured in pixels, not in characters!
        /// </value>
        public int DesiredColumn { get; set; } = 0;

        /// <value>
        ///     The current caret mode.
        /// </value>
        public CaretMode CaretMode
        {
            get => caretMode;
            set
            {
                caretMode = value;
                OnCaretModeChanged(EventArgs.Empty);
            }
        }

        public int Line
        {
            get => line;
            set
            {
                line = value;
                ValidateCaretPos();
                UpdateCaretPosition();
                OnPositionChanged(EventArgs.Empty);
            }
        }

        public int Column
        {
            get => column;
            set
            {
                column = value;
                ValidateCaretPos();
                UpdateCaretPosition();
                OnPositionChanged(EventArgs.Empty);
            }
        }

        public TextLocation Position
        {
            get => new TextLocation(column, line);
            set
            {
                line = value.Y;
                column = value.X;
                ValidateCaretPos();
                UpdateCaretPosition();
                OnPositionChanged(EventArgs.Empty);
            }
        }

        public int Offset => textArea.Document.PositionToOffset(Position);

        public Point ScreenPosition
        {
            get
            {
                var xpos = textArea.TextView.GetDrawingXPos(line, column);
                return new Point(textArea.TextView.DrawingPosition.X + xpos,
                    textArea.TextView.DrawingPosition.Y
                    + textArea.Document.GetVisibleLine(line) * textArea.TextView.FontHeight
                    - textArea.TextView.TextArea.VirtualTop.Y);
            }
        }

        public void Dispose()
        {
            textArea.GotFocus -= GotFocus;
            textArea.LostFocus -= LostFocus;
            textArea = null;
            caretImplementation.Dispose();
        }

        public TextLocation ValidatePosition(TextLocation pos)
        {
            var line = Math.Max(0, Math.Min(textArea.Document.TotalNumberOfLines - 1, pos.Y));
            var column = Math.Max(0, pos.X);

            if (column == int.MaxValue || !textArea.TextEditorProperties.AllowCaretBeyondEOL)
            {
                var lineSegment = textArea.Document.GetLineSegment(line);
                column = Math.Min(column, lineSegment.Length);
            }

            return new TextLocation(column, line);
        }

        /// <remarks>
        ///     If the caret position is outside the document text bounds
        ///     it is set to the correct position by calling ValidateCaretPos.
        /// </remarks>
        public void ValidateCaretPos()
        {
            line = Math.Max(0, Math.Min(textArea.Document.TotalNumberOfLines - 1, line));
            column = Math.Max(0, column);

            if (column == int.MaxValue || !textArea.TextEditorProperties.AllowCaretBeyondEOL)
            {
                var lineSegment = textArea.Document.GetLineSegment(line);
                column = Math.Min(column, lineSegment.Length);
            }
        }

        private void CreateCaret()
        {
            while (!caretCreated)
                switch (caretMode)
                {
                    case CaretMode.InsertMode:
                        caretCreated = caretImplementation.Create(2, textArea.TextView.FontHeight);
                        break;
                    case CaretMode.OverwriteMode:
                        caretCreated =
                            caretImplementation.Create(textArea.TextView.SpaceWidth, textArea.TextView.FontHeight);
                        break;
                }

            if (currentPos.X < 0)
            {
                ValidateCaretPos();
                currentPos = ScreenPosition;
            }

            caretImplementation.SetPosition(currentPos.X, currentPos.Y);
            caretImplementation.Show();
        }

        public void RecreateCaret()
        {
            Log("RecreateCaret");
            DisposeCaret();
            if (!hidden) CreateCaret();
        }

        private void DisposeCaret()
        {
            if (caretCreated)
            {
                caretCreated = false;
                caretImplementation.Hide();
                caretImplementation.Destroy();
            }
        }

        private void GotFocus(object sender, EventArgs e)
        {
            Log("GotFocus, IsInUpdate=" + textArea.MotherTextEditorControl.IsInUpdate);
            hidden = false;
            if (!textArea.MotherTextEditorControl.IsInUpdate)
            {
                CreateCaret();
                UpdateCaretPosition();
            }
        }

        private void LostFocus(object sender, EventArgs e)
        {
            Log("LostFocus");
            hidden = true;
            DisposeCaret();
        }

        internal void OnEndUpdate()
        {
            if (outstandingUpdate)
                UpdateCaretPosition();
        }

        private void PaintCaretLine(Graphics g)
        {
            if (!textArea.Document.TextEditorProperties.CaretLine)
                return;

            var caretLineColor = textArea.Document.HighlightingStrategy.GetColorFor("CaretLine");

            g.DrawLine(BrushRegistry.GetDotPen(caretLineColor.Color),
                currentPos.X,
                0,
                currentPos.X,
                textArea.DisplayRectangle.Height);
        }

        public void UpdateCaretPosition()
        {
            Log("UpdateCaretPosition");

            if (textArea.TextEditorProperties.CaretLine)
            {
                textArea.Invalidate();
            }
            else
            {
                if (caretImplementation.RequireRedrawOnPositionChange)
                {
                    textArea.UpdateLine(oldLine);
                    if (line != oldLine)
                        textArea.UpdateLine(line);
                }
                else
                {
                    if (textArea.MotherTextAreaControl.TextEditorProperties.LineViewerStyle ==
                        LineViewerStyle.FullRow && oldLine != line)
                    {
                        textArea.UpdateLine(oldLine);
                        textArea.UpdateLine(line);
                    }
                }
            }

            oldLine = line;


            if (hidden || textArea.MotherTextEditorControl.IsInUpdate)
            {
                outstandingUpdate = true;
                return;
            }

            outstandingUpdate = false;
            ValidateCaretPos();
            var lineNr = line;
            var xpos = textArea.TextView.GetDrawingXPos(lineNr, column);
            //LineSegment lineSegment = textArea.Document.GetLineSegment(lineNr);
            var pos = ScreenPosition;
            if (xpos >= 0)
            {
                CreateCaret();
                var success = caretImplementation.SetPosition(pos.X, pos.Y);
                if (!success)
                {
                    caretImplementation.Destroy();
                    caretCreated = false;
                    UpdateCaretPosition();
                }
            }
            else
            {
                caretImplementation.Destroy();
            }

            // set the input method editor location
            if (ime == null)
            {
                ime = new Ime(textArea.Handle, textArea.Document.TextEditorProperties.Font);
            }
            else
            {
                ime.HWnd = textArea.Handle;
                ime.Font = textArea.Document.TextEditorProperties.Font;
            }

            ime.SetIMEWindowLocation(pos.X, pos.Y);

            currentPos = pos;
        }

        [Conditional("DEBUG")]
        private static void Log(string text)
        {
            //Console.WriteLine(text);
        }

        private void FirePositionChangedAfterUpdateEnd(object sender, EventArgs e)
        {
            OnPositionChanged(EventArgs.Empty);
        }

        protected virtual void OnPositionChanged(EventArgs e)
        {
            if (textArea.MotherTextEditorControl.IsInUpdate)
            {
                if (firePositionChangedAfterUpdateEnd == false)
                {
                    firePositionChangedAfterUpdateEnd = true;
                    textArea.Document.UpdateCommited += FirePositionChangedAfterUpdateEnd;
                }

                return;
            }

            if (firePositionChangedAfterUpdateEnd)
            {
                textArea.Document.UpdateCommited -= FirePositionChangedAfterUpdateEnd;
                firePositionChangedAfterUpdateEnd = false;
            }

            var foldings = textArea.Document.FoldingManager.GetFoldingsFromPosition(line, column);
            var shouldUpdate = false;
            foreach (var foldMarker in foldings)
            {
                shouldUpdate |= foldMarker.IsFolded;
                foldMarker.IsFolded = false;
            }

            if (shouldUpdate) textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);

            if (PositionChanged != null) PositionChanged(this, e);
            textArea.ScrollToCaret();
        }

        protected virtual void OnCaretModeChanged(EventArgs e)
        {
            if (CaretModeChanged != null) CaretModeChanged(this, e);
            caretImplementation.Hide();
            caretImplementation.Destroy();
            caretCreated = false;
            CreateCaret();
            caretImplementation.Show();
        }

        /// <remarks>
        ///     Is called each time the caret is moved.
        /// </remarks>
        public event EventHandler PositionChanged;

        /// <remarks>
        ///     Is called each time the CaretMode has changed.
        /// </remarks>
        public event EventHandler CaretModeChanged;

        #region Caret implementation

        internal void PaintCaret(Graphics g)
        {
            caretImplementation.PaintCaret(g);
            PaintCaretLine(g);
        }

        private abstract class CaretImplementation : IDisposable
        {
            public bool RequireRedrawOnPositionChange;

            public virtual void Dispose()
            {
                Destroy();
            }

            public abstract bool Create(int width, int height);
            public abstract void Hide();
            public abstract void Show();
            public abstract bool SetPosition(int x, int y);
            public abstract void PaintCaret(Graphics g);
            public abstract void Destroy();
        }

        private class ManagedCaret : CaretImplementation
        {
            private bool blink = true;
            private readonly Caret parentCaret;
            private readonly TextArea textArea;
            private readonly Timer timer = new Timer {Interval = 300};
            private bool visible;
            private int x, y, width, height;

            public ManagedCaret(Caret caret)
            {
                RequireRedrawOnPositionChange = true;
                textArea = caret.textArea;
                parentCaret = caret;
                timer.Tick += CaretTimerTick;
            }

            private void CaretTimerTick(object sender, EventArgs e)
            {
                blink = !blink;
                if (visible)
                    textArea.UpdateLine(parentCaret.Line);
            }

            public override bool Create(int width, int height)
            {
                visible = true;
                this.width = width - 2;
                this.height = height;
                timer.Enabled = true;
                return true;
            }

            public override void Hide()
            {
                visible = false;
            }

            public override void Show()
            {
                visible = true;
            }

            public override bool SetPosition(int x, int y)
            {
                this.x = x - 1;
                this.y = y;
                return true;
            }

            public override void PaintCaret(Graphics g)
            {
                if (visible && blink)
                    g.DrawRectangle(Pens.Gray, x, y, width, height);
            }

            public override void Destroy()
            {
                visible = false;
                timer.Enabled = false;
            }

            public override void Dispose()
            {
                base.Dispose();
                timer.Dispose();
            }
        }

        private class Win32Caret : CaretImplementation
        {
            private readonly TextArea textArea;

            public Win32Caret(Caret caret)
            {
                textArea = caret.textArea;
            }

            [DllImport("User32.dll")]
            private static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);

            [DllImport("User32.dll")]
            private static extern bool SetCaretPos(int x, int y);

            [DllImport("User32.dll")]
            private static extern bool DestroyCaret();

            [DllImport("User32.dll")]
            private static extern bool ShowCaret(IntPtr hWnd);

            [DllImport("User32.dll")]
            private static extern bool HideCaret(IntPtr hWnd);

            public override bool Create(int width, int height)
            {
                return CreateCaret(textArea.Handle, 0, width, height);
            }

            public override void Hide()
            {
                HideCaret(textArea.Handle);
            }

            public override void Show()
            {
                ShowCaret(textArea.Handle);
            }

            public override bool SetPosition(int x, int y)
            {
                return SetCaretPos(x, y);
            }

            public override void PaintCaret(Graphics g)
            {
            }

            public override void Destroy()
            {
                DestroyCaret();
            }
        }

        #endregion
    }
}