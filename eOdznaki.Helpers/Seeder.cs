using System.Collections.Generic;
using System.Linq;
using eOdznaki.Models;
using Microsoft.AspNetCore.Identity;

namespace eOdznaki.Helpers
{
    public class Seeder
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public Seeder(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void SeedRoles()
        {
            if (roleManager.Roles.Any()) return;

            var roles = new List<Role>
            {
                new Role {Name = "Admin"},
                new Role {Name = "Moderator"},
                new Role {Name = "Member"}
            };

            foreach (var role in roles) roleManager.CreateAsync(role).Wait();
        }

        public void SeedAdmin()
        {
            if (userManager.Users.Any(u => u.NormalizedUserName == "ADMIN")) return;

            var user = new User {UserName = "admin"};
            userManager.CreateAsync(user, "Admin123!").Wait();
            userManager.AddToRolesAsync(user, new[] {"Admin"}).Wait();
        }
    }
}