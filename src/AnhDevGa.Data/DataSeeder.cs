using AnhDevGa.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhDevGa.Data
{
    public class DataSeeder
    {
        public async Task SeedAsync(AnhDevGaContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var rootAdminRoleId = Guid.NewGuid();

            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new AppRole() { Id = rootAdminRoleId, Name = "Admin", NormalizedName = "ADMIN", DisplayName = "Quản trị viên" });
            }

            if (!context.Users.Any())
            {
                var userId = Guid.NewGuid();
                var user = new AppUser()
                {
                    Id = userId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "myloc442@gmail.com",
                    NormalizedEmail = "MYLOC442@GMAIL.COM",
                    FirstName = "Loc",
                    LastName = "Duong",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    DateCreated = DateTime.Now,
                };
                
                // Hash password
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
                await context.Users.AddAsync(user);

                // Add user to role
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    UserId = userId,
                    RoleId = rootAdminRoleId
                });
                
                await context.SaveChangesAsync();
            }
        }
    }
}
