using System.ComponentModel;
using LiteDB.Studio.ICSharpCode.TextEditor.Document.HighlightingStrategy;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Gui
{
    public class HighlightStringConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(HighlightingManager.Manager.HighlightingDefinitions.Keys);
        }
    }
}