using LearningPlusWebApp.Data;
using LearningPlusWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlusWebApp.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<LearningPlusDbContext>().Database.Migrate();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<LearningPlusUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var db = serviceScope.ServiceProvider.GetService<LearningPlusDbContext>();

                CreateUsersAndRoles(userManager, roleManager, db);
            }

            return app;
        }

        private static void CreateUsersAndRoles(UserManager<LearningPlusUser> userManager,
                                                RoleManager<IdentityRole> roleManager,
                                                LearningPlusDbContext db)
        {
            Task.Run(async () =>
            {
                string[] usersAndRoles = { "Admin", "Teacher", "Parent", "Child" };

                foreach (var role in usersAndRoles)
                {
                    var adminRoleExists = await roleManager.RoleExistsAsync(role);
                    if (!adminRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = role
                        });
                    }
                }

                foreach (var username in usersAndRoles)
                {
                    var user = await userManager.FindByNameAsync(username);
                    if (user == null)
                    {
                        user = new LearningPlusUser
                        {
                            UserName = username,
                            FirstName = $"First{user}",
                            LastName = $"{user}LastName",
                            Email = $"{username}@{username}world.som"
                        };
                        await userManager.CreateAsync(user, "123456");
                        //var userCreated = await userManager.FindByNameAsync(username);
                        await userManager.AddToRoleAsync(user, username);
                    }
                }

                var child = db.Users.FirstOrDefault(u => u.UserName == "Child");
                var parent = db.Users.FirstOrDefault(u => u.UserName == "Parent");
                parent.Children.Add(child);
            }).Wait();
        }
    }
}

