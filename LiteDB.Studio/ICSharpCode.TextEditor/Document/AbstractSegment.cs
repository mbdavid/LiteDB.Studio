// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document
{
    /// <summary>
    ///     This interface is used to describe a span inside a text sequence
    /// </summary>
    public class AbstractSegment : ISegment
    {
        protected int length = -1;
        protected int offset = -1;

        public override string ToString()
        {
            return string.Format("[AbstractSegment: Offset = {0}, Length = {1}]",
                Offset,
                Length);
        }

        #region ICSharpCode.TextEditor.Document.ISegment interface implementation

        public virtual int Offset
        {
            get => offset;
            set => offset = value;
        }

        public virtual int Length
        {
            get => length;
            set => length = value;
        }

        #endregion
    }
}