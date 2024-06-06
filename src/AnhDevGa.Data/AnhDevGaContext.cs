using AnhDevGa.Core.Domain.Content;
using AnhDevGa.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhDevGa.Data
{
    public class AnhDevGaContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AnhDevGaContext(DbContextOptions options) : base(options)
        {
        }

        // Cấu hình DbSet
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostActivityLog> PostActivityLogs { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<PostInSeries> PostInSeries { get; set; }

        // Cấu hình override model of Identity
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                var dateCreatedProperty = entityEntry.Entity.GetType().GetProperty("DateCreated");

                if (dateCreatedProperty != null)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        dateCreatedProperty.SetValue(entityEntry.Entity, DateTime.Now);
                    }
                    else if (entityEntry.State == EntityState.Modified)
                    {
                        dateCreatedProperty.SetValue(entityEntry.Entity, DateTime.Now);
                    }
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
