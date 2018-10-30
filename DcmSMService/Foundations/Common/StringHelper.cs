///-----------------------------------------------------------------
///   ClassName:      StringHelper
///   Description:
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/30
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------

/*
 * CLSCompliant attribute when you want to make sure it can be used by any other .NET language. 
 *  When assembly is marked with the CLSCompliantAttribute, the compiler should check this code to see if, 
 *  when compiled, it will violate any of the CLS rules (some of which ocdecio mentioned) and report violations to you for fixing.
 *  
 *  These are the basic rules:
 *  Unsigned types should not be part of the public interface of the class. 
 *  What this means is public fields should not have unsigned types like uint or ulong, 
 *  public methods should not return unsigned types, 
 *  parameters passed to public function should not have unsigned types.
 *  However unsigned types can be part of private members.
 *  Unsafe types like pointers should not be used with public members.
 *  However they can be used with private members.
 *  Class names and member names should not differ only based on their case. 
 *  For example we cannot have two methods named MyMethod and MYMETHOD.
 *  Only properties and methods may be overloaded, Operators should not be overloaded.
 *   
 */
//[assembly: CLSCompliant(true)]

namespace Foundations.Common
{
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        /// <summary>
        /// The string cleaner
        /// </summary>
        public static readonly Regex StringCleaner = new Regex("[^0-9]", RegexOptions.Compiled);

        /// <summary>
        ///  input contains only one type of line breaks - either CR, or LF, or CR+LF
        /// </summary>
        public static string ToWinodwsLineBreaker(string input)
        {
            return input.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }
    }
}
