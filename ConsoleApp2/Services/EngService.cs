using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography; //RSA
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassLibrary1.Models;

namespace ConsoleApp2.Services
{
    public class EngService : ISerices
    {
        private readonly BTContext _BTContext;

        //https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.rsacryptoserviceprovider?view=net-6.0
        //https://www.youtube.com/watch?v=ywxQLRVqIYU&list=PLvhBoOxe9GwPHLflLHmywDJKMEbDKFO8M&index=4&t=928s
        //https://blog.csdn.net/weixin_38211198/article/details/107404282
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public EngService(BTContext bTContext) {
            _BTContext = bTContext;
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }

        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

        public string Encrypt(string plainText)
        {
            try
            {
                csp = new RSACryptoServiceProvider();
                csp.ImportParameters(_publicKey);
                var data = Encoding.UTF8.GetBytes(plainText);
                var cypher = csp.Encrypt(data, false);
                return Convert.ToBase64String(cypher);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            csp.ImportParameters(_privateKey);
            var plainText = csp.Decrypt(dataBytes, false);
            return Encoding.UTF8.GetString(plainText);
        }

        public void Summary()
        {
            //https://docs.microsoft.com/zh-tw/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0

            /*
            var user = (from a in _BTContext.Division2s
                        where a.DivisionId == 2
                        select a).FirstOrDefault();
            Console.WriteLine(user.Name);
            Console.WriteLine("Hello Worldxxx!");
            */
            Console.ReadLine();
        }
    }
}
