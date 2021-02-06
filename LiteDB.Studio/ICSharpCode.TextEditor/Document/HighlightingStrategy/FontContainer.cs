// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Drawing;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    /// <summary>
    ///     This class is used to generate bold, italic and bold/italic fonts out
    ///     of a base font.
    /// </summary>
    public class FontContainer
    {
        private static float twipsPerPixelY;
        private Font defaultFont;

        public FontContainer(Font defaultFont)
        {
            DefaultFont = defaultFont;
        }

        /// <value>
        ///     The scaled, regular version of the base font
        /// </value>
        public Font RegularFont { get; private set; }

        /// <value>
        ///     The scaled, bold version of the base font
        /// </value>
        public Font BoldFont { get; private set; }

        /// <value>
        ///     The scaled, italic version of the base font
        /// </value>
        public Font ItalicFont { get; private set; }

        /// <value>
        ///     The scaled, bold/italic version of the base font
        /// </value>
        public Font BoldItalicFont { get; private set; }

        public static float TwipsPerPixelY
        {
            get
            {
                if (twipsPerPixelY == 0)
                    using (var bmp = new Bitmap(1, 1))
                    {
                        using (var g = Graphics.FromImage(bmp))
                        {
                            twipsPerPixelY = 1440 / g.DpiY;
                        }
                    }

                return twipsPerPixelY;
            }
        }

        /// <value>
        ///     The base font
        /// </value>
        public Font DefaultFont
        {
            get => defaultFont;
            set
            {
                // 1440 twips is one inch
                var pixelSize = (float) Math.Round(value.SizeInPoints * 20 / TwipsPerPixelY);

                defaultFont = value;
                RegularFont = new Font(value.FontFamily, pixelSize * TwipsPerPixelY / 20f, FontStyle.Regular);
                BoldFont = new Font(RegularFont, FontStyle.Bold);
                ItalicFont = new Font(RegularFont, FontStyle.Italic);
                BoldItalicFont = new Font(RegularFont, FontStyle.Bold | FontStyle.Italic);
            }
        }

        public static Font ParseFont(string font)
        {
            var descr = font.Split(',', '=');
            return new Font(descr[1], float.Parse(descr[3]));
        }
    }
}