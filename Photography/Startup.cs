using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Photography.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Photography.ApplicationLogic.Services;
using Photography.ApplicationLogic.Abstractions;
using Microsoft.AspNetCore.Identity;
using Photography.ApplicationLogic.Models;

namespace Photography
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
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<PhotographyContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 10;

                options.User.RequireUniqueEmail = true;
            });

            var connection = @"Server=(localdb)\mssqllocaldb;Database=PhotographyDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<PhotographyContext>
                (options => options.UseSqlServer(connection, b =>b.MigrationsAssembly("Photography.DataAccess")));

            services.AddScoped<IAccountRepository, EFAccountRepository>();
            services.AddScoped<AccountService>();

            services.AddScoped<ICommentRepository, EFCommentRepository>();
            services.AddScoped<CommentService>();

            services.AddScoped<IContactFormRepository, EFContactFormRepository>();
            services.AddScoped<ContactFormService>();

            services.AddScoped<IPhotoRepository, EFPhotoRepository>();
            services.AddScoped<PhotoService>();

            services.AddScoped<IPostRepository, EFPostRepository>();
            services.AddScoped<PostService>();

            services.AddScoped<IRoleRepository, EFRoleRepository>();
            services.AddScoped<RoleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
