// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System.Drawing;
using LiteDB.Studio.ICSharpCode.TextEditor.Document;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    public delegate void ToolTipRequestEventHandler(object sender, ToolTipRequestEventArgs e);

    public class ToolTipRequestEventArgs
    {
        internal string toolTipText;

        public ToolTipRequestEventArgs(Point mousePosition, TextLocation logicalPosition, bool inDocument)
        {
            MousePosition = mousePosition;
            LogicalPosition = logicalPosition;
            InDocument = inDocument;
        }

        public Point MousePosition { get; }

        public TextLocation LogicalPosition { get; }

        public bool InDocument { get; }

        /// <summary>
        ///     Gets if some client handling the event has already shown a tool tip.
        /// </summary>
        public bool ToolTipShown => toolTipText != null;

        public void ShowToolTip(string text)
        {
            toolTipText = text;
        }
    }
}