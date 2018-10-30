///-----------------------------------------------------------------
///   ClassName:      Log
///   Description:    to help making application log file
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/22
///   Notes:          To config enable logging, "System.Configuration.DLL" 
///                   is required as References(Dependency) to read App.config 
///   Revision History:
///-----------------------------------------------------------------

using Foundations.Configuration;
using Foundations.File;
using System;
using System.Diagnostics;
using System.IO;

namespace Foundations.Logger
{
    public static class Log
    {
        /// <summary>
        /// To Enable trace, "Trace_Allowed" setting key and value are should be specified in App.config
        /// <appSettings>
        ///     <add key = "Trace_Allowed" value="false" />
        /// </appSettings>
        /// </summary>  
        static bool mIsAllowTrace = Boolean.Parse(Setting.Get(Setting.Key.Trace_Allowed));

        static Log()
        {
            string dirpath = Paths.tempPath;
            string appName = Paths.appName;
            string filePath = dirPath + appName + "_" + GetTime() + ".log";

            DirectoryInfo di = new DirectoryInfo(dirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            Trace.Listeners.Clear();
            //to print log on Console window. use ConsoleTraceListener when this app is running as ConsoleApplication
            Trace.Listeners.Add(new ConsoleTraceListener());
            //to print log on Console window. use TextWriterTraceListener when this app is running as without window for UI.
            Trace.Listeners.Add(new TextWriterTraceListener(filePath));
            Trace.AutoFlush = true;
        }

        private static string GetTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// to record infomation level logs
        /// 
        /// usage:
        ///     Log.Info("MyTag", "Message");
        /// </summary>
        /// <param name="tag"> identifier of this message </param>
        /// <param name="msg"> content string </param>
        public static void Info(string tag, string msg)
        {
            if (mIsAllowTrace)
            {
                Trace.TraceInformation(GetTime() + " : [" + tag + "] : " + msg);
            }
        }

        /// <summary>
        /// to record warning level logs
        /// 
        /// usage:
        ///     Log.Warn("MyTag", "Message");
        /// </summary>
        /// <param name="tag"> identifier of this message </param>
        /// <param name="msg"> content string </param>
        public static void Warn(string tag, string msg)
        {
            if (mIsAllowTrace)
            {
                Trace.TraceWarning(GetTime() + " : [" + tag + "] : " + msg);
            }
        }

        /// <summary>
        /// to record error level logs
        /// 
        /// usage:
        ///     Log.Error("MyTag", "Message");
        /// </summary>
        /// <param name="tag"> identifier of this message </param>
        /// <param name="msg"> content string </param>
        public static void Error(string tag, string msg)
        {
            if (mIsAllowTrace)
            {
                Trace.TraceError(GetTime() + " : [" + tag + "] : " + msg);
            }           
        }
    }
}
