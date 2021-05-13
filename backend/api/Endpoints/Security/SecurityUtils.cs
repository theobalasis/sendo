using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Sendo.Api.Endpoints.Security
{
    public static class SecurityUtils
    {
        public static string GenerateRandomString(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[4 * length];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            var result = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                var random = BitConverter.ToUInt32(data, i * 4);
                var index = random % chars.Length;

                result.Append(chars[index]);
            }

            return result.ToString();
        }

        public static string HashPassword(string password, string salt)
        {
            var initialHash = SHA512.HashData(Encoding.UTF8.GetBytes(password)).ToList<byte>();
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var initialSize = initialHash.Count;

            for (int i = 0, j = 0; i < initialSize; i++, j = j < salt.Length - 1 ? j + 1 : 0)
            {
                var index = initialHash[i] % initialSize;
                initialHash.Insert(index, saltBytes[j]);
            }

            return Encoding.UTF8.GetString(SHA512.HashData(initialHash.ToArray<byte>()));
        }
    }
}
