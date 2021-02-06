// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.FoldingStrategy
{
    public class FoldingManager
    {
        private readonly IDocument document;
        private List<FoldMarker> foldMarker = new List<FoldMarker>();
        private List<FoldMarker> foldMarkerByEnd = new List<FoldMarker>();

        internal FoldingManager(IDocument document, LineManager.LineManager lineTracker)
        {
            this.document = document;
            document.DocumentChanged += DocumentChanged;

//			lineTracker.LineCountChanged  += new LineManagerEventHandler(LineManagerLineCountChanged);
//			lineTracker.LineLengthChanged += new LineLengthEventHandler(LineManagerLineLengthChanged);
//			foldMarker.Add(new FoldMarker(0, 5, 3, 5));
//
//			foldMarker.Add(new FoldMarker(5, 5, 10, 3));
//			foldMarker.Add(new FoldMarker(6, 0, 8, 2));
//
//			FoldMarker fm1 = new FoldMarker(10, 4, 10, 7);
//			FoldMarker fm2 = new FoldMarker(10, 10, 10, 14);
//
//			fm1.IsFolded = true;
//			fm2.IsFolded = true;
//
//			foldMarker.Add(fm1);
//			foldMarker.Add(fm2);
//			foldMarker.Sort();
        }

        public IList<FoldMarker> FoldMarker => foldMarker.AsReadOnly();

        public IFoldingStrategy FoldingStrategy { get; set; } = null;

        private void DocumentChanged(object sender, DocumentEventArgs e)
        {
            var oldCount = foldMarker.Count;
            document.UpdateSegmentListOnDocumentChange(foldMarker, e);
            if (oldCount != foldMarker.Count)
                document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
        }

        public List<FoldMarker> GetFoldingsFromPosition(int line, int column)
        {
            var foldings = new List<FoldMarker>();
            if (foldMarker != null)
                for (var i = 0; i < foldMarker.Count; ++i)
                {
                    var fm = foldMarker[i];
                    if (fm.StartLine == line && column > fm.StartColumn &&
                        !(fm.EndLine == line && column >= fm.EndColumn) ||
                        fm.EndLine == line && column < fm.EndColumn &&
                        !(fm.StartLine == line && column <= fm.StartColumn) ||
                        line > fm.StartLine && line < fm.EndLine)
                        foldings.Add(fm);
                }

            return foldings;
        }

        private List<FoldMarker> GetFoldingsByStartAfterColumn(int lineNumber, int column, bool forceFolded)
        {
            var foldings = new List<FoldMarker>();

            if (foldMarker != null)
            {
                var index = foldMarker.BinarySearch(
                    new FoldMarker(document, lineNumber, column, lineNumber, column),
                    StartComparer.Instance);
                if (index < 0) index = ~index;

                for (; index < foldMarker.Count; index++)
                {
                    var fm = foldMarker[index];
                    if (fm.StartLine > lineNumber)
                        break;
                    if (fm.StartColumn <= column)
                        continue;
                    if (!forceFolded || fm.IsFolded)
                        foldings.Add(fm);
                }
            }

            return foldings;
        }

        public List<FoldMarker> GetFoldingsWithStart(int lineNumber)
        {
            return GetFoldingsByStartAfterColumn(lineNumber, -1, false);
        }

        public List<FoldMarker> GetFoldedFoldingsWithStart(int lineNumber)
        {
            return GetFoldingsByStartAfterColumn(lineNumber, -1, true);
        }

        public List<FoldMarker> GetFoldedFoldingsWithStartAfterColumn(int lineNumber, int column)
        {
            return GetFoldingsByStartAfterColumn(lineNumber, column, true);
        }

        private List<FoldMarker> GetFoldingsByEndAfterColumn(int lineNumber, int column, bool forceFolded)
        {
            var foldings = new List<FoldMarker>();

            if (foldMarker != null)
            {
                var index = foldMarkerByEnd.BinarySearch(
                    new FoldMarker(document, lineNumber, column, lineNumber, column),
                    EndComparer.Instance);
                if (index < 0) index = ~index;

                for (; index < foldMarkerByEnd.Count; index++)
                {
                    var fm = foldMarkerByEnd[index];
                    if (fm.EndLine > lineNumber)
                        break;
                    if (fm.EndColumn <= column)
                        continue;
                    if (!forceFolded || fm.IsFolded)
                        foldings.Add(fm);
                }
            }

            return foldings;
        }

        public List<FoldMarker> GetFoldingsWithEnd(int lineNumber)
        {
            return GetFoldingsByEndAfterColumn(lineNumber, -1, false);
        }

        public List<FoldMarker> GetFoldedFoldingsWithEnd(int lineNumber)
        {
            return GetFoldingsByEndAfterColumn(lineNumber, -1, true);
        }

        public bool IsFoldStart(int lineNumber)
        {
            return GetFoldingsWithStart(lineNumber).Count > 0;
        }

        public bool IsFoldEnd(int lineNumber)
        {
            return GetFoldingsWithEnd(lineNumber).Count > 0;
        }

        public List<FoldMarker> GetFoldingsContainsLineNumber(int lineNumber)
        {
            var foldings = new List<FoldMarker>();
            if (foldMarker != null)
                foreach (var fm in foldMarker)
                    if (fm.StartLine < lineNumber && lineNumber < fm.EndLine)
                        foldings.Add(fm);
            return foldings;
        }

        public bool IsBetweenFolding(int lineNumber)
        {
            return GetFoldingsContainsLineNumber(lineNumber).Count > 0;
        }

        public bool IsLineVisible(int lineNumber)
        {
            foreach (var fm in GetFoldingsContainsLineNumber(lineNumber))
                if (fm.IsFolded)
                    return false;
            return true;
        }

        public List<FoldMarker> GetTopLevelFoldedFoldings()
        {
            var foldings = new List<FoldMarker>();
            if (foldMarker != null)
            {
                var end = new Point(0, 0);
                foreach (var fm in foldMarker)
                    if (fm.IsFolded && (fm.StartLine > end.Y || fm.StartLine == end.Y && fm.StartColumn >= end.X))
                    {
                        foldings.Add(fm);
                        end = new Point(fm.EndColumn, fm.EndLine);
                    }
            }

            return foldings;
        }

        public void UpdateFoldings(string fileName, object parseInfo)
        {
            UpdateFoldings(FoldingStrategy.GenerateFoldMarkers(document, fileName, parseInfo));
        }

        public void UpdateFoldings(List<FoldMarker> newFoldings)
        {
            var oldFoldingsCount = foldMarker.Count;
            lock (this)
            {
                if (newFoldings != null && newFoldings.Count != 0)
                {
                    newFoldings.Sort();
                    if (foldMarker.Count == newFoldings.Count)
                    {
                        for (var i = 0; i < foldMarker.Count; ++i) newFoldings[i].IsFolded = foldMarker[i].IsFolded;
                        foldMarker = newFoldings;
                    }
                    else
                    {
                        for (int i = 0, j = 0; i < foldMarker.Count && j < newFoldings.Count;)
                        {
                            var n = newFoldings[j].CompareTo(foldMarker[i]);
                            if (n > 0)
                            {
                                ++i;
                            }
                            else
                            {
                                if (n == 0) newFoldings[j].IsFolded = foldMarker[i].IsFolded;
                                ++j;
                            }
                        }
                    }
                }

                if (newFoldings != null)
                {
                    foldMarker = newFoldings;
                    foldMarkerByEnd = new List<FoldMarker>(newFoldings);
                    foldMarkerByEnd.Sort(EndComparer.Instance);
                }
                else
                {
                    foldMarker.Clear();
                    foldMarkerByEnd.Clear();
                }
            }

            if (oldFoldingsCount != foldMarker.Count)
            {
                document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
                document.CommitUpdate();
            }
        }

        public string SerializeToString()
        {
            var sb = new StringBuilder();
            foreach (var marker in foldMarker)
            {
                sb.Append(marker.Offset);
                sb.Append("\n");
                sb.Append(marker.Length);
                sb.Append("\n");
                sb.Append(marker.FoldText);
                sb.Append("\n");
                sb.Append(marker.IsFolded);
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public void DeserializeFromString(string str)
        {
            try
            {
                var lines = str.Split('\n');
                for (var i = 0; i < lines.Length && lines[i].Length > 0; i += 4)
                {
                    var offset = int.Parse(lines[i]);
                    var length = int.Parse(lines[i + 1]);
                    var text = lines[i + 2];
                    var isFolded = bool.Parse(lines[i + 3]);
                    var found = false;
                    foreach (var marker in foldMarker)
                        if (marker.Offset == offset && marker.Length == length)
                        {
                            marker.IsFolded = isFolded;
                            found = true;
                            break;
                        }

                    if (!found) foldMarker.Add(new FoldMarker(document, offset, length, text, isFolded));
                }

                if (lines.Length > 0) NotifyFoldingsChanged(EventArgs.Empty);
            }
            catch (Exception)
            {
            }
        }

        public void NotifyFoldingsChanged(EventArgs e)
        {
            if (FoldingsChanged != null) FoldingsChanged(this, e);
        }


        public event EventHandler FoldingsChanged;

        private class StartComparer : IComparer<FoldMarker>
        {
            public static readonly StartComparer Instance = new StartComparer();

            public int Compare(FoldMarker x, FoldMarker y)
            {
                if (x.StartLine < y.StartLine)
                    return -1;
                if (x.StartLine == y.StartLine)
                    return x.StartColumn.CompareTo(y.StartColumn);
                return 1;
            }
        }

        private class EndComparer : IComparer<FoldMarker>
        {
            public static readonly EndComparer Instance = new EndComparer();

            public int Compare(FoldMarker x, FoldMarker y)
            {
                if (x.EndLine < y.EndLine)
                    return -1;
                if (x.EndLine == y.EndLine)
                    return x.EndColumn.CompareTo(y.EndColumn);
                return 1;
            }
        }
    }
}