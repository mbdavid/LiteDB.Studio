// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    public class HighlightInfo
    {
        public bool BlockSpanOn;
        public Span CurSpan;
        public bool Span;

        public HighlightInfo(Span curSpan, bool span, bool blockSpanOn)
        {
            CurSpan = curSpan;
            Span = span;
            BlockSpanOn = blockSpanOn;
        }
    }
}