using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayBook.Models
{
    public static class TokenHelper
    {
        /// <summary>
        /// Generate token with expire time equal 24 hr
        /// </summary >
        /// <returns></returns>
        public static string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        /// <summary>
        /// Check token validation
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true if token valid, otherwise return false</returns>
        public static bool IsTokenValid(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddHours(-24))
            {
                return false;
            }
            else return true;
        }
    }
}