// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui.CompletionWindow
{
    public interface ICompletionData
    {
        int ImageIndex { get; }

        string Text { get; set; }

        string Description { get; }

        /// <summary>
        ///     Gets a priority value for the completion data item.
        ///     When selecting items by their start characters, the item with the highest
        ///     priority is selected first.
        /// </summary>
        double Priority { get; }

        /// <summary>
        ///     Insert the element represented by the completion data into the text
        ///     editor.
        /// </summary>
        /// <param name="textArea">TextArea to insert the completion data in.</param>
        /// <param name="ch">
        ///     Character that should be inserted after the completion data.
        ///     \0 when no character should be inserted.
        /// </param>
        /// <returns>
        ///     Returns true when the insert action has processed the character
        ///     <paramref name="ch" />; false when the character was not processed.
        /// </returns>
        bool InsertAction(TextArea textArea, char ch);
    }

    public class DefaultCompletionData : ICompletionData
    {
        public DefaultCompletionData(string text, int imageIndex)
        {
            Text = text;
            ImageIndex = imageIndex;
        }

        public DefaultCompletionData(string text, string description, int imageIndex)
        {
            Text = text;
            Description = description;
            ImageIndex = imageIndex;
        }

        public int ImageIndex { get; }

        public string Text { get; set; }

        public virtual string Description { get; }

        public double Priority { get; set; }

        public virtual bool InsertAction(TextArea textArea, char ch)
        {
            textArea.InsertString(Text);
            return false;
        }

        public static int Compare(ICompletionData a, ICompletionData b)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (b == null)
                throw new ArgumentNullException("b");
            return string.Compare(a.Text, b.Text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}