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
		public static string Encrypt(string input, byte[] key, byte[] vector, int keySize)
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
				aesAlg.KeySize = keySize;
				aesAlg.Padding = PaddingMode.PKCS7;

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

		public static void Decrypt(Patient patient, byte[] key, byte[] vector, int keySize)
		{
			// Decrypt each encrypted data item.
			patient.FirstName = Decrypt(Convert.FromBase64String(patient.FirstName), key, vector);
		}

		private static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
		{
			string plaintext;

			// Create AesManaged    
			using (AesManaged aesAlg = new AesManaged())
			{
				aesAlg.Key = key;
				aesAlg.IV = iv;
				//aesAlg.KeySize = keySize;
				aesAlg.Padding = PaddingMode.PKCS7;


				// Create a decryptor    
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv);
				// Create the streams used for decryption.    
				using (var ms = new MemoryStream(cipherText))
				{
					// Create crypto stream    
					using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					{
						// Read crypto stream    
						using (var reader = new StreamReader(cs))
							plaintext = reader.ReadToEnd();
					}
				}
			}
			return plaintext;
		}
	}
}