// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.MarkerStrategy
{
    /// <summary>
    ///     Manages the list of markers and provides ways to retrieve markers for specific positions.
    /// </summary>
    public sealed class MarkerStrategy
    {
        private readonly Dictionary<int, List<TextMarker>> markersTable = new Dictionary<int, List<TextMarker>>();
        private readonly List<TextMarker> textMarker = new List<TextMarker>();

        public MarkerStrategy(IDocument document)
        {
            Document = document;
            document.DocumentChanged += DocumentChanged;
        }

        public IDocument Document { get; }

        public IEnumerable<TextMarker> TextMarker => textMarker.AsReadOnly();

        public void AddMarker(TextMarker item)
        {
            markersTable.Clear();
            textMarker.Add(item);
        }

        public void InsertMarker(int index, TextMarker item)
        {
            markersTable.Clear();
            textMarker.Insert(index, item);
        }

        public void RemoveMarker(TextMarker item)
        {
            markersTable.Clear();
            textMarker.Remove(item);
        }

        public void RemoveAll(Predicate<TextMarker> match)
        {
            markersTable.Clear();
            textMarker.RemoveAll(match);
        }

        public List<TextMarker> GetMarkers(int offset)
        {
            if (!markersTable.ContainsKey(offset))
            {
                var markers = new List<TextMarker>();
                for (var i = 0; i < textMarker.Count; ++i)
                {
                    var marker = textMarker[i];
                    if (marker.Offset <= offset && offset <= marker.EndOffset) markers.Add(marker);
                }

                markersTable[offset] = markers;
            }

            return markersTable[offset];
        }

        public List<TextMarker> GetMarkers(int offset, int length)
        {
            var endOffset = offset + length - 1;
            var markers = new List<TextMarker>();
            for (var i = 0; i < textMarker.Count; ++i)
            {
                var marker = textMarker[i];
                var markerOffset = marker.Offset;
                var markerEndOffset = marker.EndOffset;
                if ( // start in marker region
                    markerOffset <= offset && offset <= markerEndOffset ||
                    // end in marker region
                    markerOffset <= endOffset && endOffset <= markerEndOffset ||
                    // marker start in region
                    offset <= markerOffset && markerOffset <= endOffset ||
                    // marker end in region
                    offset <= markerEndOffset && markerEndOffset <= endOffset
                )
                    markers.Add(marker);
            }

            return markers;
        }

        public List<TextMarker> GetMarkers(TextLocation position)
        {
            if (position.Y >= Document.TotalNumberOfLines || position.Y < 0) return new List<TextMarker>();
            var segment = Document.GetLineSegment(position.Y);
            return GetMarkers(segment.Offset + position.X);
        }

        private void DocumentChanged(object sender, DocumentEventArgs e)
        {
            // reset markers table
            markersTable.Clear();
            Document.UpdateSegmentListOnDocumentChange(textMarker, e);
        }
    }
}