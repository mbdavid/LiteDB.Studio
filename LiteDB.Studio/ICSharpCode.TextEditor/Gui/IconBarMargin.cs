// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.BookmarkManager;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    /// <summary>
    ///     This class views the line numbers and folding markers.
    /// </summary>
    public class IconBarMargin : AbstractMargin
    {
        private const int iconBarWidth = 18;

        private static readonly Size iconBarSize = new Size(iconBarWidth, -1);


        public IconBarMargin(TextArea textArea) : base(textArea)
        {
        }

        public override Size Size => iconBarSize;

        public override bool IsVisible => textArea.TextEditorProperties.IsIconBarVisible;

        public override void Paint(Graphics g, Rectangle rect)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;
            // paint background
            g.FillRectangle(SystemBrushes.Control,
                new Rectangle(drawingPosition.X, rect.Top, drawingPosition.Width - 1, rect.Height));
            g.DrawLine(SystemPens.ControlDark, drawingPosition.Right - 1, rect.Top, drawingPosition.Right - 1,
                rect.Bottom);

            // paint icons
            foreach (var mark in textArea.Document.BookmarkManager.Marks)
            {
                var lineNumber = textArea.Document.GetVisibleLine(mark.LineNumber);
                var lineHeight = textArea.TextView.FontHeight;
                var yPos = lineNumber * lineHeight - textArea.VirtualTop.Y;
                if (IsLineInsideRegion(yPos, yPos + lineHeight, rect.Y, rect.Bottom))
                {
                    if (lineNumber == textArea.Document.GetVisibleLine(mark.LineNumber - 1)
                    ) // marker is inside folded region, do not draw it
                        continue;
                    mark.Draw(this, g, new Point(0, yPos));
                }
            }

            base.Paint(g, rect);
        }

        public override void HandleMouseDown(Point mousePos, MouseButtons mouseButtons)
        {
            var clickedVisibleLine = (mousePos.Y + textArea.VirtualTop.Y) / textArea.TextView.FontHeight;
            var lineNumber = textArea.Document.GetFirstLogicalLine(clickedVisibleLine);

            if ((mouseButtons & MouseButtons.Right) == MouseButtons.Right)
                if (textArea.Caret.Line != lineNumber)
                    textArea.Caret.Line = lineNumber;

            IList<Bookmark> marks = textArea.Document.BookmarkManager.Marks;
            var marksInLine = new List<Bookmark>();
            var oldCount = marks.Count;
            foreach (var mark in marks)
                if (mark.LineNumber == lineNumber)
                    marksInLine.Add(mark);
            for (var i = marksInLine.Count - 1; i >= 0; i--)
            {
                var mark = marksInLine[i];
                if (mark.Click(textArea, new MouseEventArgs(mouseButtons, 1, mousePos.X, mousePos.Y, 0)))
                {
                    if (oldCount != marks.Count) textArea.UpdateLine(lineNumber);
                    return;
                }
            }

            base.HandleMouseDown(mousePos, mouseButtons);
        }

        private static bool IsLineInsideRegion(int top, int bottom, int regionTop, int regionBottom)
        {
            if (top >= regionTop && top <= regionBottom) // Region overlaps the line's top edge.
                return true;
            if (regionTop > top && regionTop < bottom) // Region's top edge inside line.
                return true;
            return false;
        }

        #region Drawing functions

        public void DrawBreakpoint(Graphics g, int y, bool isEnabled, bool isHealthy)
        {
            var diameter = Math.Min(iconBarWidth - 2, textArea.TextView.FontHeight);
            var rect = new Rectangle(1,
                y + (textArea.TextView.FontHeight - diameter) / 2,
                diameter,
                diameter);


            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (var pthGrBrush = new PathGradientBrush(path))
                {
                    pthGrBrush.CenterPoint = new PointF(rect.Left + rect.Width / 3, rect.Top + rect.Height / 3);
                    pthGrBrush.CenterColor = Color.MistyRose;
                    Color[] colors = {isHealthy ? Color.Firebrick : Color.Olive};
                    pthGrBrush.SurroundColors = colors;

                    if (isEnabled)
                    {
                        g.FillEllipse(pthGrBrush, rect);
                    }
                    else
                    {
                        g.FillEllipse(SystemBrushes.Control, rect);
                        using (var pen = new Pen(pthGrBrush))
                        {
                            g.DrawEllipse(pen, new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2));
                        }
                    }
                }
            }
        }

        public void DrawBookmark(Graphics g, int y, bool isEnabled)
        {
            var delta = textArea.TextView.FontHeight / 8;
            var rect = new Rectangle(1, y + delta, drawingPosition.Width - 4, textArea.TextView.FontHeight - delta * 2);

            if (isEnabled)
                using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                    new Point(rect.Right, rect.Bottom),
                    Color.SkyBlue,
                    Color.White))
                {
                    FillRoundRect(g, brush, rect);
                }
            else
                FillRoundRect(g, Brushes.White, rect);

            using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Bottom),
                Color.SkyBlue,
                Color.Blue))
            {
                using (var pen = new Pen(brush))
                {
                    DrawRoundRect(g, pen, rect);
                }
            }
        }

        public void DrawArrow(Graphics g, int y)
        {
            var delta = textArea.TextView.FontHeight / 8;
            var rect = new Rectangle(1, y + delta, drawingPosition.Width - 4, textArea.TextView.FontHeight - delta * 2);
            using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Bottom),
                Color.LightYellow,
                Color.Yellow))
            {
                FillArrow(g, brush, rect);
            }

            using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Bottom),
                Color.Yellow,
                Color.Brown))
            {
                using (var pen = new Pen(brush))
                {
                    DrawArrow(g, pen, rect);
                }
            }
        }

        private GraphicsPath CreateArrowGraphicsPath(Rectangle r)
        {
            var gp = new GraphicsPath();
            var halfX = r.Width / 2;
            var halfY = r.Height / 2;
            gp.AddLine(r.X, r.Y + halfY / 2, r.X + halfX, r.Y + halfY / 2);
            gp.AddLine(r.X + halfX, r.Y + halfY / 2, r.X + halfX, r.Y);
            gp.AddLine(r.X + halfX, r.Y, r.Right, r.Y + halfY);
            gp.AddLine(r.Right, r.Y + halfY, r.X + halfX, r.Bottom);
            gp.AddLine(r.X + halfX, r.Bottom, r.X + halfX, r.Bottom - halfY / 2);
            gp.AddLine(r.X + halfX, r.Bottom - halfY / 2, r.X, r.Bottom - halfY / 2);
            gp.AddLine(r.X, r.Bottom - halfY / 2, r.X, r.Y + halfY / 2);
            gp.CloseFigure();
            return gp;
        }

        private GraphicsPath CreateRoundRectGraphicsPath(Rectangle r)
        {
            var gp = new GraphicsPath();
            var radius = r.Width / 2;
            gp.AddLine(r.X + radius, r.Y, r.Right - radius, r.Y);
            gp.AddArc(r.Right - radius, r.Y, radius, radius, 270, 90);

            gp.AddLine(r.Right, r.Y + radius, r.Right, r.Bottom - radius);
            gp.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);

            gp.AddLine(r.Right - radius, r.Bottom, r.X + radius, r.Bottom);
            gp.AddArc(r.X, r.Bottom - radius, radius, radius, 90, 90);

            gp.AddLine(r.X, r.Bottom - radius, r.X, r.Y + radius);
            gp.AddArc(r.X, r.Y, radius, radius, 180, 90);

            gp.CloseFigure();
            return gp;
        }

        private void DrawRoundRect(Graphics g, Pen p, Rectangle r)
        {
            using (var gp = CreateRoundRectGraphicsPath(r))
            {
                g.DrawPath(p, gp);
            }
        }

        private void FillRoundRect(Graphics g, Brush b, Rectangle r)
        {
            using (var gp = CreateRoundRectGraphicsPath(r))
            {
                g.FillPath(b, gp);
            }
        }

        private void DrawArrow(Graphics g, Pen p, Rectangle r)
        {
            using (var gp = CreateArrowGraphicsPath(r))
            {
                g.DrawPath(p, gp);
            }
        }

        private void FillArrow(Graphics g, Brush b, Rectangle r)
        {
            using (var gp = CreateArrowGraphicsPath(r))
            {
                g.FillPath(b, gp);
            }
        }

        #endregion
    }
}