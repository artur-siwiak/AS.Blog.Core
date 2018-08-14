using AS.Blog.Core.DB;
using AS.Blog.Core.Extension;
using AS.Blog.Core.Security;
using AS.Blog.Core.Service;
using AS.Blog.Core.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AS.Blog.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<BlogContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                })
                .AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN")
                .AddTransient<IPasswordHasher<User>, CustomPasswordHasher>()
                .AddTransient<ILookupNormalizer, CustomLookupNormalizer>()
                .AddTransient<IPasswordValidator<User>, CustomPasswordValidator>()
                ;

            // Add identity types
            services
                .AddIdentity<User, Role>()
                .AddRoleStore<CustomRoleStore>()
                .AddUserStore<CustomUserStore>()
                .AddRoleValidator<CustomRoleStore>()
                .AddUserStore<CustomUserStore>()
                .AddDefaultTokenProviders();

            services
                .AddCookieAuthentication()
                .AddRouting(opt => opt.LowercaseUrls = true)
                .AddScoped<IUserStore<User>, CustomUserStore>()
                .AddScoped<IRoleStore<Role>, CustomRoleStore>()
                .AddScoped<IUserRoleStore<User>, CustomUserStore>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRolesService, UserRolesService>()

                ;

            services
                .AddRouting()
                .LoadPolicies();

            services
                .AddTransient<IBlogService, BlogService>()
                .AddTransient<IUserService, UserService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "post_route",
                    template: "Post/{*postUrl}",
                    defaults: new { controller = "Post", Action = "Index" });

                routes.MapRoute(
                    name: "error_route",
                    template: "Error/{*errorCode}",
                    defaults: new { controller = "Error", Action = "Index" });
            });
        }
    }
}
