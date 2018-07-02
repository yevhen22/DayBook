using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook.Content
{
    public class Transposition
    {
        private static int[] key = {1,2,3,4,5,7,8, };

        public static void SetKey(int[] _key)
        {
            key = new int[_key.Length];

            for (int i = 0; i < _key.Length; i++)
                key[i] = _key[i];
        }

        public static void SetKey(string[] _key)
        {
            key = new int[_key.Length];

            for (int i = 0; i < _key.Length; i++)
                key[i] = Convert.ToInt32(_key[i]);
        }

        public void SetKey(string _key)
        {
            SetKey(_key.Split(' '));
        }

        public static string Encrypt(string input)
        {
            for (int i = 0; i < input.Length % key.Length; i++)
                input += input[i];

            string result = "";

            for (int i = 0; i < input.Length; i += key.Length)
            {
                char[] transposition = new char[key.Length];

                for (int j = 0; j < key.Length; j++)
                    transposition[key[j] - 1] = input[i + j];

                for (int j = 0; j < key.Length; j++)
                    result += transposition[j];
            }

            return result;
        }

        public static string Decrypt(string input)
        {
            string result = "";

            for (int i = 0; i < input.Length; i += key.Length)
            {
                char[] transposition = new char[key.Length];

                for (int j = 0; j < key.Length; j++)
                    transposition[j] = input[i + key[j] - 1];

                for (int j = 0; j < key.Length; j++)
                    result += transposition[j];
            }

            return result;
        }


        public static string GetEncryptedString(string input)
        {
            string newPass = string.Empty;
            int num = new Random().Next(1, 10);
            foreach (char ch in input)
            {
                newPass += (char)(ch + num);
            }
            newPass += (char)num;
            return newPass;
        }

        public static string GetDecryptedString(string input)
        {
            string decryptedPassword = string.Empty;
            int num = (int)input[input.Length - 1];
            input = input.Remove(input.Length - 1, 1);
            foreach (char ch in input)
            {
                decryptedPassword += (char)(ch - num);
            }
            return decryptedPassword;
        }
    }
}