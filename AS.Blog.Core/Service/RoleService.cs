using AS.Blog.Core.DB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public class RoleService : IRoleService
    {
        private readonly BloggingContext _context;

        public RoleService(BloggingContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRole(Role role)
        {
            var tmpRole = _context.Roles.Add(role);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return tmpRole.Entity.Id > 0;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var tmpRole = _context.Roles.Where(x => x.Id == id).FirstOrDefault();

            if (tmpRole == null)
            {
                return false;
            }

            var result = _context.Roles.Remove(tmpRole);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public Task<Role> GetRole(int role)
        {
            var result = _context.Roles.Where(x => x.Id == role).FirstOrDefault();

            if (result == null)
            {
                throw new RoleNotFoundException();
            }

            return Task.FromResult(result);
        }

        public Task<Role> GetRole(string role)
        {
            var result = _context.Roles.Where(x => x.Name == role).FirstOrDefault();

            if (result == null)
            {
                throw new RoleNotFoundException();
            }

            return Task.FromResult(result);
        }

        public async Task<bool> UpdateRole(Role role)
        {
            var result = _context.Update(role);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
