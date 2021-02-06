// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.FoldingStrategy;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Actions
{
    public class ToggleFolding : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            var foldMarkers = textArea.Document.FoldingManager.GetFoldingsWithStart(textArea.Caret.Line);
            if (foldMarkers.Count != 0)
            {
                foreach (var fm in foldMarkers)
                    fm.IsFolded = !fm.IsFolded;
            }
            else
            {
                foldMarkers = textArea.Document.FoldingManager.GetFoldingsContainsLineNumber(textArea.Caret.Line);
                if (foldMarkers.Count != 0)
                {
                    var innerMost = foldMarkers[0];
                    for (var i = 1; i < foldMarkers.Count; i++)
                        if (new TextLocation(foldMarkers[i].StartColumn, foldMarkers[i].StartLine) >
                            new TextLocation(innerMost.StartColumn, innerMost.StartLine))
                            innerMost = foldMarkers[i];
                    innerMost.IsFolded = !innerMost.IsFolded;
                }
            }

            textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
        }
    }

    public class ToggleAllFoldings : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            var doFold = true;
            foreach (var fm in textArea.Document.FoldingManager.FoldMarker)
                if (fm.IsFolded)
                {
                    doFold = false;
                    break;
                }

            foreach (var fm in textArea.Document.FoldingManager.FoldMarker) fm.IsFolded = doFold;
            textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
        }
    }

    public class ShowDefinitionsOnly : AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            foreach (var fm in textArea.Document.FoldingManager.FoldMarker)
                fm.IsFolded = fm.FoldType == FoldType.MemberBody || fm.FoldType == FoldType.Region;
            textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
        }
    }
}