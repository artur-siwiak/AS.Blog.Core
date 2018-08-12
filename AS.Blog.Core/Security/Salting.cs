using System.Security.Cryptography;
using System.Text;

namespace AS.Blog.Core.Security
{
    public static class Salting
    {
        private const int SALT_LENGTH = 20;
        private const string SALT_RANGE = "AzByCxDyEwFGvHuItJsKrLqMoNnOmPlQkRjSiThUgVfWeYdXcYbZa0918274655AzByCxDyEwFGvHuItJsKrLqMoNnOmPlQkRjSiThUgVfWeYdXcYbZa0918274655AzByCxDyEwFGvHuItJsKrLqMoNnOmPlQkRjSiThUgVfWeYdXcYbZa0918274655AzByCxDyEwFGvHuItJsKrLqMoNnOmPlQkRjSiThUgVfWeYdXcYbZa0918274655DyEwFGvH55Az";

        public static string GenerateSalt(int saltLength = SALT_LENGTH)
        {
            var sb = new StringBuilder(saltLength);
            using (var random = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[saltLength];
                random.GetBytes(bytes);

                for (int i = 0; i < saltLength; ++i)
                {
                    sb.Append(SALT_RANGE[bytes[i]]);
                }
            }

            return sb.ToString();
        }
    }
}
