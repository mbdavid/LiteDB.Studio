// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System.Drawing;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.MarkerStrategy
{
    public enum TextMarkerType
    {
        Invisible,
        SolidBlock,
        Underlined,
        WaveLine
    }

    /// <summary>
    ///     Marks a part of a document.
    /// </summary>
    public class TextMarker : ISegment
    {
        protected int length = -1;
        protected int offset = -1;

        public TextMarker(int offset, int length, TextMarkerType textMarkerType) : this(offset, length, textMarkerType,
            Color.Red)
        {
        }

        public TextMarker(int offset, int length, TextMarkerType textMarkerType, Color color)
        {
            if (length < 1) length = 1;
            this.offset = offset;
            this.length = length;
            TextMarkerType = textMarkerType;
            Color = color;
        }

        public TextMarker(int offset, int length, TextMarkerType textMarkerType, Color color, Color foreColor)
        {
            if (length < 1) length = 1;
            this.offset = offset;
            this.length = length;
            TextMarkerType = textMarkerType;
            Color = color;
            ForeColor = foreColor;
            OverrideForeColor = true;
        }

        public TextMarkerType TextMarkerType { get; }

        public Color Color { get; }

        public Color ForeColor { get; }

        public bool OverrideForeColor { get; }

        /// <summary>
        ///     Marks the text segment as read-only.
        /// </summary>
        public bool IsReadOnly { get; set; }

        public string ToolTip { get; set; } = null;

        /// <summary>
        ///     Gets the last offset that is inside the marker region.
        /// </summary>
        public int EndOffset => offset + length - 1;

        public override string ToString()
        {
            return string.Format("[TextMarker: Offset = {0}, Length = {1}, Type = {2}]",
                offset,
                length,
                TextMarkerType);
        }

        #region ICSharpCode.TextEditor.Document.ISegment interface implementation

        public int Offset
        {
            get => offset;
            set => offset = value;
        }

        public int Length
        {
            get => length;
            set => length = value;
        }

        #endregion
    }
}