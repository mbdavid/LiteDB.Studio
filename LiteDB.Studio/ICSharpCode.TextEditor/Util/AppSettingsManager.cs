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

        public static void SetLastDb(string path, bool readOnly, string password, ConnectionType connectionType)
        {
            Properties.Settings.Default.LastDbPath = path;
            Properties.Settings.Default.LastDbReadOnly = readOnly;
            Properties.Settings.Default.LastDbPassword = password;
            Properties.Settings.Default.LastDbConnectionType = connectionType;
            Properties.Settings.Default.Save();
        }

        public static (string path, bool readOnly, string password, ConnectionType connectionType) GetLastDb()
        {
            return (
                Properties.Settings.Default.LastDbPath,
                Properties.Settings.Default.LastDbReadOnly,
                Properties.Settings.Default.LastDbPassword,
                Properties.Settings.Default.LastDbConnectionType
            );
        }
    }
}