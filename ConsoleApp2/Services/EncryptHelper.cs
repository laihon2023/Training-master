using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Services
{
    /// <summary>
    /// 產生私key與公key
    /// </summary>
    public class EncryptHelper2 : IEncryptHelper2
    {
        static string privateKey = string.Empty;
        static string publiceKey = string.Empty;

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="rawInput"></param>
        /// <returns></returns>
        public string Eecrypt(string rawInput)
        {
            return RsaEncrypt(rawInput, publiceKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="rawInput"></param>
        /// <returns></returns>
        public string Decrypt(string rawInput)
        {
            return RsaDecrypt(rawInput, privateKey);
        }

        /// <summary>
        /// 生成公鑰
        /// </summary>
        /// <param name="xmlkeys"></param>
        /// <param name="xmlpublickey"></param>
        public void RSAKey(out string xmlkeys, out string xmlpublickey)
        {
            var rsa = new RSACryptoServiceProvider();
            xmlkeys = rsa.ToXmlString(true);
            xmlpublickey = rsa.ToXmlString(false);
            privateKey = xmlkeys;
            publiceKey = xmlpublickey;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="rawInput"></param>
        /// <param name="publickey"></param>
        /// <returns></returns>
        public string RsaEncrypt(string rawInput, string publickey)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(publickey))
            {
                throw new ArgumentException("Invaild public key");
            }

            using(var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(rawInput);  //有含義的字符串轉為字節流
                rsaProvider.FromXmlString(publickey);   //載入公鑰
                int buffSize = (rsaProvider.KeySize / 8) - 11;  //單節最大長度
                var buffer = new byte[buffSize];
                using(MemoryStream inputstream = new MemoryStream(inputBytes), outputstream = new MemoryStream())
                {
                    while (true)
                    {
                        //分段加密
                        int readSize = inputstream.Read(buffer, 0, buffSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp,0, readSize);
                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
                        outputstream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Convert.ToBase64String(outputstream.ToArray());  //轉化為字節流轉換方便傳輸
                }
            }
        }

        public static string RsaDecrypt(string encryptedInput, string privateKey)
        {
            if (string.IsNullOrEmpty(encryptedInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentException("Invalid Private Key");
            }

            using(var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Convert.FromBase64String(encryptedInput);
                rsaProvider.FromXmlString(privateKey);   //載入私key
                int bufferSize = rsaProvider.KeySize / 8;
                var buffer = new byte[bufferSize];
                using (MemoryStream inputstream = new MemoryStream(inputBytes), outputStream = new MemoryStream())
                {
                    while(true)
                    {
                        int readSize = inputstream.Read(buffer, 0, bufferSize);
                        if(readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var rawBytes = rsaProvider.Decrypt(temp, false);
                        outputStream.Write(rawBytes, 0, rawBytes.Length);
                    }
                    return Encoding.UTF8.GetString(outputStream.ToArray());
                }
            }
        }




    }
}
