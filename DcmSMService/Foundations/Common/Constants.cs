///-----------------------------------------------------------------
///   ClassName:      Constants
///   Description:    This class contains all the common application constant variables
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/30
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------

namespace Foundations.Common
{
    public static class Constants
    {
        public const string NotAvailable = "Not_Available";

        public const string DateTimeFormats = "MM-dd-yyyy hh:mm:ss:fff tt";
        public const string DateTimeFormatsForLog = "MM/dd/yy hh:mm:ss.fff";
        public const string DateTimeFormatsForFileName = "yyMMddhhmmssfff";

        public static string GetFormattedURL(string URL, string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                culture = "en-us";
            }

            var appendChar = URL.IndexOf('?') > 0 ? "&" : "?";
            URL = string.Format("{0}{1}culture={2}", URL, appendChar, culture);

            return URL;
        }
    }
}
