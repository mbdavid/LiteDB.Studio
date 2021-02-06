// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiteDB.Studio.ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.BookmarkManager
{
    public interface IBookmarkFactory
    {
        Bookmark CreateBookmark(IDocument document, TextLocation location);
    }

    /// <summary>
    ///     This class handles the bookmarks for a buffer
    /// </summary>
    public class BookmarkManager
    {
#if DEBUG
        private readonly IList<Bookmark> bookmark = new CheckedList<Bookmark>();
#else
		List<Bookmark> bookmark = new List<Bookmark>();
#endif

        /// <value>
        ///     Contains all bookmarks
        /// </value>
        public ReadOnlyCollection<Bookmark> Marks => new ReadOnlyCollection<Bookmark>(bookmark);

        public IDocument Document { get; }

        /// <summary>
        ///     Creates a new instance of <see cref="BookmarkManager" />
        /// </summary>
        internal BookmarkManager(IDocument document, LineManager.LineManager lineTracker)
        {
            Document = document;
        }

        /// <summary>
        ///     Gets/Sets the bookmark factory used to create bookmarks for "ToggleMarkAt".
        /// </summary>
        public IBookmarkFactory Factory { get; set; }

        /// <summary>
        ///     Sets the mark at the line <code>location.Line</code> if it is not set, if the
        ///     line is already marked the mark is cleared.
        /// </summary>
        public void ToggleMarkAt(TextLocation location)
        {
            Bookmark newMark;
            if (Factory != null)
                newMark = Factory.CreateBookmark(Document, location);
            else
                newMark = new Bookmark(Document, location);

            var newMarkType = newMark.GetType();

            for (var i = 0; i < bookmark.Count; ++i)
            {
                var mark = bookmark[i];

                if (mark.LineNumber == location.Line && mark.CanToggle && mark.GetType() == newMarkType)
                {
                    bookmark.RemoveAt(i);
                    OnRemoved(new BookmarkEventArgs(mark));
                    return;
                }
            }

            bookmark.Add(newMark);
            OnAdded(new BookmarkEventArgs(newMark));
        }

        public void AddMark(Bookmark mark)
        {
            bookmark.Add(mark);
            OnAdded(new BookmarkEventArgs(mark));
        }

        public void RemoveMark(Bookmark mark)
        {
            bookmark.Remove(mark);
            OnRemoved(new BookmarkEventArgs(mark));
        }

        public void RemoveMarks(Predicate<Bookmark> predicate)
        {
            for (var i = 0; i < bookmark.Count; ++i)
            {
                var bm = bookmark[i];
                if (predicate(bm))
                {
                    bookmark.RemoveAt(i--);
                    OnRemoved(new BookmarkEventArgs(bm));
                }
            }
        }

        /// <returns>
        ///     true, if a mark at mark exists, otherwise false
        /// </returns>
        public bool IsMarked(int lineNr)
        {
            for (var i = 0; i < bookmark.Count; ++i)
                if (bookmark[i].LineNumber == lineNr)
                    return true;
            return false;
        }

        /// <remarks>
        ///     Clears all bookmark
        /// </remarks>
        public void Clear()
        {
            foreach (var mark in bookmark) OnRemoved(new BookmarkEventArgs(mark));
            bookmark.Clear();
        }

        /// <value>
        ///     The lowest mark, if no marks exists it returns -1
        /// </value>
        public Bookmark GetFirstMark(Predicate<Bookmark> predicate)
        {
            if (bookmark.Count < 1) return null;
            Bookmark first = null;
            for (var i = 0; i < bookmark.Count; ++i)
                if (predicate(bookmark[i]) && bookmark[i].IsEnabled &&
                    (first == null || bookmark[i].LineNumber < first.LineNumber))
                    first = bookmark[i];
            return first;
        }

        /// <value>
        ///     The highest mark, if no marks exists it returns -1
        /// </value>
        public Bookmark GetLastMark(Predicate<Bookmark> predicate)
        {
            if (bookmark.Count < 1) return null;
            Bookmark last = null;
            for (var i = 0; i < bookmark.Count; ++i)
                if (predicate(bookmark[i]) && bookmark[i].IsEnabled &&
                    (last == null || bookmark[i].LineNumber > last.LineNumber))
                    last = bookmark[i];
            return last;
        }

        private bool AcceptAnyMarkPredicate(Bookmark mark)
        {
            return true;
        }

        public Bookmark GetNextMark(int curLineNr)
        {
            return GetNextMark(curLineNr, AcceptAnyMarkPredicate);
        }

        /// <remarks>
        ///     returns first mark higher than <code>lineNr</code>
        /// </remarks>
        /// <returns>
        ///     returns the next mark > cur, if it not exists it returns FirstMark()
        /// </returns>
        public Bookmark GetNextMark(int curLineNr, Predicate<Bookmark> predicate)
        {
            if (bookmark.Count == 0) return null;

            var next = GetFirstMark(predicate);
            foreach (var mark in bookmark)
                if (predicate(mark) && mark.IsEnabled && mark.LineNumber > curLineNr)
                    if (mark.LineNumber < next.LineNumber || next.LineNumber <= curLineNr)
                        next = mark;
            return next;
        }

        public Bookmark GetPrevMark(int curLineNr)
        {
            return GetPrevMark(curLineNr, AcceptAnyMarkPredicate);
        }

        /// <remarks>
        ///     returns first mark lower than <code>lineNr</code>
        /// </remarks>
        /// <returns>
        ///     returns the next mark lower than cur, if it not exists it returns LastMark()
        /// </returns>
        public Bookmark GetPrevMark(int curLineNr, Predicate<Bookmark> predicate)
        {
            if (bookmark.Count == 0) return null;

            var prev = GetLastMark(predicate);

            foreach (var mark in bookmark)
                if (predicate(mark) && mark.IsEnabled && mark.LineNumber < curLineNr)
                    if (mark.LineNumber > prev.LineNumber || prev.LineNumber >= curLineNr)
                        prev = mark;
            return prev;
        }

        protected virtual void OnRemoved(BookmarkEventArgs e)
        {
            if (Removed != null) Removed(this, e);
        }

        protected virtual void OnAdded(BookmarkEventArgs e)
        {
            if (Added != null) Added(this, e);
        }

        public event BookmarkEventHandler Removed;
        public event BookmarkEventHandler Added;
    }
}