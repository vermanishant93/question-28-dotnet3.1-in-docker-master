using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet3._1_in_docker.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dotnet3._1_in_docker
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
            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson();
            /*
            services.AddDbContext<ApplicationContext>(options =>
            options.UseInMemoryDatabase("InMemoryDB"));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BackendTest";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10000);
                options.LoginPath = "/Home/Login";
            });
            */
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            /*
            app.UseAuthorization();
            */
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            */
            ///*
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{api}/{v1}/{controller=Home}/{action=Index}");
                //endpoints.MapControllerRoute("default", "{api/controller=Home}/{action=Index}");
                //endpoints.MapDefaultControllerRoute();

            });
            //*/

        }
    }
}
