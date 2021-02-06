// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Collections.Generic;
using System.Xml;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy.SyntaxModes
{
    public interface ISyntaxModeFileProvider
    {
        ICollection<SyntaxMode> SyntaxModes { get; }

        XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode);
        void UpdateSyntaxModeList();
    }
}