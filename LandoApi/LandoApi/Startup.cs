﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandoApi.Filters;
using LandoApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;

namespace LandoApi
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
            services.Configure<HotelInfo>(
                Configuration.GetSection("Info"));

            // Use in-memory database for quick dev and testing
            // TODO: Swap out for a real database in production
            services.AddDbContext<HotelApiDbContext>(
                options => options.UseInMemoryDatabase("landondb"));

            services
                .AddMvc(options => 
                {
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<RequireHttpsOrCloseAttribute>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning( options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                    policy => policy
                        .WithOrigins("https://example.com"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi3WithApiExplorer(options =>
                {
                    options.GeneratorSettings.DefaultPropertyNameHandling
                        = NJsonSchema.PropertyNameHandling.CamelCase;
                });
            }
            else
            {
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors("AllowMyApp");
            app.UseMvc();
        }
    }
}
