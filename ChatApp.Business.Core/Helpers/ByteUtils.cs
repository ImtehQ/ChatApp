using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Helpers
{
    public static class ByteUtils
    {
        public static byte[] Generate(this byte[] bytes, int seed = 3123)
        {
            Random r = new Random(seed);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)r.Next(0, 255);
            }
            return bytes;
        }
    }
}
