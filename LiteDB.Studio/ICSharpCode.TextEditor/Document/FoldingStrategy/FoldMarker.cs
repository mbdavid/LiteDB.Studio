// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.FoldingStrategy
{
    public enum FoldType
    {
        Unspecified,
        MemberBody,
        Region,
        TypeBody
    }

    public class FoldMarker : ISegment, IComparable
    {
        private readonly IDocument document;

        protected int length = -1;
        protected int offset = -1;
        private int startLine = -1, startColumn, endLine = -1, endColumn;

        public FoldMarker(IDocument document, int offset, int length, string foldText, bool isFolded)
        {
            this.document = document;
            this.offset = offset;
            this.length = length;
            FoldText = foldText;
            IsFolded = isFolded;
        }

        public FoldMarker(IDocument document, int startLine, int startColumn, int endLine, int endColumn) : this(
            document, startLine, startColumn, endLine, endColumn, FoldType.Unspecified)
        {
        }

        public FoldMarker(IDocument document, int startLine, int startColumn, int endLine, int endColumn,
            FoldType foldType) : this(document, startLine, startColumn, endLine, endColumn, foldType, "...")
        {
        }

        public FoldMarker(IDocument document, int startLine, int startColumn, int endLine, int endColumn,
            FoldType foldType, string foldText) : this(document, startLine, startColumn, endLine, endColumn, foldType,
            foldText, false)
        {
        }

        public FoldMarker(IDocument document, int startLine, int startColumn, int endLine, int endColumn,
            FoldType foldType, string foldText, bool isFolded)
        {
            this.document = document;

            startLine = Math.Min(document.TotalNumberOfLines - 1, Math.Max(startLine, 0));
            ISegment startLineSegment = document.GetLineSegment(startLine);

            endLine = Math.Min(document.TotalNumberOfLines - 1, Math.Max(endLine, 0));
            ISegment endLineSegment = document.GetLineSegment(endLine);

            // Prevent the region from completely disappearing
            if (string.IsNullOrEmpty(foldText)) foldText = "...";

            FoldType = foldType;
            FoldText = foldText;
            offset = startLineSegment.Offset + Math.Min(startColumn, startLineSegment.Length);
            length = endLineSegment.Offset + Math.Min(endColumn, endLineSegment.Length) - offset;
            IsFolded = isFolded;
        }

        public FoldType FoldType { get; set; } = FoldType.Unspecified;

        public int StartLine
        {
            get
            {
                if (startLine < 0) GetPointForOffset(document, offset, out startLine, out startColumn);
                return startLine;
            }
        }

        public int StartColumn
        {
            get
            {
                if (startLine < 0) GetPointForOffset(document, offset, out startLine, out startColumn);
                return startColumn;
            }
        }

        public int EndLine
        {
            get
            {
                if (endLine < 0) GetPointForOffset(document, offset + length, out endLine, out endColumn);
                return endLine;
            }
        }

        public int EndColumn
        {
            get
            {
                if (endLine < 0) GetPointForOffset(document, offset + length, out endLine, out endColumn);
                return endColumn;
            }
        }

        public bool IsFolded { get; set; }

        public string FoldText { get; } = "...";

        public string InnerText => document.GetText(offset, length);

        public int CompareTo(object o)
        {
            if (!(o is FoldMarker)) throw new ArgumentException();
            var f = (FoldMarker) o;
            if (offset != f.offset) return offset.CompareTo(f.offset);

            return length.CompareTo(f.length);
        }

        public override string ToString()
        {
            return string.Format("[FoldMarker: Offset = {0}, Length = {1}]",
                offset,
                length);
        }

        private static void GetPointForOffset(IDocument document, int offset, out int line, out int column)
        {
            if (offset > document.TextLength)
            {
                line = document.TotalNumberOfLines + 1;
                column = 1;
            }
            else if (offset < 0)
            {
                line = -1;
                column = -1;
            }
            else
            {
                line = document.GetLineNumberForOffset(offset);
                column = offset - document.GetLineSegment(line).Offset;
            }
        }

        #region ICSharpCode.TextEditor.Document.ISegment interface implementation

        public int Offset
        {
            get => offset;
            set
            {
                offset = value;
                startLine = -1;
                endLine = -1;
            }
        }

        public int Length
        {
            get => length;
            set
            {
                length = value;
                endLine = -1;
            }
        }

        #endregion
    }
}