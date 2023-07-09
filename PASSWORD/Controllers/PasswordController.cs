using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class PasswordController : Controller
    {
        private static readonly TimeSpan PasswordValidityDuration = TimeSpan.FromSeconds(30);

        // Metoda pentru generarea parolei
        [HttpGet]
        public IActionResult GeneratePassword(string userId)
        {
            DateTime dateTime = DateTime.Now;
            string password = GenerateUniquePassword(userId, dateTime);

            return Ok(password);
        }

        // Metoda pentru generarea unei parole unice bazată pe User ID și Data Time
        private static string GenerateUniquePassword(string userId, DateTime dateTime)
        {
            string uniqueId = userId + dateTime.ToString("yyyyMMddHHmmss");
            string password = CalculateHash(uniqueId);
            return password;
        }

        // Metoda pentru calcularea hash-ului unui șir de caractere
        private static string CalculateHash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
