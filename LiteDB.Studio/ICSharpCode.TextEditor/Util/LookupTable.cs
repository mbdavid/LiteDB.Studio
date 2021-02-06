// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.LineManager;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    /// <summary>
    ///     This class implements a keyword map. It implements a digital search trees (tries) to find
    ///     a word.
    /// </summary>
    public class LookupTable
    {
        private readonly bool casesensitive;
        private readonly Node root = new Node(null, null);

        /// <summary>
        ///     Creates a new instance of <see cref="LookupTable" />
        /// </summary>
        public LookupTable(bool casesensitive)
        {
            this.casesensitive = casesensitive;
        }

        /// <value>
        ///     The number of elements in the table
        /// </value>
        public int Count { get; private set; }

        /// <summary>
        ///     Get the object, which was inserted under the keyword (line, at offset, with length length),
        ///     returns null, if no such keyword was inserted.
        /// </summary>
        public object this[IDocument document, LineSegment line, int offset, int length]
        {
            get
            {
                if (length == 0) return null;
                var next = root;

                var wordOffset = line.Offset + offset;
                if (casesensitive)
                    for (var i = 0; i < length; ++i)
                    {
                        var index = document.GetCharAt(wordOffset + i) % 256;
                        next = next[index];

                        if (next == null) return null;

                        if (next.color != null && TextUtility.RegionMatches(document, wordOffset, length, next.word))
                            return next.color;
                    }
                else
                    for (var i = 0; i < length; ++i)
                    {
                        var index = char.ToUpper(document.GetCharAt(wordOffset + i)) % 256;

                        next = next[index];

                        if (next == null) return null;

                        if (next.color != null &&
                            TextUtility.RegionMatches(document, casesensitive, wordOffset, length, next.word))
                            return next.color;
                    }

                return null;
            }
        }

        /// <summary>
        ///     Inserts an object in the tree, under keyword
        /// </summary>
        public object this[string keyword]
        {
            set
            {
                var node = root;
                var next = root;
                if (!casesensitive) keyword = keyword.ToUpper();
                ++Count;

                // insert word into the tree
                for (var i = 0; i < keyword.Length; ++i)
                {
                    var index = keyword[i] % 256; // index of curchar
                    var d = keyword[i] == '\\';

                    next = next[index]; // get node to this index

                    if (next == null)
                    {
                        // no node created -> insert word here
                        node[index] = new Node(value, keyword);
                        break;
                    }

                    if (next.word != null && next.word.Length != i)
                    {
                        // node there, take node content and insert them again
                        var tmpword =
                            next.word; // this word will be inserted 1 level deeper (better, don't need too much
                        var tmpcolor = next.color; // string comparisons for finding.)
                        next.color = next.word = null;
                        this[tmpword] = tmpcolor;
                    }

                    if (i == keyword.Length - 1)
                    {
                        // end of keyword reached, insert node there, if a node was here it was
                        next.word =
                            keyword; // reinserted, if it has the same length (keyword EQUALS this word) it will be overwritten
                        next.color = value;
                        break;
                    }

                    node = next;
                }
            }
        }

        private class Node
        {
            private Node[] children;
            public object color;

            public string word;

            public Node(object color, string word)
            {
                this.word = word;
                this.color = color;
            }

            // Lazily initialize children array. Saves 200 KB of memory for the C# highlighting
            // because we don't have to store the array for leaf nodes.
            public Node this[int index]
            {
                get
                {
                    if (children != null)
                        return children[index];
                    return null;
                }
                set
                {
                    if (children == null)
                        children = new Node[256];
                    children[index] = value;
                }
            }
        }
    }
}