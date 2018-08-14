using Microsoft.Extensions.DependencyInjection;
using System;

namespace AS.Blog.Core.Extension
{
    public static class CookieAuthExtension
    {
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection serviceCollecion)
        {
            return serviceCollecion.ConfigureApplicationCookie(opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromDays(7);
                opt.LoginPath = $"/user/login";
                opt.LogoutPath = $"/user/logoff";
                opt.AccessDeniedPath = $"/accessdenied";
            });
        }
    }
}
