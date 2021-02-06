// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Runtime.Serialization;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    [Serializable]
    public class HighlightingColorNotFoundException : Exception
    {
        public HighlightingColorNotFoundException()
        {
        }

        public HighlightingColorNotFoundException(string message) : base(message)
        {
        }

        public HighlightingColorNotFoundException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        protected HighlightingColorNotFoundException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}