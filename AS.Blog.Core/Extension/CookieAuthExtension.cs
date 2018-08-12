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
                //opt.LoginPath = $"/{Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName}/user/login";
                //opt.LogoutPath = $"/{Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName}/user/logoff";
                //opt.AccessDeniedPath = $"/{Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName}/accessdenied";
            });
        }
    }
}
