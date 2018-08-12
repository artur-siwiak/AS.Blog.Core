using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AS.Blog.Core.DB
{
    public class BloggingContext : DbContext
    {
        private readonly ILogger<BloggingContext> _log;

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        //public DbSet<Policy> Policies { get; set; }

        public BloggingContext(DbContextOptions<BloggingContext> options, ILogger<BloggingContext> log)
            : base(options)
        {
            _log = log;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasIndex(p => p.Url)
                .IsUnique(true);

            //modelBuilder.Entity<Policy>()
            //    .HasIndex(p => p.Name)
            //    .IsUnique();

            modelBuilder.Entity<UserRole>()
                .HasKey(bc => new { bc.RoleId, bc.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Roles)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<Role>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }

        public override int SaveChanges()
        {
            return CatchException(() => base.SaveChanges());
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return CatchException(() => base.SaveChanges(acceptAllChangesOnSuccess));
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return CatchExceptionAsync(() => base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return CatchExceptionAsync(() => base.SaveChangesAsync(cancellationToken));
        }

        private int CatchException(Func<int> func)
        {
            try
            {
                return func();
            }
            catch (DbUpdateConcurrencyException duce)
            {
                _log.LogError(duce.InnerException.Message);
            }
            catch (DbUpdateException due)
            {
                _log.LogError(due.InnerException.Message);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
            }

            return int.MinValue;
        }

        private Task<int> CatchExceptionAsync(Func<Task<int>> func)
        {
            try
            {
                return func();
            }
            catch (DbUpdateConcurrencyException duce)
            {
                _log.LogError(duce.InnerException.Message);
            }
            catch (DbUpdateException due)
            {
                _log.LogError(due.InnerException.Message);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
            }

            return Task.FromResult(int.MinValue);
        }
    }
}
