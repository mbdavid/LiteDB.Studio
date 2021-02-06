// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy.SyntaxModes
{
    public class SyntaxMode
    {
        public SyntaxMode(string fileName, string name, string extensions)
        {
            this.FileName = fileName;
            this.Name = name;
            this.Extensions = extensions.Split(';', '|', ',');
        }

        public SyntaxMode(string fileName, string name, string[] extensions)
        {
            this.FileName = fileName;
            this.Name = name;
            this.Extensions = extensions;
        }

        public string FileName { get; set; }

        public string Name { get; set; }

        public string[] Extensions { get; set; }

        public static List<SyntaxMode> GetSyntaxModes(Stream xmlSyntaxModeStream)
        {
            var reader = new XmlTextReader(xmlSyntaxModeStream);
            var syntaxModes = new List<SyntaxMode>();
            while (reader.Read())
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "SyntaxModes":
                                var version = reader.GetAttribute("version");
                                if (version != "1.0")
                                    throw new HighlightingDefinitionInvalidException(
                                        "Unknown syntax mode file defininition with version " + version);
                                break;
                            case "Mode":
                                syntaxModes.Add(new SyntaxMode(reader.GetAttribute("file"),
                                    reader.GetAttribute("name"),
                                    reader.GetAttribute("extensions")));
                                break;
                            default:
                                throw new HighlightingDefinitionInvalidException("Unknown node in syntax mode file :" +
                                    reader.Name);
                        }

                        break;
                }

            reader.Close();
            return syntaxModes;
        }

        public override string ToString()
        {
            return string.Format("[SyntaxMode: FileName={0}, Name={1}, Extensions=({2})]", FileName, Name,
                string.Join(",", Extensions));
        }
    }
}