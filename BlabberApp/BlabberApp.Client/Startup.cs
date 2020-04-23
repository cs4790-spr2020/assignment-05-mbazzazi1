using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlabberApp.DataStore.Adapters;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Services;

namespace BlabberApp.Client
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
            UserServiceFactory userServiceFactory = new UserServiceFactory();
            IUserPlugin userPlugin = userServiceFactory.CreateUserPlugin("mysql");
            UserAdapter userAdapter = userServiceFactory.CreateUserAdapter(userPlugin);
            UserService userService = userServiceFactory.CreateUserService(userAdapter);

            BlabServiceFactory blabServiceFactory = new BlabServiceFactory();
            IBlabPlugin blabPlugin = blabServiceFactory.CreateBlabPlugin("mysql");
            BlabAdapter blabAdapter = blabServiceFactory.CreateBlabAdapter(blabPlugin);
            BlabService blabService = blabServiceFactory.CreateBlabService(blabAdapter);

            services.AddSingleton<IUserService>(s => userService);
            services.AddSingleton<IBlabService>(s => blabService);
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
