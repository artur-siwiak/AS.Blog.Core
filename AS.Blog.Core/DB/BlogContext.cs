using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AS.Blog.Core.DB
{
    public class BlogContext : DbContext
    {
        private readonly ILogger<BlogContext> _log;

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTags> PostTags { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options, ILogger<BlogContext> log)
            : base(options)
        {
            _log = log;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            void Seed()
            {
                var id = 1;

                foreach (var role in Security.Policies.Roles)
                {
                    modelBuilder.Entity<Role>().HasData(new Role { RoleId = id, Name = role });

                    id++;
                }
            }

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.Url)
                .IsUnique(true);

            modelBuilder.Entity<UserRoles>()
                .HasKey(bc => new { bc.RoleId, bc.UserId });

            modelBuilder.Entity<UserRoles>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Roles)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<PostTags>()
                .HasKey(bc => new { bc.PostId, bc.TagId });

            modelBuilder.Entity<UserRoles>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Roles)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<PostTags>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.Tags)
                .HasForeignKey(bc => bc.TagId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<PostTags>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.Posts)
                .HasForeignKey(bc => bc.PostId);

            modelBuilder.Entity<Role>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(p => p.Name)
                .IsUnique();

            Seed();
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
