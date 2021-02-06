// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="none" email=""/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Drawing;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    internal class CountTipText : TipText
    {
        private readonly float triHeight = 10;
        private readonly float triWidth = 10;
        public Rectangle DrawingRectangle1;
        public Rectangle DrawingRectangle2;

        public CountTipText(Graphics graphics, Font font, string text) : base(graphics, font, text)
        {
        }

        private void DrawTriangle(float x, float y, bool flipped)
        {
            var brush = BrushRegistry.GetBrush(Color.FromArgb(192, 192, 192));
            Graphics.FillRectangle(brush, new RectangleF(x, y, triHeight, triHeight));
            var triHeight2 = triHeight / 2;
            var triHeight4 = triHeight / 4;
            brush = Brushes.Black;
            if (flipped)
                Graphics.FillPolygon(brush, new[]
                {
                    new PointF(x, y + triHeight2 - triHeight4),
                    new PointF(x + triWidth / 2, y + triHeight2 + triHeight4),
                    new PointF(x + triWidth, y + triHeight2 - triHeight4)
                });
            else
                Graphics.FillPolygon(brush, new[]
                {
                    new PointF(x, y + triHeight2 + triHeight4),
                    new PointF(x + triWidth / 2, y + triHeight2 - triHeight4),
                    new PointF(x + triWidth, y + triHeight2 + triHeight4)
                });
        }

        public override void Draw(PointF location)
        {
            if (tipText != null && tipText.Length > 0)
            {
                base.Draw(new PointF(location.X + triWidth + 4, location.Y));
                DrawingRectangle1 = new Rectangle((int) location.X + 2,
                    (int) location.Y + 2,
                    (int) triWidth,
                    (int) triHeight);
                DrawingRectangle2 = new Rectangle((int) (location.X + AllocatedSize.Width - triWidth - 2),
                    (int) location.Y + 2,
                    (int) triWidth,
                    (int) triHeight);
                DrawTriangle(location.X + 2, location.Y + 2, false);
                DrawTriangle(location.X + AllocatedSize.Width - triWidth - 2, location.Y + 2, true);
            }
        }

        protected override void OnMaximumSizeChanged()
        {
            if (IsTextVisible())
            {
                var tipSize = Graphics.MeasureString
                (tipText, tipFont, MaximumSize,
                    GetInternalStringFormat());
                tipSize.Width += triWidth * 2 + 8;
                SetRequiredSize(tipSize);
            }
            else
            {
                SetRequiredSize(SizeF.Empty);
            }
        }
    }

    internal class TipText : TipSection
    {
        protected StringAlignment horzAlign;
        protected Color tipColor;
        protected Font tipFont;
        protected StringFormat tipFormat;
        protected string tipText;
        protected StringAlignment vertAlign;

        public TipText(Graphics graphics, Font font, string text) :
            base(graphics)
        {
            tipFont = font;
            tipText = text;
            if (text != null && text.Length > short.MaxValue)
                throw new ArgumentException("TipText: text too long (max. is " + short.MaxValue + " characters)",
                    "text");

            Color = SystemColors.InfoText;
            HorizontalAlignment = StringAlignment.Near;
            VerticalAlignment = StringAlignment.Near;
        }

        public Color Color
        {
            get => tipColor;
            set => tipColor = value;
        }

        public StringAlignment HorizontalAlignment
        {
            get => horzAlign;
            set
            {
                horzAlign = value;
                tipFormat = null;
            }
        }

        public StringAlignment VerticalAlignment
        {
            get => vertAlign;
            set
            {
                vertAlign = value;
                tipFormat = null;
            }
        }

        public override void Draw(PointF location)
        {
            if (IsTextVisible())
            {
                var drawRectangle = new RectangleF(location, AllocatedSize);

                Graphics.DrawString(tipText, tipFont,
                    BrushRegistry.GetBrush(Color),
                    drawRectangle,
                    GetInternalStringFormat());
            }
        }

        protected StringFormat GetInternalStringFormat()
        {
            if (tipFormat == null) tipFormat = CreateTipStringFormat(horzAlign, vertAlign);

            return tipFormat;
        }

        protected override void OnMaximumSizeChanged()
        {
            base.OnMaximumSizeChanged();

            if (IsTextVisible())
            {
                var tipSize = Graphics.MeasureString
                (tipText, tipFont, MaximumSize,
                    GetInternalStringFormat());

                SetRequiredSize(tipSize);
            }
            else
            {
                SetRequiredSize(SizeF.Empty);
            }
        }

        private static StringFormat CreateTipStringFormat(StringAlignment horizontalAlignment,
            StringAlignment verticalAlignment)
        {
            var format = (StringFormat) StringFormat.GenericTypographic.Clone();
            format.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces;
            // note: Align Near, Line Center seemed to do something before

            format.Alignment = horizontalAlignment;
            format.LineAlignment = verticalAlignment;

            return format;
        }

        protected bool IsTextVisible()
        {
            return tipText != null && tipText.Length > 0;
        }
    }
}