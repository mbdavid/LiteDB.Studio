// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Collections.Generic;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.FoldingStrategy
{
    /// <summary>
    ///     A simple folding strategy which calculates the folding level
    ///     using the indent level of the line.
    /// </summary>
    public class IndentFoldingStrategy : IFoldingStrategy
    {
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            var l = new List<FoldMarker>();
            var offsetStack = new Stack<int>();
            var textStack = new Stack<string>();
            //int level = 0;
            //foreach (LineSegment segment in document.LineSegmentCollection) {
            //	
            //}
            return l;
        }

        private int GetLevel(IDocument document, int offset)
        {
            var level = 0;
            var spaces = 0;
            for (var i = offset; i < document.TextLength; ++i)
            {
                var c = document.GetCharAt(i);
                if (c == '\t' || c == ' ' && ++spaces == 4)
                {
                    spaces = 0;
                    ++level;
                }
                else
                {
                    break;
                }
            }

            return level;
        }
    }
}