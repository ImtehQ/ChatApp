using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Cryptography
{
    public static class Rfc2898
    {
        public static string Convert(string password, string salt)
        {
            byte[] saltArray = Encoding.ASCII.GetBytes(salt);
            Rfc2898DeriveBytes rfcKey = new Rfc2898DeriveBytes(password, saltArray);
            return System.Text.Encoding.Default.GetString(rfcKey.GetBytes(32));
        }
    }
}
