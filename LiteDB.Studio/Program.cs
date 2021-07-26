using ICSharpCode.TextEditor.Util;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LiteDB.Studio
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                _ = SetProcessDPIAware();

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
