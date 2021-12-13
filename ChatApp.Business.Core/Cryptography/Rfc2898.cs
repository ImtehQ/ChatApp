using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Business.Core.Cryptography
{
    public static class Rfc2898
    {
        public static string Convert(string password, string salt)
        {
            byte[] saltArray = Encoding.ASCII.GetBytes(salt);
            Rfc2898DeriveBytes rfcKey = new Rfc2898DeriveBytes(password, saltArray);
            return System.Convert.ToBase64String(rfcKey.GetBytes(32));
        }
    }
}
