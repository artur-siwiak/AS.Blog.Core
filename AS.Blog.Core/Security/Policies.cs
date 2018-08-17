using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AS.Blog.Core.Security
{
    public static class Policies
    {
        public static List<string> Roles { get; }

        static Policies()
        {
            Roles = Enum.GetNames(typeof(PoliciesEnum)).ToList();
        }

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
