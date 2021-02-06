// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System.Xml;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy
{
    public sealed class Span
    {
        private readonly HighlightColor beginColor;
        private readonly HighlightColor endColor;

        public Span(XmlElement span)
        {
            Color = new HighlightColor(span);

            if (span.HasAttribute("rule")) Rule = span.GetAttribute("rule");

            if (span.HasAttribute("escapecharacter")) EscapeCharacter = span.GetAttribute("escapecharacter")[0];

            Name = span.GetAttribute("name");
            if (span.HasAttribute("stopateol")) StopEOL = bool.Parse(span.GetAttribute("stopateol"));

            Begin = span["Begin"].InnerText.ToCharArray();
            beginColor = new HighlightColor(span["Begin"], Color);

            if (span["Begin"].HasAttribute("singleword"))
                IsBeginSingleWord = bool.Parse(span["Begin"].GetAttribute("singleword"));
            if (span["Begin"].HasAttribute("startofline"))
                IsBeginStartOfLine = bool.Parse(span["Begin"].GetAttribute("startofline"));

            if (span["End"] != null)
            {
                End = span["End"].InnerText.ToCharArray();
                endColor = new HighlightColor(span["End"], Color);
                if (span["End"].HasAttribute("singleword"))
                    IsEndSingleWord = bool.Parse(span["End"].GetAttribute("singleword"));
            }
        }

        internal HighlightRuleSet RuleSet { get; set; }

        public bool IgnoreCase { get; set; }

        public bool StopEOL { get; }

        public bool? IsBeginStartOfLine { get; }

        public bool IsBeginSingleWord { get; }

        public bool IsEndSingleWord { get; }

        public HighlightColor Color { get; }

        public HighlightColor BeginColor
        {
            get
            {
                if (beginColor != null)
                    return beginColor;
                return Color;
            }
        }

        public HighlightColor EndColor => endColor != null ? endColor : Color;

        public char[] Begin { get; }

        public char[] End { get; }

        public string Name { get; }

        public string Rule { get; }

        /// <summary>
        ///     Gets the escape character of the span. The escape character is a character that can be used in front
        ///     of the span end to make it not end the span. The escape character followed by another escape character
        ///     means the escape character was escaped like in @"a "" b" literals in C#.
        ///     The default value '\0' means no escape character is allowed.
        /// </summary>
        public char EscapeCharacter { get; }
    }
}