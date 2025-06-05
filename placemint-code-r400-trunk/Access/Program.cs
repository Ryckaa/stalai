using System;
using System.Windows.Forms;
using System.Reflection;

namespace PlaceMint.Access
{
    using PlaceMint.Manager;
    using PlaceMint.Access.Properties;
    using System.Threading;
    static class Program
    {
        public static readonly string TITLE_BASE = string.Format(Resources.mainGUITitle, version());
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(PlaceMint_fatalUnhandled);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(PlaceMint_fatalUI);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //check for help argument
            foreach (string arg in args)
            {
                if (arg == "/?")
                {
                    msgBoxShow(null, Resources.help);
                    return;
                }
            }
            Application.Run(new MainGUI(args));
        }

        public static string version()
        {
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            return v.Major + "." + v.Minor;
        }

        public static string systemInfo()
        {
            return string.Format("  Version:{0}\n  System:{1}\n  .NET version:{2}", Program.versionFull(),
                Environment.OSVersion.VersionString, Environment.Version);
        }

        public static string versionFull()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private static void PlaceMint_fatalUI(object sender, ThreadExceptionEventArgs t)
        {
            PlaceMint_fatal(t.Exception, "UI");
        }

        private static void PlaceMint_fatalUnhandled(object sender, UnhandledExceptionEventArgs e)
        {
            PlaceMint_fatal(e.ExceptionObject, "Application");
        }

        private static void PlaceMint_fatal(Object e, String thread)
        {
            try
            {
                Exception ex = (Exception)e;
                MessageBox.Show("PlaceMint has encountered an error and must close. Please contact the developer about this issue."
                    + "There is a log file with more detials for the developer.\nError Message:\n\n" + ex.GetType()
                    + "\n" + ex.Message, "Fatal Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                String message = (ex.Message != null) ? ex.Message : "No exception message";
                String stackTrace = (ex.StackTrace != null) ? ex.StackTrace : "No stack trace";
                String targetSite = (ex.TargetSite != null) ? ex.TargetSite.ToString() : "No target site";
                Logger.Fatal("Exception Type: {0}", ex.GetType());
                String data = "";
                if (ex.Data == null)
                {
                    data = "Exception Data: No exception data";
                }
                else
                {
                    foreach (object o in ex.Data)
                    {
                        data += o.ToString() + "\n";
                    }
                }
                Logger.Fatal("--- PlaceMint Fatal Exception ---\n{0}\n  Thread:{1}\n  Exception Type:{2}\n"
                    + "  Exception Message:{3}\n  Stack Trace:{4}\n"
                    + "  Target Site:{5}\n  Exception Data:{6}\n",
                    systemInfo(), thread, ex.GetType(), message, stackTrace, targetSite, data);
            }
            finally
            {
                Logger.Fatal("End level log");
                Application.Exit();
            }
        }

        public enum MessageLevel { None, Warning, Error }

        public static void msgBoxShow(IWin32Window win, string msg)
        {
            msgBoxShow(win, msg, MessageLevel.Error);
        }

        public static void msgBoxShow(IWin32Window win, string msg, MessageLevel level)
        {
            msgBoxShow(win, msg, " " + ((level != MessageLevel.None) ? level.ToString() : ""));
        }

        public static void msgBoxShow(IWin32Window win, string msg, string titleAppend)
        {
            MessageBox.Show(win, msg, TITLE_BASE + titleAppend);
        }
    }
}