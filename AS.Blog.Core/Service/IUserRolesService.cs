using AS.Blog.Core.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public interface IUserRolesService
    {
        Task AddRoleForUser(string email, string roleName);
        Task RemoveRoleFromUser(string email, string roleName);
        Task<bool> IsInRole(string email, string roleName);
        Task<IList<User>> GetUsersForRole(string roleName);
        Task<IList<string>> GetUserRoles(string email);
    }
}
