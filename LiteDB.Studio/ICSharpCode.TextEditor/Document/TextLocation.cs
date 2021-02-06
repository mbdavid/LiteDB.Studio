// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 2658$</version>
// </file>

using System;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document
{
    /// <summary>
    ///     A line/column position.
    ///     Text editor lines/columns are counting from zero.
    /// </summary>
    public struct TextLocation : IComparable<TextLocation>, IEquatable<TextLocation>
    {
        /// <summary>
        ///     Represents no text location (-1, -1).
        /// </summary>
        public static readonly TextLocation Empty = new TextLocation(-1, -1);

        public TextLocation(int column, int line)
        {
            X = column;
            Y = line;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Line
        {
            get => Y;
            set => Y = value;
        }

        public int Column
        {
            get => X;
            set => X = value;
        }

        public bool IsEmpty => X <= 0 && Y <= 0;

        public override string ToString()
        {
            return string.Format("(Line {1}, Col {0})", X, Y);
        }

        public override int GetHashCode()
        {
            return unchecked((87 * X.GetHashCode()) ^ Y.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TextLocation)) return false;
            return (TextLocation) obj == this;
        }

        public bool Equals(TextLocation other)
        {
            return this == other;
        }

        public static bool operator ==(TextLocation a, TextLocation b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(TextLocation a, TextLocation b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static bool operator <(TextLocation a, TextLocation b)
        {
            if (a.Y < b.Y)
                return true;
            if (a.Y == b.Y)
                return a.X < b.X;
            return false;
        }

        public static bool operator >(TextLocation a, TextLocation b)
        {
            if (a.Y > b.Y)
                return true;
            if (a.Y == b.Y)
                return a.X > b.X;
            return false;
        }

        public static bool operator <=(TextLocation a, TextLocation b)
        {
            return !(a > b);
        }

        public static bool operator >=(TextLocation a, TextLocation b)
        {
            return !(a < b);
        }

        public int CompareTo(TextLocation other)
        {
            if (this == other)
                return 0;
            if (this < other)
                return -1;
            return 1;
        }
    }
}