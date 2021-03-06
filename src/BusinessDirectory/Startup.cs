﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using BusinessDirectory.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessDirectory.ViewModels;
using BusinessDirectory.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BusinessDirectory
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddIdentity<BusinessUser, IdentityRole>(config => 
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength= 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Admin/Login";
                config.Cookies.ApplicationCookie.LogoutPath = "/Admin/Logout";
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = false;
                config.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(1);
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                        ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };

            })
            .AddEntityFrameworkStores<BusinessDbContext>();
            services.AddDbContext<BusinessDbContext>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddTransient<GeoCoordService>();
            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
             .AddJsonOptions(config =>
                {

                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            
            services.AddTransient<BusinessSeedData>();
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            BusinessSeedData seeder)
        {
            
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            Mapper.Initialize(config => {
                config.CreateMap<BusinessViewModel, Business>().ReverseMap();
                config.CreateMap<CategoryViewModel, Category>().ReverseMap();
                config.CreateMap<RegisterViewModel, BusinessUser>().ReverseMap();
            });
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });
            seeder.EnsureData().Wait();
        }
    }
}
