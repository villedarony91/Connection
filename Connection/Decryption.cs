using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace Connection
{
    class Decryption
    {
        public string decryption(String encryptedString)
        {
            string skey = "IOJDGF123FGT1548IOJDGF123FGT1548";
            string sIV = "IOJDGF123FGT1548";
            byte[] IV = System.Text.Encoding.UTF8.GetBytes(sIV);
            byte[] key = System.Text.Encoding.UTF8.GetBytes(skey);
            return decryptString(encryptedString, key, IV);
            
        }

        public string decryptString(String encryptedMessage, byte[] Key, byte[] IV)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedMessage);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                Rijndael RijndaelAlg = Rijndael.Create();
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                             RijndaelAlg.CreateDecryptor(Key, IV),
                                                             CryptoStreamMode.Read);
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error durante proceso de desencriptación " + e.ToString());
                return e.ToString();

            }

        }
    }
}
