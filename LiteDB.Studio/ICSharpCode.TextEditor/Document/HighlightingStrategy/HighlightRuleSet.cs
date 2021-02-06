// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Collections;
using System.Xml;
using LiteDB.Studio.ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    public class HighlightRuleSet
    {
        internal IHighlightingStrategyUsingRuleSets Highlighter;

        public HighlightRuleSet()
        {
            KeyWords = new LookupTable(false);
            PrevMarkers = new LookupTable(false);
            NextMarkers = new LookupTable(false);
        }

        public HighlightRuleSet(XmlElement el)
        {
            XmlNodeList nodes;

            if (el.Attributes["name"] != null) Name = el.Attributes["name"].InnerText;

            if (el.HasAttribute("escapecharacter")) EscapeCharacter = el.GetAttribute("escapecharacter")[0];

            if (el.Attributes["reference"] != null) Reference = el.Attributes["reference"].InnerText;

            if (el.Attributes["ignorecase"] != null) IgnoreCase = bool.Parse(el.Attributes["ignorecase"].InnerText);

            for (var i = 0; i < Delimiters.Length; ++i) Delimiters[i] = false;

            if (el["Delimiters"] != null)
            {
                var delimiterString = el["Delimiters"].InnerText;
                foreach (var ch in delimiterString) Delimiters[ch] = true;
            }

//			Spans       = new LookupTable(!IgnoreCase);

            KeyWords = new LookupTable(!IgnoreCase);
            PrevMarkers = new LookupTable(!IgnoreCase);
            NextMarkers = new LookupTable(!IgnoreCase);

            nodes = el.GetElementsByTagName("KeyWords");
            foreach (XmlElement el2 in nodes)
            {
                var color = new HighlightColor(el2);

                var keys = el2.GetElementsByTagName("Key");
                foreach (XmlElement node in keys) KeyWords[node.Attributes["word"].InnerText] = color;
            }

            nodes = el.GetElementsByTagName("Span");
            foreach (XmlElement el2 in nodes) Spans.Add(new Span(el2));
            /*
				Span span = new Span(el2);
				Spans[span.Begin] = span;*/

            nodes = el.GetElementsByTagName("MarkPrevious");
            foreach (XmlElement el2 in nodes)
            {
                var prev = new PrevMarker(el2);
                PrevMarkers[prev.What] = prev;
            }

            nodes = el.GetElementsByTagName("MarkFollowing");
            foreach (XmlElement el2 in nodes)
            {
                var next = new NextMarker(el2);
                NextMarkers[next.What] = next;
            }
        }

        public ArrayList Spans { get; private set; } = new ArrayList();

        public LookupTable KeyWords { get; }

        public LookupTable PrevMarkers { get; }

        public LookupTable NextMarkers { get; }

        public bool[] Delimiters { get; } = new bool[256];

        public char EscapeCharacter { get; }

        public bool IgnoreCase { get; }

        public string Name { get; set; }

        public string Reference { get; }

        /// <summary>
        ///     Merges spans etc. from the other rule set into this rule set.
        /// </summary>
        public void MergeFrom(HighlightRuleSet ruleSet)
        {
            for (var i = 0; i < Delimiters.Length; i++) Delimiters[i] |= ruleSet.Delimiters[i];
            // insert merged spans in front of old spans
            var oldSpans = Spans;
            Spans = (ArrayList) ruleSet.Spans.Clone();
            Spans.AddRange(oldSpans);
            //keyWords.MergeFrom(ruleSet.keyWords);
            //prevMarkers.MergeFrom(ruleSet.prevMarkers);
            //nextMarkers.MergeFrom(ruleSet.nextMarkers);
        }
    }
}