using AS.Blog.Core.DB;
using AS.Blog.Core.Service;
using AS.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AS.Blog.Core.Store
{
    public class CustomRoleStore : IRoleStore<Role>, IRoleValidator<Role>
    {
        private readonly IRoleService _db;

        public CustomRoleStore(IRoleService db)
        {
            _db = db;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CustomRoleStore() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            var added = await _db.AddRole(role).ConfigureAwait(false);

            return added
                ? IdentityResult.Success
                : IdentityResultError.CannotInsertRole(role.Name);
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var deleted = await _db.DeleteRole(role.Id).ConfigureAwait(false);

            return deleted
                ? IdentityResult.Success
                : IdentityResultError.CannotDeleteRole(role.Name);
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var role = int.Parse(roleId);

            return _db.GetRole(role);
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var role = normalizedRoleName.ToUppercaseFirst();

            return _db.GetRole(role);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name.ToUppercaseFirst());
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName.ToUppercaseFirst();

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName.ToUppercaseFirst();

            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            bool updated = await _db.UpdateRole(role).ConfigureAwait(false);

            return updated
                ? IdentityResult.Success
                : IdentityResultError.CannotUpdateRole(role.Name);
        }

        public Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
