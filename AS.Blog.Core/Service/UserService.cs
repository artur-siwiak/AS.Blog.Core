using AS.Blog.Core.DB;
using AS.Blog.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public async Task AddRoleForUser(string email, string roleName)
        {
            var role = _context.Roles.Where(x => x.Name == roleName).FirstOrDefault();
            var user = _context.Users.Where(x => x.Email == email).First();

            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            _context.UserRoles.Add(
                new UserRoles { User = user, Role = role });

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> CreateUser(User model)
        {
            try
            {
                _context.Users.Add(model);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUser(string email)
        {
            var user = _context.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> EditUser(UserModel user)
        {
            var u = _context.Users.Where(x => x.Email == user.Login).FirstOrDefault();

            if (u == null)
            {
                return false;
            }

            _context.Update(u);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public Task<User> FindUserById(string userId)
        {
            return Task.FromResult(
                _context.Users.
                Where(x => x.UserId.ToString() == userId)
                .First());
        }

        public Task<User> FindUserByName(string email)
        {
            return Task.FromResult(_context.Users.Where(x => x.Email == email).FirstOrDefault());
        }

        public Task<bool> GetRoleForUser(string email, string roleName)
        {
            return Task.FromResult(_context.Users.Where(x => x.Email == email)
                .SelectMany(x => x.Roles)
                .Where(x => x.Role.Name == roleName)
                .Select(x => x.Role)
                .FirstOrDefault() != null);
        }

        public Task<List<string>> GetRolesForUser(string email)
        {
            var a = _context.Users.Where(x => x.Email == email)
                .SelectMany(x => x.Roles)
                .Select(x => x.Role.Name)
                .ToList();

            return Task.FromResult(a);
        }

        public Task<string> GetUserId(object name)
        {
            return Task.FromResult((string)name);
        }

        public Task<IList<User>> GetUsersInRole(string roleName)
        {
            var users = _context.UserRoles.Where(x => x.Role.Name == roleName).Select(x => x.User).ToList() as IList<User>;

            return Task.FromResult(users);
        }

        public async Task RemoveRoleForUser(string email, string roleName)
        {
            var role = _context.Users.Where(x => x.Email == email)
                .SelectMany(x => x.Roles)
                .Where(x => x.Role.Name == roleName);
            _context.Remove(role);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
