using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Yank.WeiXin.Robot.Utility
{
    class SecurityUtility
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="intput">输入字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA1Encrypt(string intput)
        {
            byte[] StrRes = Encoding.Default.GetBytes(intput);
            HashAlgorithm mySHA = new SHA1CryptoServiceProvider();
            StrRes = mySHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }
            return EnText.ToString();
        }

        //对密码进行MD5加密
        static string Md5EncryptStr32(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            // Convert the input string to a byte array and compute the hash.  
            char[] temp = str.ToCharArray();
            byte[] buf = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                buf[i] = (byte)temp[i];
            }
            byte[] data = md5Hasher.ComputeHash(buf);
            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data   
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.  
            return sBuilder.ToString();
        }
    }
}
