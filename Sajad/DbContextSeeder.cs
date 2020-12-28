using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sajad
{
    public class DbContextSeeder
    {
        private readonly SajadDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbContextSeeder(SajadDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public void Seed()
        {
            dbContext.Database.Migrate();

            if (userManager.FindByNameAsync("Admin").Result is null)
            {
                var result = userManager.CreateAsync(new IdentityUser() { UserName = "Admin" }, "admin").Result;
            }

            if(roleManager.FindByNameAsync("Admin").Result is null)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                var user = userManager.FindByNameAsync("Admin").Result;
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
