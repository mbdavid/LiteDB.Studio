using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.TextEditor.Util.Model;
using LiteDB;
using LiteDB.Studio.Properties;
using Newtonsoft.Json;

namespace ICSharpCode.TextEditor.Util
{
    public static class AppSettingsManager
    {
        public static ApplicationSettings ApplicationSettings { get; set; }
        static AppSettingsManager()
        {
            if (string.IsNullOrEmpty(Settings.Default.ApplicationSettings))
            {
                ApplicationSettings = new ApplicationSettings();
                ReplaceApplicationSettings(ApplicationSettings);
            }
            else
            {
                ApplicationSettings =
                    JsonConvert.DeserializeObject<ApplicationSettings>(Settings.Default.ApplicationSettings);
            }
        }

        private static void ReplaceApplicationSettings(ApplicationSettings applicationSettings = null)
        {
            if (applicationSettings == null)
            {
                Settings.Default.ApplicationSettings = JsonConvert.SerializeObject(ApplicationSettings);
                Settings.Default.Save();
            }
            else
            {
                Settings.Default.ApplicationSettings = JsonConvert.SerializeObject(applicationSettings);
                Settings.Default.Save();
                ApplicationSettings = applicationSettings;
            }
        }

        public static bool IsLastDbExist()
        {
            if (ApplicationSettings.LastConnectionStrings == null)
            {
                return false;
            }
            var ldb = ApplicationSettings.LastConnectionStrings.Filename;
            return !string.IsNullOrEmpty(ldb) && File.Exists(ldb);
        }

        public static bool IsDbExist(string db)
        {
            return !string.IsNullOrEmpty(db) && File.Exists(db);
        }

        public static void PersistData()
        {
            ReplaceApplicationSettings();
        }

        public static void AddToRecentList(ConnectionString connectionString)
        {
            // check duplication
            var connection = ApplicationSettings.RecentConnectionStrings.FirstOrDefault(cs => cs.Filename == connectionString.Filename);
            if (connection != null)
            {
                // remove old
                ApplicationSettings.RecentConnectionStrings.Remove(connection);
                // add new to the top
                ApplicationSettings.RecentConnectionStrings = 
                    new List<ConnectionString>(ApplicationSettings.RecentConnectionStrings.Prepend(connectionString));
            }
            else
            {
                ApplicationSettings.RecentConnectionStrings.Add(connectionString);
            }
        }
        
        /// <summary>
        /// Remove any item from recent list if it does not exist
        /// </summary>
        public static void ValidateRecentList()
        {
            foreach (var connectionString in ApplicationSettings.RecentConnectionStrings.Where(connectionString => !IsDbExist(connectionString.Filename)))
            {
                File.Delete(connectionString.Filename);
            }
        }

        public static void ClearRecentList()
        {
            ApplicationSettings.RecentConnectionStrings.Clear();
        }

    }
}