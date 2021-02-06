// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Collections.Generic;
using System.Xml;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.BookmarkManager
{
    /// <summary>
    ///     This class is used for storing the state of a bookmark manager
    /// </summary>
    public class BookmarkManagerMemento
    {
        /// <summary>
        ///     Creates a new instance of <see cref="BookmarkManagerMemento" />
        /// </summary>
        public BookmarkManagerMemento()
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="BookmarkManagerMemento" />
        /// </summary>
        public BookmarkManagerMemento(XmlElement element)
        {
            foreach (XmlElement el in element.ChildNodes) Bookmarks.Add(int.Parse(el.Attributes["line"].InnerText));
        }

        /// <summary>
        ///     Creates a new instance of <see cref="BookmarkManagerMemento" />
        /// </summary>
        public BookmarkManagerMemento(List<int> bookmarks)
        {
            Bookmarks = bookmarks;
        }

        /// <value>
        ///     Contains all bookmarks as int values
        /// </value>
        public List<int> Bookmarks { get; set; } = new List<int>();

        /// <summary>
        ///     Validates all bookmarks if they're in range of the document.
        ///     (removing all bookmarks &lt; 0 and bookmarks &gt; max. line number
        /// </summary>
        public void CheckMemento(IDocument document)
        {
            for (var i = 0; i < Bookmarks.Count; ++i)
            {
                var mark = Bookmarks[i];
                if (mark < 0 || mark >= document.TotalNumberOfLines)
                {
                    Bookmarks.RemoveAt(i);
                    --i;
                }
            }
        }

        /// <summary>
        ///     Converts a xml element to a <see cref="BookmarkManagerMemento" /> object
        /// </summary>
        public object FromXmlElement(XmlElement element)
        {
            return new BookmarkManagerMemento(element);
        }

        /// <summary>
        ///     Converts this <see cref="BookmarkManagerMemento" /> to a xml element
        /// </summary>
        public XmlElement ToXmlElement(XmlDocument doc)
        {
            var bookmarknode = doc.CreateElement("Bookmarks");

            foreach (var line in Bookmarks)
            {
                var markNode = doc.CreateElement("Mark");

                var lineAttr = doc.CreateAttribute("line");
                lineAttr.InnerText = line.ToString();
                markNode.Attributes.Append(lineAttr);

                bookmarknode.AppendChild(markNode);
            }

            return bookmarknode;
        }
    }
}