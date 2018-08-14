using Microsoft.AspNetCore.Identity;
using System;

namespace AS.Blog.Core.Errors
{
    public static class IdentityResultError
    {
        public static IdentityResult CannotInsertUser(string user)
        {
            return IdentityResult.Failed(
                new IdentityError() { Code = "INSERT_USER_ERROR", Description = $"Could not insert user {user}." });
        }

        public static IdentityResult CannotDeleteUser(string user)
        {
            return IdentityResult.Failed(
                new IdentityError() { Code = "DELETE_USER_ERROR", Description = $"Could not delete user {user}." });
        }

        public static IdentityResult CannotUpdateUser(string user)
        {
            return IdentityResult.Failed(
                new IdentityError() { Code = "UPDATE_USER_ERROR", Description = $"Could not update user {user}." });
        }

        public static IdentityResult CannotInsertRole(string role)
        {
            return IdentityResult.Failed(
                new IdentityError() { Code = "INSERT_ROLE_ERROR", Description = $"Could not insert role {role}." });
        }

        internal static IdentityResult CannotDeleteRole(string name)
        {
            throw new NotImplementedException();
        }

        public static IdentityResult CannotUpdateRole(string name)
        {
            return IdentityResult.Failed(
                new IdentityError() { Code = "UPDATE_ROLE_ERROR", Description = $"Could not edit role {name}." });
        }
    }
}
