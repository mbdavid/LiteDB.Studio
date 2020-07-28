using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Util;

namespace LiteDB.Studio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ApplicationExit += OnExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args.Length == 0 ? null : args[0]));
        }

        private static void OnExit(object sender, EventArgs eventArgs)
        {
            Application.ApplicationExit -= OnExit;
            AppSettingsManager.PersistData();
        }
    }
}
