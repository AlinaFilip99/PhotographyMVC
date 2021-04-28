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

            var connection = @"Server=(localdb)\mssqllocaldb;Database=TestEntityFrameworkDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<PhotographyContext>
                (options => options.UseSqlServer(connection, b =>b.MigrationsAssembly("Photography.ApplicationLogic")));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
