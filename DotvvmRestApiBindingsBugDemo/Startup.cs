using DotVVM.Framework.Api.Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using DotVVM.Core.Common;
using DotvvmRestApiBindingsBugDemo.Services;
using Newtonsoft.Json;

namespace DotvvmRestApiBindingsBugDemo
{
    public class Startup
    {

        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddAuthentication();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.EnableDotvvmIntegration();
            });
            services.AddSingleton<DateService>();

            services.Configure<DotvvmApiOptions>(opt =>
            {
            });

            services.AddDotVVM<DotvvmStartup>();
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
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseRouting();

			app.UseAuthentication();
            app.UseAuthorization();

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
            dotvvmConfiguration.AssertConfigurationIsValid();
            
            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath)
            });

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapDotvvmHotReload();
                endpoints.MapSwagger();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                // register ASP.NET Core MVC and other endpoint routing middlewares
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });
        }
    }
}
