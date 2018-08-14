using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AS.Blog.Core.Security
{
    public static class Policies
    {
        public static List<string> Roles => new List<string>()
        {
            "Administrator",
            "User"
        };

        public static IServiceCollection LoadPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                Roles.ForEach(x =>
                    options.AddPolicy("Policies",
                    policy => policy.RequireRole(x)
                    )
                );
            });

            return services;
        }
    }
}
