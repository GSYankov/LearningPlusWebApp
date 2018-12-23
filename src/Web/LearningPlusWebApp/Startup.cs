using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlus.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearningPlus.Models;
using LearningPlus.Web.Infrastructure.Extensions;
using LearningPlus.Web.Services.EmailSender;
using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Data.DbRepository;
using LerningPlus.Web.Services.NewsService.Contract;
using LerningPlus.Web.Services.NewsService;
using LerningPlus.Web.Services.UsersService;
using LerningPlus.Web.Services.UsersService.Contract;
using LerningPlus.Web.Services.ClassesService.Contract;
using LerningPlus.Web.Services.ClassesService;
using LearningPlus.Web.Hubs;
using LerningPlus.Web.Services.BlobService.Contract;
using LerningPlus.Web.Services.BlobService;
using LerningPlus.Web.Services.HomeworkService.Contract;
using LerningPlus.Web.Services.HomeworkService;

namespace LearningPlus.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LearningPlusDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<LearningPlusDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddIdentity<LearningPlusUser, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<LearningPlusDbContext>();

            services.AddAutoMapper();
            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddTransient<IEmailService, SmtpEmailService>();
            services.AddScoped<ILearningPlusNewsService, LearningPlusNewsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IClassesService, ClassesService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IHomeworkService, HomeworkService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
            app.UseSignalR(
                route =>
                {
                    route.MapHub<ChatHub>("/chat");
                });

            app.UseDatabaseSeedingAndMigration();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "area route",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
