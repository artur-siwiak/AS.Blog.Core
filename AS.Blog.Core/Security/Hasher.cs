using AS.Extensions;
using System.Security.Cryptography;

namespace AS.Blog.Core.Security
{
    public static class Hasher
    {
        public static string HmacHash(byte[] key, byte[] dataToHash)
        {
            using (var hmac = new HMACSHA512(Hash(key)))
            {
                return hmac.ComputeHash(dataToHash).BytesToString();
            }
        }

        public static string HmacHash(string key, string stringToHash)
        {
            return HmacHash(key.ToBytes(), stringToHash.ToBytes());
        }

        public static byte[] Hash(byte[] dataToHash)
        {
            using (var sha = new SHA512CryptoServiceProvider())
            {
                return sha.ComputeHash(dataToHash);
            }
        }
    }
}
