using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace App.Global.Encreption
{
    public static class Hashing
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string ComputeHMACSHA512Hash(string slat, string password)
        {
            var hash = new HMACSHA512(Encoding.UTF8.GetBytes(slat));
            var result = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(result);
        } 
    }
}
