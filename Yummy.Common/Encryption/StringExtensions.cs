using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.Web.Security;

namespace Yummy.Common.Encryption
{
    public static class StringExtensions
    {
        // <summary>
        /// Processes an MD5 hash string
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <returns>MD5 Hash string</returns>
        public static string Encrypt(this string password)
        {
            //we use codepage 1252 because that is what sql server uses
            byte[] pwdBytes = Encoding.GetEncoding(1252).GetBytes(password);
            byte[] hashBytes = MD5.Create().ComputeHash(pwdBytes);
            return Encoding.GetEncoding(1252).GetString(hashBytes);
        }

        /// <summary>
        /// Encrypts the passwords using SHA1 encryption
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Encrypt(this string password, string salt)
        {
            HashAlgorithm hash = new SHA1Managed();

            System.Text.Encoding Encoding = System.Text.Encoding.GetEncoding("Windows-1252");
            byte[] b = Encoding.GetBytes(String.Concat(password, salt));
            byte[] b2 = hash.ComputeHash(b);

            return Convert.ToBase64String(b2);
        }
    }
}
