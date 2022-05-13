using KCMS.AppConfigs;
using KCMS.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace KCMS
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
            services.AddCors();

            services.AddControllers()
                .AddJsonOptions(options =>
                             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddNewtonsoftJson(options =>
                             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services
                .AddDatabase(Configuration)
                .AddRepositories()
                .AddServices();

            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var adminUrl = Configuration.GetValue<string>("AppSettings:WebAdminUrl");
            var customerUrl = Configuration.GetValue<string>("AppSettings:WebCustomerUrl");
            string[] origins = new string[2] { adminUrl, customerUrl };

            app.UseCors(builder => builder
                 .WithOrigins(origins)
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            // app.UseHttpsRedirection();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseRouting();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "uploads")),
                RequestPath = "/uploads"
            });

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
