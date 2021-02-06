// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="none" email=""/>
//     <version>$Revision$</version>
// </file>

using System.Drawing;
using System.Windows.Forms;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    internal static class TipPainterTools
    {
        private const int SpacerSize = 4;

        // btw. I know it's ugly.
        public static Rectangle DrawingRectangle1;
        public static Rectangle DrawingRectangle2;

        public static Size GetLeftHandSideDrawingSizeHelpTipFromCombinedDescription(Control control,
            Graphics graphics,
            Font font,
            string countMessage,
            string description,
            Point p)
        {
            string basicDescription = null;
            string documentation = null;

            if (IsVisibleText(description))
            {
                var splitDescription = description.Split(new[] {'\n'}, 2);

                if (splitDescription.Length > 0)
                {
                    basicDescription = splitDescription[0];

                    if (splitDescription.Length > 1) documentation = splitDescription[1].Trim();
                }
            }

            return GetLeftHandSideDrawingSizeDrawHelpTip(control, graphics, font, countMessage, basicDescription,
                documentation, p);
        }

        public static Size GetDrawingSizeHelpTipFromCombinedDescription(Control control,
            Graphics graphics,
            Font font,
            string countMessage,
            string description)
        {
            string basicDescription = null;
            string documentation = null;

            if (IsVisibleText(description))
            {
                var splitDescription = description.Split(new[] {'\n'}, 2);

                if (splitDescription.Length > 0)
                {
                    basicDescription = splitDescription[0];

                    if (splitDescription.Length > 1) documentation = splitDescription[1].Trim();
                }
            }

            return GetDrawingSizeDrawHelpTip(control, graphics, font, countMessage, basicDescription, documentation);
        }

        public static Size DrawHelpTipFromCombinedDescription(Control control,
            Graphics graphics,
            Font font,
            string countMessage,
            string description)
        {
            string basicDescription = null;
            string documentation = null;

            if (IsVisibleText(description))
            {
                var splitDescription = description.Split
                    (new[] {'\n'}, 2);

                if (splitDescription.Length > 0)
                {
                    basicDescription = splitDescription[0];

                    if (splitDescription.Length > 1) documentation = splitDescription[1].Trim();
                }
            }

            return DrawHelpTip(control, graphics, font, countMessage,
                basicDescription, documentation);
        }

        public static Size DrawFixedWidthHelpTipFromCombinedDescription(Control control,
            Graphics graphics,
            Font font,
            string countMessage,
            string description)
        {
            string basicDescription = null;
            string documentation = null;

            if (IsVisibleText(description))
            {
                var splitDescription = description.Split
                    (new[] {'\n'}, 2);

                if (splitDescription.Length > 0)
                {
                    basicDescription = splitDescription[0];

                    if (splitDescription.Length > 1) documentation = splitDescription[1].Trim();
                }
            }

            return DrawFixedWidthHelpTip(control, graphics, font, countMessage,
                basicDescription, documentation);
        }

        public static Size GetDrawingSizeDrawHelpTip(Control control,
            Graphics graphics, Font font,
            string countMessage,
            string basicDescription,
            string documentation)
        {
            if (IsVisibleText(countMessage) ||
                IsVisibleText(basicDescription) ||
                IsVisibleText(documentation))
            {
                // Create all the TipSection objects.
                var countMessageTip = new CountTipText(graphics, font, countMessage);

                var countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

                var descriptionTip = new TipText(graphics, font, basicDescription);

                var docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

                var docTip = new TipText(graphics, font, documentation);

                // Now put them together.
                var descSplitter = new TipSplitter(graphics, false,
                    descriptionTip,
                    docSpacer
                );

                var mainSplitter = new TipSplitter(graphics, true,
                    countMessageTip,
                    countSpacer,
                    descSplitter);

                var mainSplitter2 = new TipSplitter(graphics, false,
                    mainSplitter,
                    docTip);

                // Show it.
                var size = TipPainter.GetTipSize(control, graphics, mainSplitter2);
                DrawingRectangle1 = countMessageTip.DrawingRectangle1;
                DrawingRectangle2 = countMessageTip.DrawingRectangle2;
                return size;
            }

            return Size.Empty;
        }

        public static Size GetLeftHandSideDrawingSizeDrawHelpTip(Control control,
            Graphics graphics, Font font,
            string countMessage,
            string basicDescription,
            string documentation,
            Point p)
        {
            if (IsVisibleText(countMessage) ||
                IsVisibleText(basicDescription) ||
                IsVisibleText(documentation))
            {
                // Create all the TipSection objects.
                var countMessageTip = new CountTipText(graphics, font, countMessage);

                var countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

                var descriptionTip = new TipText(graphics, font, basicDescription);

                var docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

                var docTip = new TipText(graphics, font, documentation);

                // Now put them together.
                var descSplitter = new TipSplitter(graphics, false,
                    descriptionTip,
                    docSpacer
                );

                var mainSplitter = new TipSplitter(graphics, true,
                    countMessageTip,
                    countSpacer,
                    descSplitter);

                var mainSplitter2 = new TipSplitter(graphics, false,
                    mainSplitter,
                    docTip);

                // Show it.
                var size = TipPainter.GetLeftHandSideTipSize(control, graphics, mainSplitter2, p);
                return size;
            }

            return Size.Empty;
        }

        public static Size DrawHelpTip(Control control,
            Graphics graphics, Font font,
            string countMessage,
            string basicDescription,
            string documentation)
        {
            if (IsVisibleText(countMessage) ||
                IsVisibleText(basicDescription) ||
                IsVisibleText(documentation))
            {
                // Create all the TipSection objects.
                var countMessageTip = new CountTipText(graphics, font, countMessage);

                var countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

                var descriptionTip = new TipText(graphics, font, basicDescription);

                var docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

                var docTip = new TipText(graphics, font, documentation);

                // Now put them together.
                var descSplitter = new TipSplitter(graphics, false,
                    descriptionTip,
                    docSpacer
                );

                var mainSplitter = new TipSplitter(graphics, true,
                    countMessageTip,
                    countSpacer,
                    descSplitter);

                var mainSplitter2 = new TipSplitter(graphics, false,
                    mainSplitter,
                    docTip);

                // Show it.
                var size = TipPainter.DrawTip(control, graphics, mainSplitter2);
                DrawingRectangle1 = countMessageTip.DrawingRectangle1;
                DrawingRectangle2 = countMessageTip.DrawingRectangle2;
                return size;
            }

            return Size.Empty;
        }

        public static Size DrawFixedWidthHelpTip(Control control,
            Graphics graphics, Font font,
            string countMessage,
            string basicDescription,
            string documentation)
        {
            if (IsVisibleText(countMessage) ||
                IsVisibleText(basicDescription) ||
                IsVisibleText(documentation))
            {
                // Create all the TipSection objects.
                var countMessageTip = new CountTipText(graphics, font, countMessage);

                var countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

                var descriptionTip = new TipText(graphics, font, basicDescription);

                var docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

                var docTip = new TipText(graphics, font, documentation);

                // Now put them together.
                var descSplitter = new TipSplitter(graphics, false,
                    descriptionTip,
                    docSpacer
                );

                var mainSplitter = new TipSplitter(graphics, true,
                    countMessageTip,
                    countSpacer,
                    descSplitter);

                var mainSplitter2 = new TipSplitter(graphics, false,
                    mainSplitter,
                    docTip);

                // Show it.
                var size = TipPainter.DrawFixedWidthTip(control, graphics, mainSplitter2);
                DrawingRectangle1 = countMessageTip.DrawingRectangle1;
                DrawingRectangle2 = countMessageTip.DrawingRectangle2;
                return size;
            }

            return Size.Empty;
        }

        private static bool IsVisibleText(string text)
        {
            return text != null && text.Length > 0;
        }
    }
}