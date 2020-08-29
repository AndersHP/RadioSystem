using System;
using System.IO;
using System.Reflection;
using Core.Repositories;
using Core.Usecases;
using DAL.InMemoryRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MotorolaRadioSystem
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //IoC Injection of Repositories and usecases
            //TODO: Could setup database and add a startup to the test project to allow it to use its own faked setup
            services.AddSingleton<IRadioRepository, InMemoryRadioRepository>();
            services.AddScoped<IRadioGod, RadioGod>();


            //Add Swagger documentation
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("RadiosystemAPI", new OpenApiInfo()
                {
                    Title = "Radiosystem API",
                    Description = "API for Radio systems",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                // Adds XML comments for Presentation.API
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //Use swagger and UI to display API
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/RadiosystemAPI/swagger.json", "Radiosystem API");
                options.RoutePrefix = "api";
            });
        }
    }
}
