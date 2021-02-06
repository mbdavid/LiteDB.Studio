// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using LiteDB.Studio.ICSharpCode.TextEditor.Document;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    /// <summary>
    ///     This enum describes all implemented request types
    /// </summary>
    public enum TextAreaUpdateType
    {
        WholeTextArea,
        SingleLine,
        SinglePosition,
        PositionToLineEnd,
        PositionToEnd,
        LinesBetween
    }

    /// <summary>
    ///     This class is used to request an update of the textarea
    /// </summary>
    public class TextAreaUpdate
    {
        /// <summary>
        ///     Creates a new instance of <see cref="TextAreaUpdate" />
        /// </summary>
        public TextAreaUpdate(TextAreaUpdateType type)
        {
            TextAreaUpdateType = type;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="TextAreaUpdate" />
        /// </summary>
        public TextAreaUpdate(TextAreaUpdateType type, TextLocation position)
        {
            TextAreaUpdateType = type;
            Position = position;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="TextAreaUpdate" />
        /// </summary>
        public TextAreaUpdate(TextAreaUpdateType type, int startLine, int endLine)
        {
            TextAreaUpdateType = type;
            Position = new TextLocation(startLine, endLine);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="TextAreaUpdate" />
        /// </summary>
        public TextAreaUpdate(TextAreaUpdateType type, int singleLine)
        {
            TextAreaUpdateType = type;
            Position = new TextLocation(0, singleLine);
        }

        public TextAreaUpdateType TextAreaUpdateType { get; }

        public TextLocation Position { get; }

        public override string ToString()
        {
            return string.Format("[TextAreaUpdate: Type={0}, Position={1}]", TextAreaUpdateType, Position);
        }
    }
}