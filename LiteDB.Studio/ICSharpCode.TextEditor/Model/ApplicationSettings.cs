using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace ICSharpCode.TextEditor.Util.Model
{
    [Serializable]
    public class ApplicationSettings
    {
        public ConnectionString LastConnectionStrings { get; set; }
        public bool LoadLastDbOnStartup { get; set; }

        public ApplicationSettings()
        {
        }
    }
}
