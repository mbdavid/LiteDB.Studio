// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Drawing;
using System.Windows.Forms;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui.CompletionWindow
{
    /// <summary>
    ///     Description of AbstractCompletionWindow.
    /// </summary>
    public abstract class AbstractCompletionWindow : Form
    {
        private static int shadowStatus;
        protected TextEditorControl control;
        protected Size drawingSize;
        private readonly Form parentForm;
        private Rectangle workingScreen;

        protected AbstractCompletionWindow(Form parentForm, TextEditorControl control)
        {
            workingScreen = Screen.GetWorkingArea(parentForm);
//			SetStyle(ControlStyles.Selectable, false);
            this.parentForm = parentForm;
            this.control = control;

            SetLocation();
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            MinimumSize = new Size(1, 1);
            Size = new Size(1, 1);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var p = base.CreateParams;
                AddShadowToWindow(p);
                return p;
            }
        }

        protected override bool ShowWithoutActivation => true;

        protected virtual void SetLocation()
        {
            var textArea = control.ActiveTextAreaControl.TextArea;
            var caretPos = textArea.Caret.Position;

            var xpos = textArea.TextView.GetDrawingXPos(caretPos.Y, caretPos.X);
            var rulerHeight = textArea.TextEditorProperties.ShowHorizontalRuler ? textArea.TextView.FontHeight : 0;
            var pos = new Point(textArea.TextView.DrawingPosition.X + xpos,
                textArea.TextView.DrawingPosition.Y +
                textArea.Document.GetVisibleLine(caretPos.Y) * textArea.TextView.FontHeight
                - textArea.TextView.TextArea.VirtualTop.Y + textArea.TextView.FontHeight + rulerHeight);

            var location = control.ActiveTextAreaControl.PointToScreen(pos);

            // set bounds
            var bounds = new Rectangle(location, drawingSize);

            if (!workingScreen.Contains(bounds))
            {
                if (bounds.Right > workingScreen.Right) bounds.X = workingScreen.Right - bounds.Width;
                if (bounds.Left < workingScreen.Left) bounds.X = workingScreen.Left;
                if (bounds.Top < workingScreen.Top) bounds.Y = workingScreen.Top;
                if (bounds.Bottom > workingScreen.Bottom)
                {
                    bounds.Y = bounds.Y - bounds.Height - control.ActiveTextAreaControl.TextArea.TextView.FontHeight;
                    if (bounds.Bottom > workingScreen.Bottom) bounds.Y = workingScreen.Bottom - bounds.Height;
                }
            }

            Bounds = bounds;
        }

        /// <summary>
        ///     Adds a shadow to the create params if it is supported by the operating system.
        /// </summary>
        public static void AddShadowToWindow(CreateParams createParams)
        {
            if (shadowStatus == 0)
            {
                // Test OS version
                shadowStatus = -1; // shadow not supported
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    var ver = Environment.OSVersion.Version;
                    if (ver.Major > 5 || ver.Major == 5 && ver.Minor >= 1) shadowStatus = 1;
                }
            }

            if (shadowStatus == 1) createParams.ClassStyle |= 0x00020000; // set CS_DROPSHADOW
        }

        protected void ShowCompletionWindow()
        {
            Owner = parentForm;
            Enabled = true;
            Show();

            control.Focus();

            if (parentForm != null) parentForm.LocationChanged += ParentFormLocationChanged;

            control.ActiveTextAreaControl.VScrollBar.ValueChanged += ParentFormLocationChanged;
            control.ActiveTextAreaControl.HScrollBar.ValueChanged += ParentFormLocationChanged;
            control.ActiveTextAreaControl.TextArea.DoProcessDialogKey += ProcessTextAreaKey;
            control.ActiveTextAreaControl.Caret.PositionChanged += CaretOffsetChanged;
            control.ActiveTextAreaControl.TextArea.LostFocus += TextEditorLostFocus;
            control.Resize += ParentFormLocationChanged;

            foreach (Control c in Controls) c.MouseMove += ControlMouseMove;
        }

        private void ParentFormLocationChanged(object sender, EventArgs e)
        {
            SetLocation();
        }

        public virtual bool ProcessKeyEvent(char ch)
        {
            return false;
        }

        protected virtual bool ProcessTextAreaKey(Keys keyData)
        {
            if (!Visible) return false;
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    return true;
            }

            return false;
        }

        protected virtual void CaretOffsetChanged(object sender, EventArgs e)
        {
        }

        protected void TextEditorLostFocus(object sender, EventArgs e)
        {
            if (!control.ActiveTextAreaControl.TextArea.Focused && !ContainsFocus) Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // take out the inserted methods
            parentForm.LocationChanged -= ParentFormLocationChanged;

            foreach (Control c in Controls) c.MouseMove -= ControlMouseMove;

            if (control.ActiveTextAreaControl.VScrollBar != null)
                control.ActiveTextAreaControl.VScrollBar.ValueChanged -= ParentFormLocationChanged;
            if (control.ActiveTextAreaControl.HScrollBar != null)
                control.ActiveTextAreaControl.HScrollBar.ValueChanged -= ParentFormLocationChanged;

            control.ActiveTextAreaControl.TextArea.LostFocus -= TextEditorLostFocus;
            control.ActiveTextAreaControl.Caret.PositionChanged -= CaretOffsetChanged;
            control.ActiveTextAreaControl.TextArea.DoProcessDialogKey -= ProcessTextAreaKey;
            control.Resize -= ParentFormLocationChanged;
            Dispose();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ControlMouseMove(this, e);
        }

        /// <summary>
        ///     Invoked when the mouse moves over this form or any child control.
        ///     Shows the mouse cursor on the text area if it has been hidden.
        /// </summary>
        /// <remarks>
        ///     Derived classes should attach this handler to the MouseMove event
        ///     of all created controls which are not added to the Controls
        ///     collection.
        /// </remarks>
        protected void ControlMouseMove(object sender, MouseEventArgs e)
        {
            control.ActiveTextAreaControl.TextArea.ShowHiddenCursor(false);
        }
    }
}