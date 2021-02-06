// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy;
using LiteDB.Studio.ICSharpCode.TextEditor.Gui;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    public class RtfWriter
    {
        private static Dictionary<string, int> colors;
        private static int colorNum;
        private static StringBuilder colorString;

        public static string GenerateRtf(TextArea textArea)
        {
            colors = new Dictionary<string, int>();
            colorNum = 0;
            colorString = new StringBuilder();


            var rtf = new StringBuilder();

            rtf.Append(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1031");
            BuildFontTable(textArea.Document, rtf);
            rtf.Append('\n');

            var fileContent = BuildFileContent(textArea);
            BuildColorTable(textArea.Document, rtf);
            rtf.Append('\n');
            rtf.Append(@"\viewkind4\uc1\pard");
            rtf.Append(fileContent);
            rtf.Append("}");
            return rtf.ToString();
        }

        private static void BuildColorTable(IDocument doc, StringBuilder rtf)
        {
            rtf.Append(@"{\colortbl ;");
            rtf.Append(colorString);
            rtf.Append("}");
        }

        private static void BuildFontTable(IDocument doc, StringBuilder rtf)
        {
            rtf.Append(@"{\fonttbl");
            rtf.Append(@"{\f0\fmodern\fprq1\fcharset0 " + doc.TextEditorProperties.Font.Name + ";}");
            rtf.Append("}");
        }

        private static string BuildFileContent(TextArea textArea)
        {
            var rtf = new StringBuilder();
            var firstLine = true;
            var curColor = Color.Black;
            var oldItalic = false;
            var oldBold = false;
            var escapeSequence = false;

            foreach (var selection in textArea.SelectionManager.SelectionCollection)
            {
                var selectionOffset = textArea.Document.PositionToOffset(selection.StartPosition);
                var selectionEndOffset = textArea.Document.PositionToOffset(selection.EndPosition);
                for (var i = selection.StartPosition.Y; i <= selection.EndPosition.Y; ++i)
                {
                    var line = textArea.Document.GetLineSegment(i);
                    var offset = line.Offset;
                    if (line.Words == null) continue;

                    foreach (var word in line.Words)
                        switch (word.Type)
                        {
                            case TextWordType.Space:
                                if (selection.ContainsOffset(offset)) rtf.Append(' ');
                                ++offset;
                                break;

                            case TextWordType.Tab:
                                if (selection.ContainsOffset(offset)) rtf.Append(@"\tab");
                                ++offset;
                                escapeSequence = true;
                                break;

                            case TextWordType.Word:
                                var c = word.Color;

                                if (offset + word.Word.Length > selectionOffset && offset < selectionEndOffset)
                                {
                                    var colorstr = c.R + ", " + c.G + ", " + c.B;

                                    if (!colors.ContainsKey(colorstr))
                                    {
                                        colors[colorstr] = ++colorNum;
                                        colorString.Append(@"\red" + c.R + @"\green" + c.G + @"\blue" + c.B + ";");
                                    }

                                    if (c != curColor || firstLine)
                                    {
                                        rtf.Append(@"\cf" + colors[colorstr]);
                                        curColor = c;
                                        escapeSequence = true;
                                    }

                                    if (oldItalic != word.Italic)
                                    {
                                        if (word.Italic)
                                            rtf.Append(@"\i");
                                        else
                                            rtf.Append(@"\i0");
                                        oldItalic = word.Italic;
                                        escapeSequence = true;
                                    }

                                    if (oldBold != word.Bold)
                                    {
                                        if (word.Bold)
                                            rtf.Append(@"\b");
                                        else
                                            rtf.Append(@"\b0");
                                        oldBold = word.Bold;
                                        escapeSequence = true;
                                    }

                                    if (firstLine)
                                    {
                                        rtf.Append(@"\f0\fs" + textArea.TextEditorProperties.Font.Size * 2);
                                        firstLine = false;
                                    }

                                    if (escapeSequence)
                                    {
                                        rtf.Append(' ');
                                        escapeSequence = false;
                                    }

                                    string printWord;
                                    if (offset < selectionOffset)
                                        printWord = word.Word.Substring(selectionOffset - offset);
                                    else if (offset + word.Word.Length > selectionEndOffset)
                                        printWord = word.Word.Substring(0,
                                            offset + word.Word.Length - selectionEndOffset);
                                    else
                                        printWord = word.Word;

                                    AppendText(rtf, printWord);
                                }

                                offset += word.Length;
                                break;
                        }

                    if (offset < selectionEndOffset) rtf.Append(@"\par");
                    rtf.Append('\n');
                }
            }

            return rtf.ToString();
        }

        private static void AppendText(StringBuilder rtfOutput, string text)
        {
            //rtf.Append(printWord.Replace(@"\", @"\\").Replace("{", "\\{").Replace("}", "\\}"));
            foreach (var c in text)
                switch (c)
                {
                    case '\\':
                        rtfOutput.Append(@"\\");
                        break;
                    case '{':
                        rtfOutput.Append("\\{");
                        break;
                    case '}':
                        rtfOutput.Append("\\}");
                        break;
                    default:
                        if (c < 256)
                            rtfOutput.Append(c);
                        else // yes, RTF really expects signed 16-bit integers!
                            rtfOutput.Append("\\u" + unchecked((short) c) + "?");
                        break;
                }
        }
    }
}