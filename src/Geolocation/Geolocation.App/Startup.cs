using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Dadata;
using Geolocation.App.Example;

namespace Geolocation.App
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
            services.AddOptions();
            services.Configure<AppSetting>(Configuration);
            var configuration = Configuration.Get<AppSetting>();

            _ = Configuration["Environment"] == Environments.Development
                ? services.AddTransient<ISuggestClientAsync, SuggestClientTest>()
                : services.AddTransient<ISuggestClientAsync>(_ => new SuggestClientAsync(configuration.Token));

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.CustomOperationIds(e => $"{e.RelativePath.Replace("{", "").Replace("}", "")}{e.HttpMethod}");
                options.SwaggerDoc("v1", new OpenApiInfo { Title = GetType().Namespace, Version = "v1" });
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(s => s.SerializeAsV2 = true);
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geolocation.App v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
