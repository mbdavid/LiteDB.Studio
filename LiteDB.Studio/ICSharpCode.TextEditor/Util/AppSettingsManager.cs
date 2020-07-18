using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using LiteDB.Studio.Properties;

namespace ICSharpCode.TextEditor.Util
{
    public static class AppSettingsManager
    {
        public static bool IsLoadLastDbEnabled()
        {
            return Settings.Default.LoadLastDb;
        }

        public static void SetLoadLastDb(bool enable)
        {
            Settings.Default.LoadLastDb = enable;
            Settings.Default.Save();
        }

        public static void SetLastDb(string path, bool readOnly, string password, ConnectionType connectionType)
        {
            Settings.Default.LastDbPath = path;
            Settings.Default.LastDbReadOnly = readOnly;
            Settings.Default.LastDbPassword = password;
            Settings.Default.LastDbConnectionType = connectionType;
            Settings.Default.Save();
        }

        public static (string path, bool readOnly, string password, ConnectionType connectionType) GetLastDb()
        {
            return (
                Settings.Default.LastDbPath,
                Settings.Default.LastDbReadOnly,
                Settings.Default.LastDbPassword,
                Settings.Default.LastDbConnectionType
            );
        }

        public static ConnectionString GetLastDbConnectionString()
        {
            // we must do this, because if we even pass "" as an empty password: this will thrown an exception
            var password = string.IsNullOrEmpty(Settings.Default.LastDbPassword) ? null : Settings.Default.LastDbPassword;

            return new ConnectionString
            {
                Filename = Settings.Default.LastDbPath,
                Password = password,
                ReadOnly = Settings.Default.LastDbReadOnly,
                Connection = Settings.Default.LastDbConnectionType
            };
        }

        public static bool IsLastDbExist()
        {
            var ldb = Settings.Default.LastDbPath;
            return !string.IsNullOrEmpty(ldb) && File.Exists(ldb);
        }
    }
}