using AS.Blog.Core.DB;
using Microsoft.AspNetCore.Identity;

namespace AS.Blog.Core.Security
{
    public sealed class CustomPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            user.Salt = Salting.GenerateSalt();
            return Hasher.HmacHash(user.Salt, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            var password = Hasher.HmacHash(user.Salt, providedPassword) == hashedPassword;

            return password ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
