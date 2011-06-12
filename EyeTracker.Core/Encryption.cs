using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using EyeTracker.Common.Logger;

namespace EyeTracker.Core
{
    public class Encryption
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public static string ENCRYPTION_KEY = "uDZBUjP6hLxjcXR8wi";

        private static string initVector = "qZt82RNqqZt82RNq";
        private static string saltVaue = "j7QOAsuS3CeSsA";

        /// <summary>
		/// This function Encrypts specified plaintext using symmetric key algorithm
		/// and returns a base64-encoded result.
		/// </summary>
		/// <param name="plainText">
		/// Plaintext value to be encrypted.
		/// </param>
		/// <param name="passPhrase">
		/// This password will be used to generate the encryption key.
		/// </param>
		public static string EncryptLow(string plainText, string passPhrase)
		{
			return encrypt(plainText, passPhrase, saltVaue, "MD5", 1, initVector, 64);
		}

		public static string EncryptMedium(string plainText, string passPhrase)
		{
			return encrypt(plainText, passPhrase, saltVaue, "MD5", 1, initVector, 128);
		}

		public static string EncryptHigh(string plainText, string passPhrase)
		{
			return encrypt(plainText, passPhrase, saltVaue, "SHA1", 2, initVector, 256);
		}


		/// <summary>
		/// Decrypts specified ciphertext using symmetric key algorithm.
		/// </summary>
		/// <param name="plainText">
		/// Base64-formatted ciphertext value.
		/// </param>
		/// <param name="passPhrase">
		/// This password will be used to generate the encryption key.
		/// </param>
		public static string DecryptLow(string securedText, string passPhrase)
		{
            return decrypt(securedText, passPhrase, saltVaue, "MD5", 1, initVector, 64);
		}

        public static string DecryptMedium(string securedText, string passPhrase)
		{
            return decrypt(securedText, passPhrase, saltVaue, "MD5", 1, initVector, 128);
		}

        public static string DecryptHigh(string securedText, string passPhrase)
		{
            return decrypt(securedText, passPhrase, saltVaue, "SHA1", 2, initVector, 256);
		}

		#region Encrypt-Decrypt

		/// <param name="saltValue">
		/// Salt value used along with passphrase to generate password. Salt can
		/// be any string.
		/// </param>
		/// <param name="hashAlgorithm">
		/// Hash algorithm used to generate password. Allowed values are: "MD5" and
		/// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
		/// </param>
		/// <param name="passwordIterations">
		/// Number of iterations used to generate password. One or two iterations
		/// should be enough.
		/// </param>
		/// <param name="initVector">
		/// Initialization vector (or IV). This value is required to encrypt the
		/// first block of plaintext data. For RijndaelManaged class IV must be 
		/// exactly 16 ASCII characters long.
		/// </param>
		/// <param name="keySize">
		/// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
		/// Longer keys are more secure than shorter keys.
		/// </param>
		/// <returns>
		/// Encrypted value formatted as a base64-encoded string.
		/// </returns>
		private static string encrypt(string plainText,
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

			byte[] keyBytes = password.GetBytes(keySize/8);

			RijndaelManaged symmetricKey = new RijndaelManaged();

			symmetricKey.Mode = CipherMode.CBC;

			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
				keyBytes,
				initVectorBytes);

			MemoryStream memoryStream = new MemoryStream();

			CryptoStream cryptoStream = new CryptoStream(memoryStream,
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

		private static string decrypt(string cipherText,
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

			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm,
			                                                       passwordIterations);

			byte[] keyBytes = password.GetBytes(keySize/8);

			RijndaelManaged symmetricKey = new RijndaelManaged();

			symmetricKey.Mode = CipherMode.CBC;

			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			// Start decrypting.
			int decryptedByteCount = cryptoStream.Read(plainTextBytes,
			                                           0,
			                                           plainTextBytes.Length);

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
