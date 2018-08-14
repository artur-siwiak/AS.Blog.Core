using AS.Blog.Core.DB;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AS.Blog.Core.Security
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            // todo
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
