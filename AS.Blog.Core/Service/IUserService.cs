using AS.Blog.Core.DB;
using AS.Blog.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public interface IUserService
    {
        Task<bool> CreateUser(User model);
        Task<bool> DeleteUser(string email);
        Task<bool> EditUser(UserModel user);
        Task<User> FindUserById(string userId);
        Task<User> FindUserByName(string email);
        Task<string> GetUserId(object name);
        Task AddRoleForUser(string email, string roleName);
        Task RemoveRoleForUser(string email, string roleName);
        Task<List<string>> GetRolesForUser(string email);
        Task<bool> GetRoleForUser(string email, string roleName);
        Task<IList<User>> GetUsersInRole(string roleName);
    }
}
