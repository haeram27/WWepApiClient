///-----------------------------------------------------------------
///   ClassName:      CSVHelper
///   Description:    helps to using application App.config files
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/30
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------

using Foundations.Common;
using Foundations.Logger;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Foundations.File
{
    internal static class CSVHelper
    {
        private const string TAG = "Extensions";
        private static SHA256Managed _sha256;

        private static SHA256Managed SHA256
        {
            get
            {
                if (_sha256 == null)
                {
                    _sha256 = new SHA256Managed();
                }
                return _sha256;
            }
        }

        /// <summary>
        /// EXAMPLE: to make CSV format using DTO(Data Transer Object) Model
        /// Provider CSV representation of ResponseModel object.
        /// The format uses ";" character in place of newline
        /// Format:
        /// Timestamp,0101201400000;Reputation;Safe,1;Attention,2;Data;11111,1,I;22222,2,U
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
/*
        internal static string ToCSV(ResponseModel response)
        {
            var builder = new StringBuilder();

            // Add timestamp
            builder.Append("Timestamp");
            builder.Append(",");
            builder.Append(response.Timestamp.ToString(Constants.DateTimeFormats));
            builder.Append(";");

            // Add reputation mapping
            builder.Append("Reputation");
            builder.Append(";");
            foreach (ResponseModel model in response.Reputation)
            {
                builder.Append(model.Reputation);
                builder.Append(",");
                builder.Append(model.ID);
                builder.Append(";");
            }

            // Add Data component
            builder.Append("Data");
            builder.Append(";");
            foreach (ResponseModel model in response.Data)
            {
                builder.Append(model.PhoneNumber);
                builder.Append(",");
                builder.Append(model.Reputation);
                builder.Append(";");
            }
            return builder.ToString();
        }
*/

        /// <summary>
        /// To the CSV.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        internal static string ToCSV(this IList<string> response)
        {
            try
            {
                var result = new StringBuilder();
                foreach (string item in response)
                {
                    result.Append(item);
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(TAG, "Error preparing Export CSV\n" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Extension method to clean and hash a phone number value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string Transform(this string value, string saltValue)
        {
            /*
               Input string is split into 2 parts - first part contains the first 7 characters of the string and second part contains the remaining characters
               If input length is less than 7 characters, then all characters except the last one are hashed 
               (this case should only occur if the number sent by third party would be without country and area code. This should be avoided if possible)
               First part is hashed using SHA256 and converted to base64 string
               Hashed first part and plain text second part are combined to produce the final value
            */
            // Compute the hash
            var inputLength = value.Length;
            var hashLength = (inputLength) < 7 ? (inputLength - 1) : 7; // if input is less than 7 characters, get all the characters except the last, otherwise get starting 7 characters

            var valueToHash = value.Substring(0, hashLength);
            var hashBytes = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(valueToHash + saltValue));

            // Prepare the final value
            value = Convert.ToBase64String(hashBytes) + value.Substring(hashLength, inputLength - hashLength);

            return value;
        }

        /// <summary>
        /// Extension method to clean the specified value.
        /// Removes all non-numeric characters and leading zeroes
        /// E.g. +01333444 will be give an output of 1333444
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>clean string</returns>
        internal static string Clean(this string value)
        {
            // Clean the string
            return StringHelper.StringCleaner.Replace(value, string.Empty).TrimStart('0');
        }

        /// <summary>
        /// Reverses the string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string ToReverse(this string value)
        {
            var reverseArray = value.ToCharArray();
            Array.Reverse(reverseArray);
            return new string(reverseArray);
        }

        /// <summary>
        /// Masks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string Mask(this string value)
        {
            return ("#" + value.Substring(value.Length / 2, (value.Length - value.Length / 2)));
        }
    }
}