using LearningPlus.Data;
using LearningPlus.Models;
using LearningPlus.Web.Models;
using LearningPlus.Web.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseDatabaseSeedingAndMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<LearningPlusDbContext>().Database.Migrate();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<LearningPlusUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var db = serviceScope.ServiceProvider.GetService<LearningPlusDbContext>();

                CreateUsersAndRoles(userManager, roleManager, db);
                CreateNews(db);

            }

            return app;
        }

        private static void CreateNews(LearningPlusDbContext db)
        {


            if (db.News.Count() == 0)
            {
                var holidays = new string[] {
                "3rd of March",
                "1st of June",
            };

                var newsRange = new List<LearningPlusNews>();

                foreach (var day in holidays)
                {
                    var news = new LearningPlusNews
                    {
                        Creator = db.Users.FirstOrDefault(u => u.UserName == "Admin"),
                        Message = $"{day} will be vacation for all in the center!",
                        TargetRoles = new List<LearningPlusNewsTargetRole>()
                                    {
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Admin},
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Teacher},
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Parent},
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Child}
                                    }
                    };
                    newsRange.Add(news);
                }

                var adminNews = new LearningPlusNews
                {
                    Creator = db.Users.FirstOrDefault(u => u.UserName == "Admin"),
                    Message = $"Admins work from home today.",
                    TargetRoles = new List<LearningPlusNewsTargetRole>()
                                    {
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Admin}
                                    }
                };

                var teacherNews = new LearningPlusNews
                {
                    Creator = db.Users.FirstOrDefault(u => u.UserName == "Teacher"),
                    Message = $"Teachers work till noon today.",
                    TargetRoles = new List<LearningPlusNewsTargetRole>()
                                    {
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Teacher}
                                    }
                };

                var childParentNews = new LearningPlusNews
                {
                    Creator = db.Users.FirstOrDefault(u => u.UserName == "Admin"),
                    Message = $"Kids will attend origami workshop today.",
                    TargetRoles = new List<LearningPlusNewsTargetRole>()
                                    {
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Parent},
                                     new LearningPlusNewsTargetRole{ TargetRole = UserRoles.Child}                                     }
                };
                newsRange.Add(adminNews);
                newsRange.Add(teacherNews);
                newsRange.Add(childParentNews);

                db.News.AddRange(newsRange);
                db.SaveChanges();
            }
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

