using System;
using System.IO;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    public enum LoggingLevel
    {
        Fatal = 0x1,
        Debug = 0x3,
        Trace = 0x7
    }

    public static class Logger
    {
        static private LoggingLevel _level = LoggingLevel.Fatal;
        static private Object _lock = new Object();

        public static LoggingLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// Only use fatal when program is unrecoverable
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Fatal(string msg, params object[] args)
        {
            Fatal(string.Format(msg, args));
        }

        /// <summary>
        /// Only use fatal when program is unrecoverable
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            write(msg, LoggingLevel.Fatal);
        }

        public static void Debug(string msg, params object[] args)
        {
            Debug(string.Format(msg, args));
        }

        public static void Debug(string msg)
        {
            write(msg, LoggingLevel.Debug);
        }

        public static void Trace(string msg, params object[] args)
        {
            Trace(string.Format(msg, args));
        }

        public static void Trace(string msg)
        {
            write(msg, LoggingLevel.Trace);
        }

        private static void write(string msg, LoggingLevel msgLevel)
        {
            if ((_level & msgLevel) == msgLevel)
            {
                lock (_lock)
                {
                    TextWriter tw = new StreamWriter(Properties.Resources.logFileName, true);
                    tw.WriteLine(string.Format(Properties.Resources.logFormat, DateTime.Now, msgLevel.ToString(), msg));
                    tw.Close();
                }
            }
        }
    }
}