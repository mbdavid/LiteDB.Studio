// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Drawing;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui.CompletionWindow
{
    public interface IDeclarationViewWindow
    {
        string Description { get; set; }

        void ShowDeclarationViewWindow();
        void CloseDeclarationViewWindow();
    }

    public class DeclarationViewWindow : Form, IDeclarationViewWindow
    {
        private string description = string.Empty;

        public bool HideOnClick;

        public DeclarationViewWindow(Form parent)
        {
            SetStyle(ControlStyles.Selectable, false);
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Owner = parent;
            ShowInTaskbar = false;
            Size = new Size(0, 0);
            base.CreateHandle();
        }

        public bool FixedWidth { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                var p = base.CreateParams;
                AbstractCompletionWindow.AddShadowToWindow(p);
                return p;
            }
        }

        protected override bool ShowWithoutActivation => true;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                if (value == null && Visible)
                {
                    Visible = false;
                }
                else if (value != null)
                {
                    if (!Visible) ShowDeclarationViewWindow();
                    Refresh();
                }
            }
        }

        public void ShowDeclarationViewWindow()
        {
            Show();
        }

        public void CloseDeclarationViewWindow()
        {
            Close();
            Dispose();
        }

        public int GetRequiredLeftHandSideWidth(Point p)
        {
            if (description != null && description.Length > 0)
                using (var g = CreateGraphics())
                {
                    var s = TipPainterTools.GetLeftHandSideDrawingSizeHelpTipFromCombinedDescription(this, g, Font,
                        null, description, p);
                    return s.Width;
                }

            return 0;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (HideOnClick) Hide();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (description != null && description.Length > 0)
            {
                if (FixedWidth)
                    TipPainterTools.DrawFixedWidthHelpTipFromCombinedDescription(this, pe.Graphics, Font, null,
                        description);
                else
                    TipPainterTools.DrawHelpTipFromCombinedDescription(this, pe.Graphics, Font, null, description);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(SystemBrushes.Info, pe.ClipRectangle);
        }
    }
}