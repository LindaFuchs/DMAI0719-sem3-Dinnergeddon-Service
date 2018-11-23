using System;
using System.Security.Cryptography;

namespace Controller
{
    public class PasswordHasher
    {
        /// <summary>
        /// This method hashes a password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hash of the password</returns>
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer;

            if (password == null || password == "")
                throw new ArgumentNullException("Password cannot be null or empty");

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer, 0, dst, 0x11, 0x20);

            return Convert.ToBase64String(dst);
        }

        /// <summary>
        /// This method checks if a password is correct
        /// </summary>
        /// <param name="passwordHash">The hash of a password</param>
        /// <param name="password">The password to check against the hash</param>
        /// <returns>If the password is verified against the hash</returns>
        public static bool VerifyPassword(string passwordHash, string password)
        {
            if (passwordHash == null)
                return false;
            if (password == null || password == "")
                throw new ArgumentNullException("Password cannot be null or empty");

            byte[] buffer4;
            byte[] src = Convert.FromBase64String(passwordHash);
            if ((src.Length != 0x31) || (src[0] != 0))
                return false;

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
                buffer4 = bytes.GetBytes(0x20);

            return ByteArraysEqual(buffer3, buffer4);
        }

        /// <summary>
        /// This method checks if two byte arrays are the same
        /// </summary>
        /// <param name="b0">first array</param>
        /// <param name="b1">second array</param>
        /// <returns>If the two arrays are the same</returns>
        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
                return true;

            if (b0 == null || b1 == null)
                return false;

            if (b0.Length != b1.Length)
                return false;

            for (int i = 0; i < b0.Length; i++)
                if (b0[i] != b1[i])
                    return false;

            return true;
        }
    }
}
