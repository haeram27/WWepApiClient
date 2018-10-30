///-----------------------------------------------------------------
///   ClassName:      Setting
///   Description:    helps to using application App.config files
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/22
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------

using System;
using System.Configuration;

namespace Foundations.Configuration
{
    public static class Setting
    {
        public enum Key
        {
            Trace_Allowed,
            Trace_Console_Enabled,
            Trace_Textfile_Enalbed
        }

        public static string Get(Key key)
        {
            return ConfigurationManager.AppSettings[Enum.Parse(typeof(Key), key.ToString()).ToString()];
        }
    }
}
