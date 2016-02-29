using System;
using System.Text;
using Microsoft.VisualBasic;

namespace Medivh.Logger
{
    internal class LogHelper
    {
        public static Action<string> Logger { get; set; }
        private static uint LogLevel { get; set; }
        public static void Init(Action<string> action, uint logLevel)
        {
            Logger = action;
            LogLevel = logLevel;
        }


        public static void Info(string msg)
        {
            msg = string.Format("[Info]:{0} ", msg);
            if (Logger != null)
            {
                Logger(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public static void Error(string msg)
        {
            msg = string.Format("[Error]:{0} ", msg);
            if (Logger != null)
            {
                Logger(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public static void Debug(string msg)
        {
            if (LogLevel == 0)
            {
                return;
            }
            msg = string.Format("[Debug]:{0} ", msg);
            if (Logger != null)
            {
                Logger(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }

        }

        public static void Error(Exception ex, string msg = "")
        {
            if (ex != null)
            {
                StringBuilder sb = new StringBuilder();
                sb = sb.Append(string.Format("{0}\n{1}\n", msg, ex.Message)).Append(ex.Data).Append(ex.Source).Append(ex.StackTrace);

                Error(sb.ToString());
                if (ex.InnerException != null)
                {
                    Error(ex.InnerException);
                }
            }
        }


    }
}
