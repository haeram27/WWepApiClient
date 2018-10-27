///-----------------------------------------------------------------
///   ClassName:      ConfigHelper
///   Description:    helps to using application App.config files
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/22
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------

using System;
using System.Configuration;

namespace DcmSMService.Utils
{
    public static class Settings
    {
        public enum Key
        {
            AllowTrace
        }

        public static string Get(Key key)
        {
            return ConfigurationManager.AppSettings[Enum.Parse(typeof(Key), key.ToString()).ToString()];
        }
    }
}
