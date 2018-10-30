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
using System.Globalization;
using System.IO;

namespace Foundations.Logger
{
    public static class Log
    {
        private const string TAG = "Log";

        /// <summary>
        /// To Enable trace, "Trace_Allowed" setting key and value are should be specified in App.config
        /// <appSettings>
        ///     <add key = "Trace_Allowed" value="false" />
        /// </appSettings>
        /// </summary>  
        static bool mIsAllowTrace = Boolean.Parse(Setting.Get(Setting.Key.Trace_Allowed));

        static Log()
        {
            string dirPath = Paths.tempPath;
            string appName = Paths.appName;
            string filePath = dirPath + appName + "_" + GetFileTime() + ".log";

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

        private static string GetFileTime()
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            return DateTime.Now.ToString(Foundations.Common.Constants.DateTimeFormatsForFileName, ci);
        }

        private static string GetLogTime()
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            return DateTime.Now.ToString(Foundations.Common.Constants.DateTimeFormatsForLog, ci);
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
            try
            { 
                if (mIsAllowTrace)
                {
                    Trace.TraceInformation(GetLogTime() + " : [" + tag + "] : " + msg);
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.Message);
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
            try
            {
                if (mIsAllowTrace)
                {
                    Trace.TraceWarning(GetLogTime() + " : [" + tag + "] : " + msg);
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.Message);
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
            try
            {
                if (mIsAllowTrace)
                {
                    Trace.TraceError(GetLogTime() + " : [" + tag + "] : " + msg);
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.Message);
            }
        }
    }
}
