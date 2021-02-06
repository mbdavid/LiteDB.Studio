using System;
using System.Collections.Generic;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Model
{
    [Serializable]
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            RecentConnectionStrings = new List<ConnectionString>();
        }

        public ConnectionString LastConnectionStrings { get; set; }
        public List<ConnectionString> RecentConnectionStrings { get; set; }

        public int MaxRecentListItems { get; set; } = 10;
        public bool LoadLastDbOnStartup { get; set; }
    }
}