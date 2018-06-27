using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DayBook.Models
{
    public class CryptoHelper
    {
        private static string RsaKey;
        public static byte[] Encode(string str)
        {
            var provider = new RSACryptoServiceProvider();
            RsaKey = provider.ToXmlString(true);
            return provider.Encrypt(Encoding.UTF8.GetBytes(str), true);
        }

        public static string Decode(byte[] bytes)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(RsaKey);
            return Encoding.UTF8.GetString(provider.Decrypt(bytes, true));
        }
    }
}