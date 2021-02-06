// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    public class Highlight
    {
        public Highlight(TextLocation openBrace, TextLocation closeBrace)
        {
            OpenBrace = openBrace;
            CloseBrace = closeBrace;
        }

        public TextLocation OpenBrace { get; set; }
        public TextLocation CloseBrace { get; set; }
    }

    public class BracketHighlightingSheme
    {
        public BracketHighlightingSheme(char opentag, char closingtag)
        {
            OpenTag = opentag;
            ClosingTag = closingtag;
        }

        public char OpenTag { get; set; }

        public char ClosingTag { get; set; }

        public Highlight GetHighlight(IDocument document, int offset)
        {
            int searchOffset;
            if (document.TextEditorProperties.BracketMatchingStyle == BracketMatchingStyle.After)
                searchOffset = offset;
            else
                searchOffset = offset + 1;
            var word = document.GetCharAt(Math.Max(0, Math.Min(document.TextLength - 1, searchOffset)));

            var endP = document.OffsetToPosition(searchOffset);
            if (word == OpenTag)
            {
                if (searchOffset < document.TextLength)
                {
                    var bracketOffset =
                        TextUtilities.SearchBracketForward(document, searchOffset + 1, OpenTag, ClosingTag);
                    if (bracketOffset >= 0)
                    {
                        var p = document.OffsetToPosition(bracketOffset);
                        return new Highlight(p, endP);
                    }
                }
            }
            else if (word == ClosingTag)
            {
                if (searchOffset > 0)
                {
                    var bracketOffset =
                        TextUtilities.SearchBracketBackward(document, searchOffset - 1, OpenTag, ClosingTag);
                    if (bracketOffset >= 0)
                    {
                        var p = document.OffsetToPosition(bracketOffset);
                        return new Highlight(p, endP);
                    }
                }
            }

            return null;
        }
    }
}