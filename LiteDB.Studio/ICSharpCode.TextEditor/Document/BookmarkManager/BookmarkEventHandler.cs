// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.BookmarkManager
{
    public delegate void BookmarkEventHandler(object sender, BookmarkEventArgs e);

    /// <summary>
    ///     Description of BookmarkEventHandler.
    /// </summary>
    public class BookmarkEventArgs : EventArgs
    {
        public BookmarkEventArgs(Bookmark bookmark)
        {
            Bookmark = bookmark;
        }

        public Bookmark Bookmark { get; }
    }
}