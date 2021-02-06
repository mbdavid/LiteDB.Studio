// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.Selection
{
    public class ColumnRange
    {
        public static readonly ColumnRange NoColumn = new ColumnRange(-2, -2);
        public static readonly ColumnRange WholeColumn = new ColumnRange(-1, -1);

        public ColumnRange(int startColumn, int endColumn)
        {
            this.StartColumn = startColumn;
            this.EndColumn = endColumn;
        }

        public int StartColumn { get; set; }

        public int EndColumn { get; set; }

        public override int GetHashCode()
        {
            return StartColumn + (EndColumn << 16);
        }

        public override bool Equals(object obj)
        {
            if (obj is ColumnRange)
                return ((ColumnRange) obj).StartColumn == StartColumn &&
                       ((ColumnRange) obj).EndColumn == EndColumn;
            return false;
        }

        public override string ToString()
        {
            return string.Format("[ColumnRange: StartColumn={0}, EndColumn={1}]", StartColumn, EndColumn);
        }
    }
}