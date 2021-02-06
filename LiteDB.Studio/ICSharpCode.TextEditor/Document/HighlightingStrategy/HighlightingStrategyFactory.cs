// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    public class HighlightingStrategyFactory
    {
        public static IHighlightingStrategy CreateHighlightingStrategy()
        {
            return (IHighlightingStrategy) HighlightingManager.Manager.HighlightingDefinitions["Default"];
        }

        public static IHighlightingStrategy CreateHighlightingStrategy(string name)
        {
            var highlightingStrategy = HighlightingManager.Manager.FindHighlighter(name);

            if (highlightingStrategy == null) return CreateHighlightingStrategy();
            return highlightingStrategy;
        }

        public static IHighlightingStrategy CreateHighlightingStrategyForFile(string fileName)
        {
            var highlightingStrategy = HighlightingManager.Manager.FindHighlighterForFile(fileName);
            if (highlightingStrategy == null) return CreateHighlightingStrategy();
            return highlightingStrategy;
        }
    }
}