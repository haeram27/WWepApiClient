///-----------------------------------------------------------------
///   ClassName:      Log
///   Description:    helps to make application log file
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/22
///   Notes:          To config enable logging, "System.Configuration.DLL" is required as References(Dependency)
///   Revision History:
///-----------------------------------------------------------------

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace DcmSMService.Utils
{
    public static class Log
    {
        /// <summary>
        /// To Enable trace on file and console "AllowTrace" setting key and value are should be specified in App.config
        /// <appSettings>
        ///     <add key = "AllowTrace" value="false" />
        /// </appSettings>
        /// </summary>  
        static bool mIsAllowTrace = Boolean.Parse(ConfigurationManager.AppSettings[ConfigHelper.KEY_APPCONF_ALLOW_TRACE]);
            
        static Log()
        {
            string appName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            //Path to save log file: System Temp directory 
            string dirPath = @"c:\Temp\";
            //Path to save log file: %APPDATA% directory
            //string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + AppName + @"\";
            string filePath = dirPath + appName + "_" + GetTime() + ".log";

            DirectoryInfo di = new DirectoryInfo(dirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            Trace.Listeners.Clear();
            Trace.Listeners.Add(new ConsoleTraceListener());
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
