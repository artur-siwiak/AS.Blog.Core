using AS.Blog.Core.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public class UserRolesService : IUserRolesService
    {
        private readonly BlogContext _db;

        public UserRolesService(BlogContext db)
        {
            _db = db;
        }

        public async Task AddRoleForUser(string email, string roleName)
        {
            var user = _db.Users.Where(x => x.Email == email).FirstOrDefault();
            var role = _db.Roles.Where(x => x.Name == roleName).FirstOrDefault();

            if (user == null || role == null)
            {
                return;
            }

            var newRoleForUser = new UserRoles
            {
                RoleId = role.RoleId,
                UserId = user.UserId
            };

            await _db.UserRoles.AddAsync(newRoleForUser).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<IList<string>> GetUserRoles(string email)
        {
            return Task.FromResult(
                (IList<string>)_db.Users.Where(x => x.Email == email)
                    .SelectMany(x => x.Roles)
                    .Select(x => x.Role.Name)
                    .ToList()
                );
        }

        public Task<IList<User>> GetUsersForRole(string roleName)
        {
            return Task.FromResult(
                (IList<User>)_db.Roles.Where(x => x.Name == roleName)
                .SelectMany(x => x.Users)
                .ToList());
        }

        public Task<bool> IsInRole(string email, string roleName)
        {
            return Task.FromResult(
                _db.Roles.Where(x => x.Name == roleName)
                .SelectMany(x => x.Users)
                .Where(x => x.User.Email == email)
                .FirstOrDefault() != null);
        }

        public Task RemoveRoleFromUser(string email, string roleName)
        {
            return Task.FromResult(
                _db.Users.Where(x => x.Email == email)
                .SelectMany(x => x.Roles)
                .Where(x => x.Role.Name == roleName)
                .FirstOrDefault() != null);
        }
    }
}
