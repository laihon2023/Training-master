using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ConsoleApp2.Services;

namespace ConsoleApp2
{
    public class App
    {
        //注入
        private readonly ISerices _service;
        private readonly IEncryptHelper2 _IEncryptHelper2;

        public App(ISerices service, IEncryptHelper2 IEncryptHelper2)
        {
            _service = service;
            _IEncryptHelper2 = IEncryptHelper2;
        }

        /// <summary>
        /// 將類別以加密方式寫入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="t"></param>
        public static void WriteEncorypt<T>(string path, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            //var jsonEncry = EncryptHelper
        }

        public void RunEncrypt()
        {
            string cypher = string.Empty;   //密文
            string plainText = string.Empty;   //解密文
            string privatekey = string.Empty;
            string publickey = string.Empty;
            _IEncryptHelper2.RSAKey(out privatekey, out publickey);
            //Console.WriteLine($"public key:{ _IEncryptHelper2.RSAKey(out privatekey, out publickey) } \n"); //取得public key

            Console.WriteLine("請輸入文字來加密");

            var text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text))    //密文
            {
                cypher = _IEncryptHelper2.Eecrypt(text);
                Console.WriteLine($"密文:{cypher} \n");  //密文內容
            }

            Console.Write("請按任意鍵，取得解密文\n");
            Console.ReadLine();
            if (!string.IsNullOrEmpty(cypher))
            {
                plainText = _IEncryptHelper2.Decrypt(cypher);
                Console.WriteLine($"解密文:{plainText} \n");  //解密文內容
            }
            Console.ReadKey();  //關閉視窗
        }

       

        public void Run2()
        {
            string cypher = string.Empty;   //密文
            string plainText = string.Empty;   //解密文

            Console.WriteLine($"public key:{_service.GetPublicKey()} \n"); //取得public key

            Console.WriteLine("請輸入文字來加密");

            var text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text))    //密文
            {
                cypher = _service.Encrypt(text);
                Console.WriteLine($"密文:{cypher} \n");  //密文內容
            }

            Console.Write("請按任意鍵，取得解密文\n");
            Console.ReadLine();
            if (!string.IsNullOrEmpty(cypher))
            {
                plainText = _service.Decrypt(cypher);
                Console.WriteLine($"解密文:{plainText} \n");  //解密文內容
            }
            Console.ReadKey();  //關閉視窗
        }
    }
}
