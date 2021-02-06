// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy.SyntaxModes
{
    public class FileSyntaxModeProvider : ISyntaxModeFileProvider
    {
        private readonly string directory;
        private List<SyntaxMode> syntaxModes;

        public FileSyntaxModeProvider(string directory)
        {
            this.directory = directory;
            UpdateSyntaxModeList();
        }

        public ICollection<SyntaxMode> SyntaxModes => syntaxModes;

        public void UpdateSyntaxModeList()
        {
            var syntaxModeFile = Path.Combine(directory, "SyntaxModes.xml");
            if (File.Exists(syntaxModeFile))
            {
                Stream s = File.OpenRead(syntaxModeFile);
                syntaxModes = SyntaxMode.GetSyntaxModes(s);
                s.Close();
            }
            else
            {
                syntaxModes = ScanDirectory(directory);
            }
        }

        public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
        {
            var syntaxModeFile = Path.Combine(directory, syntaxMode.FileName);
            if (!File.Exists(syntaxModeFile))
                throw new HighlightingDefinitionInvalidException("Can't load highlighting definition " +
                                                                 syntaxModeFile + " (file not found)!");
            return new XmlTextReader(File.OpenRead(syntaxModeFile));
        }

        private List<SyntaxMode> ScanDirectory(string directory)
        {
            var files = Directory.GetFiles(directory);
            var modes = new List<SyntaxMode>();
            foreach (var file in files)
                if (Path.GetExtension(file).Equals(".XSHD", StringComparison.OrdinalIgnoreCase))
                {
                    var reader = new XmlTextReader(file);
                    while (reader.Read())
                        if (reader.NodeType == XmlNodeType.Element)
                            switch (reader.Name)
                            {
                                case "SyntaxDefinition":
                                    var name = reader.GetAttribute("name");
                                    var extensions = reader.GetAttribute("extensions");
                                    modes.Add(new SyntaxMode(Path.GetFileName(file),
                                        name,
                                        extensions));
                                    goto bailout;
                                default:
                                    throw new HighlightingDefinitionInvalidException(
                                        "Unknown root node in syntax highlighting file :" + reader.Name);
                            }

                    bailout:
                    reader.Close();
                }

            return modes;
        }
    }
}