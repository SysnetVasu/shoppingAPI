using API.Data;
using API.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using API.Abstractions;
using DinkToPdf.Contracts;
using DinkToPdf;
using API.PrintTemplates;

namespace API
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

         
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<ITemplateProvider, TemplateProvider>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {

                var configuration = ConfigurationOptions.Parse(Configuration
                   .GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigin",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .SetIsOriginAllowed(origin => true)
            //            .AllowAnyMethod()//.SetIsOriginAllowed((data) => true)
            //            .AllowCredentials()
            //            .WithOrigins("http://localhost", "http://localhost:8100", "http://ammanhq.dyndns.biz:5352", "http://ammanhq.dyndns.biz", "https://localhost:4200", "http://localhost:8100");
            //        });
            //});

            services.AddCors(options => options.AddPolicy("CorsPolicy",
           builder =>
           {
               builder.AllowAnyHeader()
                      .SetIsOriginAllowed(origin => true)
                      .AllowAnyMethod()//.SetIsOriginAllowed((data) => true)
                      .AllowCredentials()
                      .WithOrigins("http://localhost", "http://ammanhq.dyndns.biz:5352", "http://ammanhq.dyndns.biz", "http://localhost:8100", "https://localhost:4200");
           }));

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
           

            app.UseHttpsRedirection();

            app.UseRouting();
          
            app.UseStaticFiles();
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")
                ),
                RequestPath = new PathString("/content")
            });            

           // app.UseCors("AllowOrigin");
            
           // app.UseCors("corsAllowAllPolicy");
           // //app.UseCors(x => x.AllowAnyHeader()
           // //  .AllowAnyMethod()
           // //  .AllowCredentials()
           // //  .WithOrigins("http://localhost", "http://localhost:8100", "http://ammanhq.dyndns.biz:5352"));

           // //  app.UseAuthorization();

           // app.UseCors(
           //options => options.WithOrigins("http://localhost:8100", "http://ammanhq.dyndns.biz:5352").AllowAnyMethod()

            //);
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
