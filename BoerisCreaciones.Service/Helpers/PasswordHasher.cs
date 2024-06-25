﻿using System.Text;
using System.Security.Cryptography;
using BoerisCreaciones.Service.Excepciones;

namespace BoerisCreaciones.Service.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static void VerifyPassword(string hashedPasswordFromDatabase, string providedPassword)
        {
            string hashedProvidedPassword = HashPassword(providedPassword);
            if (hashedPasswordFromDatabase != hashedProvidedPassword)
                throw new InvalidPasswordException("Contraseña incorrecta");
        }
    }
}
