﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsSite.Models;

namespace NewsSite
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
            services.AddDbContext<NewsSiteContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<NewsSiteContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccessToHiddenNews", policy => 
                    policy.RequireRole("Publisher", "Subscriber", "Administrator"));

                options.AddPolicy("Adult", policy =>
                    policy.RequireClaim("news", "readerAtleast20", "admin", "publisher"));

                options.AddPolicy("PublishEconomy", policy =>
                    policy.RequireClaim("news", "publisheconomy", "admin"));

                options.AddPolicy("PublishSports", policy =>
                    policy.RequireClaim("news", "publishsports", "admin"));

                options.AddPolicy("PublishCulture", policy =>
                    policy.RequireClaim("news", "publishculture", "admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseDirectoryBrowser();
            app.UseMvc();
        }
    }
}
