// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="none" email=""/>
//     <version>$Revision$</version>
// </file>

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    internal static class TipPainter
    {
        private const float HorizontalBorder = 2;
        private const float VerticalBorder = 1;

        //static StringFormat centerTipFormat = CreateTipStringFormat();

        public static Size GetTipSize(Control control, Graphics graphics, Font font, string description)
        {
            return GetTipSize(control, graphics, new TipText(graphics, font, description));
        }

        private static Rectangle GetWorkingArea(Control control)
        {
            var ownerForm = control.FindForm();
            if (ownerForm.Owner != null) ownerForm = ownerForm.Owner;

            return Screen.GetWorkingArea(ownerForm);
        }

        public static Size GetTipSize(Control control, Graphics graphics, TipSection tipData)
        {
            var tipSize = Size.Empty;
            var tipSizeF = SizeF.Empty;

            RectangleF workingArea = GetWorkingArea(control);

            PointF screenLocation = control.PointToScreen(Point.Empty);

            var maxLayoutSize = new SizeF(workingArea.Right - screenLocation.X - HorizontalBorder * 2,
                workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

            if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
            {
                graphics.TextRenderingHint =
                    TextRenderingHint.AntiAliasGridFit;

                tipData.SetMaximumSize(maxLayoutSize);
                tipSizeF = tipData.GetRequiredSize();
                tipData.SetAllocatedSize(tipSizeF);

                tipSizeF += new SizeF(HorizontalBorder * 2,
                    VerticalBorder * 2);
                tipSize = Size.Ceiling(tipSizeF);
            }

            if (control.ClientSize != tipSize) control.ClientSize = tipSize;

            return tipSize;
        }

        public static Size GetLeftHandSideTipSize(Control control, Graphics graphics, TipSection tipData, Point p)
        {
            var tipSize = Size.Empty;
            var tipSizeF = SizeF.Empty;

            RectangleF workingArea = GetWorkingArea(control);

            PointF screenLocation = p;

            var maxLayoutSize = new SizeF(screenLocation.X - HorizontalBorder * 2,
                workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

            if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
            {
                graphics.TextRenderingHint =
                    TextRenderingHint.AntiAliasGridFit;

                tipData.SetMaximumSize(maxLayoutSize);
                tipSizeF = tipData.GetRequiredSize();
                tipData.SetAllocatedSize(tipSizeF);

                tipSizeF += new SizeF(HorizontalBorder * 2,
                    VerticalBorder * 2);
                tipSize = Size.Ceiling(tipSizeF);
            }

            return tipSize;
        }

        public static Size DrawTip(Control control, Graphics graphics, Font font, string description)
        {
            return DrawTip(control, graphics, new TipText(graphics, font, description));
        }

        public static Size DrawTip(Control control, Graphics graphics, TipSection tipData)
        {
            var tipSize = Size.Empty;
            var tipSizeF = SizeF.Empty;

            PointF screenLocation = control.PointToScreen(Point.Empty);

            RectangleF workingArea = GetWorkingArea(control);

            var maxLayoutSize = new SizeF(workingArea.Right - screenLocation.X - HorizontalBorder * 2,
                workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

            if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
            {
                graphics.TextRenderingHint =
                    TextRenderingHint.AntiAliasGridFit;

                tipData.SetMaximumSize(maxLayoutSize);
                tipSizeF = tipData.GetRequiredSize();
                tipData.SetAllocatedSize(tipSizeF);

                tipSizeF += new SizeF(HorizontalBorder * 2,
                    VerticalBorder * 2);
                tipSize = Size.Ceiling(tipSizeF);
            }

            if (control.ClientSize != tipSize) control.ClientSize = tipSize;

            if (tipSize != Size.Empty)
            {
                var borderRectangle = new Rectangle
                    (Point.Empty, tipSize - new Size(1, 1));

                var displayRectangle = new RectangleF
                (HorizontalBorder, VerticalBorder,
                    tipSizeF.Width - HorizontalBorder * 2,
                    tipSizeF.Height - VerticalBorder * 2);

                // DrawRectangle draws from Left to Left + Width. A bug? :-/
                graphics.DrawRectangle(SystemPens.WindowFrame,
                    borderRectangle);
                tipData.Draw(new PointF(HorizontalBorder, VerticalBorder));
            }

            return tipSize;
        }

        public static Size DrawFixedWidthTip(Control control, Graphics graphics, TipSection tipData)
        {
            var tipSize = Size.Empty;
            var tipSizeF = SizeF.Empty;

            PointF screenLocation = control.PointToScreen(new Point(control.Width, 0));

            RectangleF workingArea = GetWorkingArea(control);

            var maxLayoutSize = new SizeF(screenLocation.X - HorizontalBorder * 2,
                workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

            if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
            {
                graphics.TextRenderingHint =
                    TextRenderingHint.AntiAliasGridFit;

                tipData.SetMaximumSize(maxLayoutSize);
                tipSizeF = tipData.GetRequiredSize();
                tipData.SetAllocatedSize(tipSizeF);

                tipSizeF += new SizeF(HorizontalBorder * 2,
                    VerticalBorder * 2);
                tipSize = Size.Ceiling(tipSizeF);
            }

            if (control.Height != tipSize.Height) control.Height = tipSize.Height;

            if (tipSize != Size.Empty)
            {
                var borderRectangle = new Rectangle
                    (Point.Empty, control.Size - new Size(1, 1));

                var displayRectangle = new RectangleF
                (HorizontalBorder, VerticalBorder,
                    tipSizeF.Width - HorizontalBorder * 2,
                    tipSizeF.Height - VerticalBorder * 2);

                // DrawRectangle draws from Left to Left + Width. A bug? :-/
                graphics.DrawRectangle(SystemPens.WindowFrame,
                    borderRectangle);
                tipData.Draw(new PointF(HorizontalBorder, VerticalBorder));
            }

            return tipSize;
        }
    }
}