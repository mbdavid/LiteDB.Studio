// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using LiteDB.Studio.ICSharpCode.TextEditor.Document;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    public class TextUtility
    {
        public static bool RegionMatches(IDocument document, int offset, int length, string word)
        {
            if (length != word.Length || document.TextLength < offset + length) return false;

            for (var i = 0; i < length; ++i)
                if (document.GetCharAt(offset + i) != word[i])
                    return false;
            return true;
        }

        public static bool RegionMatches(IDocument document, bool casesensitive, int offset, int length, string word)
        {
            if (casesensitive) return RegionMatches(document, offset, length, word);

            if (length != word.Length || document.TextLength < offset + length) return false;

            for (var i = 0; i < length; ++i)
                if (char.ToUpper(document.GetCharAt(offset + i)) != char.ToUpper(word[i]))
                    return false;
            return true;
        }

        public static bool RegionMatches(IDocument document, int offset, int length, char[] word)
        {
            if (length != word.Length || document.TextLength < offset + length) return false;

            for (var i = 0; i < length; ++i)
                if (document.GetCharAt(offset + i) != word[i])
                    return false;
            return true;
        }

        public static bool RegionMatches(IDocument document, bool casesensitive, int offset, int length, char[] word)
        {
            if (casesensitive) return RegionMatches(document, offset, length, word);

            if (length != word.Length || document.TextLength < offset + length) return false;

            for (var i = 0; i < length; ++i)
                if (char.ToUpper(document.GetCharAt(offset + i)) != char.ToUpper(word[i]))
                    return false;
            return true;
        }
    }
}