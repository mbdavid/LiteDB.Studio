// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.LineManager
{
    public class LineCountChangeEventArgs : EventArgs
    {
        public LineCountChangeEventArgs(IDocument document, int lineStart, int linesMoved)
        {
            Document = document;
            LineStart = lineStart;
            LinesMoved = linesMoved;
        }

        /// <returns>
        ///     always a valid Document which is related to the Event.
        /// </returns>
        public IDocument Document { get; }

        /// <returns>
        ///     -1 if no offset was specified for this event
        /// </returns>
        public int LineStart { get; }

        /// <returns>
        ///     -1 if no length was specified for this event
        /// </returns>
        public int LinesMoved { get; }
    }

    public class LineEventArgs : EventArgs
    {
        public LineEventArgs(IDocument document, LineSegment lineSegment)
        {
            Document = document;
            LineSegment = lineSegment;
        }

        public IDocument Document { get; }

        public LineSegment LineSegment { get; }

        public override string ToString()
        {
            return string.Format("[LineEventArgs Document={0} LineSegment={1}]", Document, LineSegment);
        }
    }

    public class LineLengthChangeEventArgs : LineEventArgs
    {
        public LineLengthChangeEventArgs(IDocument document, LineSegment lineSegment, int moved)
            : base(document, lineSegment)
        {
            LengthDelta = moved;
        }

        public int LengthDelta { get; }

        public override string ToString()
        {
            return string.Format("[LineLengthEventArgs Document={0} LineSegment={1} LengthDelta={2}]", Document,
                LineSegment, LengthDelta);
        }
    }
}