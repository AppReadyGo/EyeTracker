using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EyeTracker.Common
{
    public static class Encryption
    {
        public static string EncryptLow(this string plainText, string salt = null)
        {
            if (string.IsNullOrEmpty(salt))
            {
                salt = EncryptionSettings.Settings.SaltVaue;
            }

            return Encrypt(plainText, EncryptionSettings.Settings.PassPhrase, salt, "MD5", 1, EncryptionSettings.Settings.InitVector, 64).Replace("+", "-");
        }

        public static string EncryptHigh(this string plainText, string salt = null)
        {
            if (string.IsNullOrEmpty(salt))
            {
                salt = EncryptionSettings.Settings.SaltVaue;
            }

            return Encrypt(plainText, EncryptionSettings.Settings.PassPhrase, salt, "SHA1", 2, EncryptionSettings.Settings.InitVector, 256);
        }

        public static string DecryptHigh(this string securedText, string salt = null)
        {
            if (string.IsNullOrEmpty(salt))
            {
                salt = EncryptionSettings.Settings.SaltVaue;
            }

            return Decrypt(securedText, EncryptionSettings.Settings.PassPhrase, salt, "SHA1", 2, EncryptionSettings.Settings.InitVector, 256);
        }

        public static string DecryptLow(this string securedText, string salt = null)
        {
            if (string.IsNullOrEmpty(salt))
            {
                salt = EncryptionSettings.Settings.SaltVaue;
            }

            return Decrypt(securedText.Replace("-", "+"), EncryptionSettings.Settings.PassPhrase, salt, "MD5", 1, EncryptionSettings.Settings.InitVector, 64);
        }

        public static string GenerateSalt(int saltLength = 4)
        {
            HashAlgorithm hashProvider = new SHA256Managed();
            byte[] saltData = new byte[saltLength];

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            // Create a random salt
            random.GetNonZeroBytes(saltData);

            // Transform the byte[] to Base-64 encoded strings
            return Convert.ToBase64String(saltData);
        }

        public static string SaltedHash(string plainText, string salt)
        {
            return GetHash(Convert.FromBase64String(salt), plainText);
        }

        private static string GetHash(byte[] saltData, string plainText)
        {
            HashAlgorithm hashProvider = new SHA256Managed();

            byte[] plainData = Encoding.UTF8.GetBytes(plainText);

            // Allocate memory to store both the Data and Salt together
            byte[] dataAndSalt = new byte[plainData.Length + saltData.Length];

            // Copy both the data and salt into the new array
            Array.Copy(plainData, dataAndSalt, plainData.Length);
            Array.Copy(saltData, 0, dataAndSalt, plainData.Length, saltData.Length);

            // Calculate the hash
            // Compute hash value of our plain text with appended salt.
            byte[] hashData = hashProvider.ComputeHash(dataAndSalt);

            return Convert.ToBase64String(hashData);
        }

        #region Encrypt-Decrypt

        private static string Encrypt(
                                    string plainText,
                                    string passPhrase,
                                    string saltValue,
                                    string hashAlgorithm,
                                    int passwordIterations,
                                    string initVector,
                                    int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                    passPhrase,
                    saltValueBytes,
                    hashAlgorithm,
                    passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                    keyBytes,
                    initVectorBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(
                                            memoryStream,
                                            encryptor,
                                            CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        private static string Decrypt(
            string cipherText,
            string passPhrase,
            string saltValue,
            string hashAlgorithm,
            int passwordIterations,
            string initVector,
            int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            // Return decrypted string.   
            return plainText;
        }

        #endregion Encrypt-Decrypt
    }
}
