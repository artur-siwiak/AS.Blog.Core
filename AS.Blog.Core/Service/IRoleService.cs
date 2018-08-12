using AS.Blog.Core.DB;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public interface IRoleService
    {
        Task<bool> AddRole(Role role);
        Task<bool> DeleteRole(int id);
        Task<Role> GetRole(int role);
        Task<Role> GetRole(string role);
        Task<bool> UpdateRole(Role role);
    }
}
