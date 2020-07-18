using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    public static class AppSettingsManager
    {
        public static bool IsLoadLastDbEnabled()
        {
            return Properties.Settings.Default.LoadLastDb;
        }

        public static void SetLoadLastDb(bool enable)
        {
            Properties.Settings.Default.LoadLastDb = enable;
            Properties.Settings.Default.Save();
        }

        public static void SetLastDbPath(string path)
        {
            Properties.Settings.Default.LastDbPath = path;
            Properties.Settings.Default.Save();
        }
    }
}
