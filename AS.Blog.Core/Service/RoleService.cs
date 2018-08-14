using AS.Blog.Core.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public class RoleService : IRoleService
    {
        private readonly BlogContext _db;

        public RoleService(BlogContext db)
        {
            _db = db;
        }

        public async Task<bool> AddRole(Role role)
        {
            var tmpRole = _db.Roles.Add(role);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return tmpRole.Entity.RoleId > 0;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var tmpRole = _db.Roles.Where(x => x.RoleId == id).FirstOrDefault();

            if (tmpRole == null)
            {
                return false;
            }

            var result = _db.Roles.Remove(tmpRole);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public Task<Role> GetRole(int role)
        {
            var result = _db.Roles.Where(x => x.RoleId == role).FirstOrDefault();

            if (result == null)
            {
                throw new RoleNotFoundException();
            }

            return Task.FromResult(result);
        }

        public Task<Role> GetRole(string role)
        {
            var result = _db.Roles.Where(x => x.Name == role).FirstOrDefault();

            if (result == null)
            {
                throw new RoleNotFoundException();
            }

            return Task.FromResult(result);
        }

        public async Task<bool> UpdateRole(Role role)
        {
            var result = _db.Update(role);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
