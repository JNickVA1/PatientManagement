using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PatientManagement
{
	public static class Crypto
	{
		public static string Encrypt(string input, byte[] key, byte[] vector)
		{
			//
			byte[] encrypted;

			//
			// Create an AesManaged object
			// with the specified key and IV.
			using (AesManaged aesAlg = new AesManaged())
			{
				aesAlg.Key = key;
				aesAlg.IV = vector;

				// Create the Encryptor to perform the stream transform.
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{

							//Write all data to the stream.
							swEncrypt.Write(input);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}
			//
			string cipherText = Convert.ToBase64String(encrypted);
			return cipherText;
		}

		public static string Decrypt(string input, byte[] key, byte[] vector)
		{
			return "";
		}
	}
}