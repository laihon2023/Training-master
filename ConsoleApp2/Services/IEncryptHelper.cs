using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Services
{
    public interface IEncryptHelper2
    {
        string Eecrypt(string rawInput);
        string Decrypt(string rawInput);
        void RSAKey(out string xmlkeys,out string xmlpublickey);
        string RsaEncrypt(string rawInput, string publickey);
    }
}
