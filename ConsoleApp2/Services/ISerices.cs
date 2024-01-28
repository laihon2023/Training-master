using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Services
{
    public interface ISerices
    {
        /// <summary>
        /// 站台匯到總站
        /// </summary>
        void Summary();

        string GetPublicKey();
        string Encrypt(string plainText);
        string Decrypt(string cypherText);
    }
}
