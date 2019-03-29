using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Helpers
{
    public class RijndaelAESEncryptionHelper
    {
        private string _keyString;
        private string _ivString;
        public RijndaelAESEncryptionHelper(string keyString, string ivString)
        {
            _keyString = keyString;
            _ivString = ivString;
        }

        public string EncryptText(string text)
        {
            byte[] key = Encoding.UTF8.GetBytes(_keyString);
            byte[] iv = Encoding.UTF8.GetBytes(_ivString);

            using (RijndaelManaged rijndaelManaged = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC })
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }
                    }

                    byte[] encoded = memoryStream.ToArray();
                    return Convert.ToBase64String(encoded);
                }
            }
        }

        public string DecryptText(string encryptedText)
        {
            byte[] key = Encoding.UTF8.GetBytes(_keyString);
            byte[] iv = Encoding.UTF8.GetBytes(_ivString);

            using (RijndaelManaged rijndaelManaged = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC })
            {
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    {
                        return new StreamReader(cryptoStream).ReadToEnd();
                    }
                }
            }
        }
    }
}
