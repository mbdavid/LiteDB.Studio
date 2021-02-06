// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Xml;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    /// <summary>
    ///     Used for mark previous token
    /// </summary>
    public class PrevMarker
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PrevMarker" />
        /// </summary>
        public PrevMarker(XmlElement mark)
        {
            Color = new HighlightColor(mark);
            What = mark.InnerText;
            if (mark.Attributes["markmarker"] != null) MarkMarker = bool.Parse(mark.Attributes["markmarker"].InnerText);
        }

        /// <value>
        ///     String value to indicate to mark previous token
        /// </value>
        public string What { get; }

        /// <value>
        ///     Color for marking previous token
        /// </value>
        public HighlightColor Color { get; }

        /// <value>
        ///     If true the indication text will be marked with the same color
        ///     too
        /// </value>
        public bool MarkMarker { get; }
    }
}