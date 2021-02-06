// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.LineManager;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Actions
{
    public class Home : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            LineSegment curLine;
            var newPos = textArea.Caret.Position;
            var jumpedIntoFolding = false;
            do
            {
                curLine = textArea.Document.GetLineSegment(newPos.Y);

                if (TextUtilities.IsEmptyLine(textArea.Document, newPos.Y))
                {
                    if (newPos.X != 0)
                        newPos.X = 0;
                    else
                        newPos.X = curLine.Length;
                }
                else
                {
                    var firstCharOffset = TextUtilities.GetFirstNonWSChar(textArea.Document, curLine.Offset);
                    var firstCharColumn = firstCharOffset - curLine.Offset;

                    if (newPos.X == firstCharColumn)
                        newPos.X = 0;
                    else
                        newPos.X = firstCharColumn;
                }

                var foldings = textArea.Document.FoldingManager.GetFoldingsFromPosition(newPos.Y, newPos.X);
                jumpedIntoFolding = false;
                foreach (var foldMarker in foldings)
                    if (foldMarker.IsFolded)
                    {
                        newPos = new TextLocation(foldMarker.StartColumn, foldMarker.StartLine);
                        jumpedIntoFolding = true;
                        break;
                    }
            } while (jumpedIntoFolding);

            if (newPos != textArea.Caret.Position)
            {
                textArea.Caret.Position = newPos;
                textArea.SetDesiredColumn();
            }
        }
    }

    public class End : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            LineSegment curLine;
            var newPos = textArea.Caret.Position;
            var jumpedIntoFolding = false;
            do
            {
                curLine = textArea.Document.GetLineSegment(newPos.Y);
                newPos.X = curLine.Length;

                var foldings = textArea.Document.FoldingManager.GetFoldingsFromPosition(newPos.Y, newPos.X);
                jumpedIntoFolding = false;
                foreach (var foldMarker in foldings)
                    if (foldMarker.IsFolded)
                    {
                        newPos = new TextLocation(foldMarker.EndColumn, foldMarker.EndLine);
                        jumpedIntoFolding = true;
                        break;
                    }
            } while (jumpedIntoFolding);

            if (newPos != textArea.Caret.Position)
            {
                textArea.Caret.Position = newPos;
                textArea.SetDesiredColumn();
            }
        }
    }


    public class MoveToStart : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            if (textArea.Caret.Line != 0 || textArea.Caret.Column != 0)
            {
                textArea.Caret.Position = new TextLocation(0, 0);
                textArea.SetDesiredColumn();
            }
        }
    }


    public class MoveToEnd : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            var endPos = textArea.Document.OffsetToPosition(textArea.Document.TextLength);
            if (textArea.Caret.Position != endPos)
            {
                textArea.Caret.Position = endPos;
                textArea.SetDesiredColumn();
            }
        }
    }
}