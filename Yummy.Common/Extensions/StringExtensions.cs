using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yummy.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// used to check whether the first letter of the string is a vowel.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool FirstLetterIsVowel(this String context)
        {
            if (!string.IsNullOrEmpty(context))
            {
                string vowels = "aeoiu";
                if (vowels.Contains(context.Substring(0, 1).ToLower())) return true;
            }
            return false;
        }

        /// <summary>
        /// Use to replace a list of items in a string.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Replace(this String context, Dictionary<string, string> list)
        {
            foreach (var item in list)
            {
                context = context.Replace(item.Key, item.Value);
            }

            return context;
        }

        /// <summary>
        /// method for determining is the user provided a valid email address
        /// We use regular expressions in this check, as it is a more thorough
        /// way of checking the address provided
        /// </summary>
        /// <param name="email">email address to validate</param>
        /// <returns>true is valid, false if not valid</returns>
        public static bool IsEmail(this String email)
        {
            string pattern = @"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$";

            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            if (!string.IsNullOrEmpty(email))
            {
                return check.IsMatch(email);
            }
            return false;
        }
    }
}
