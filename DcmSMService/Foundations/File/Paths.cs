///-----------------------------------------------------------------
///   ClassName:      Paths
///   Description:    Run task repeatly with interval
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/30
///   Notes:          to get Applcation and Environment Paths
///   Revision History:
///-----------------------------------------------------------------

using System;

namespace Foundations.File
{
    public static class Paths
    {
        // Name of this application
        public static readonly string appName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

        // Path to save log file: System Temp directory 
        public static readonly string tempPath = @"c:\Temp\";

        // Path to save System %APPDATA% directory
        // To get more Winodws's SpecialFolder Info, please check Environment.SpecialFolder enum
        public static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Path to save this application's directory in %APPDATA%
        public static readonly string appDataDedicatedPath = appDataPath+ @"\" + appName;
    }
}
