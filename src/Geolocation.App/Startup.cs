using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Dadata;
using Geolocation.App.Example;
using Geolocation.App.Filter;
using Geolocation.Domain.Interfaces;
using Geolocation.Infrastructure;
using Geolocation.Infrastructure.Saga;
using MassTransit;
using MassTransit.Definition;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

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

            services.AddDbContext<GeolocationContext>(c =>
                c.UseNpgsql(Configuration.GetConnectionString(nameof(GeolocationContext))));
            services.AddScoped<IGeolocationContext, GeolocationContext>();

            services.AddMassTransit(x =>
            {

                x.AddSagaStateMachine<AddressStateMachine, AddressSaga>(typeof(AddressSagaDefinition))
                    .EntityFrameworkRepository(r =>
                    {
                        r.UsePostgres();
                        r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                        r.ExistingDbContext<GeolocationContext>();
                    });

                x.UsingRabbitMq((context, cfg) =>
                {

                    cfg.Host(Configuration.GetConnectionString(nameof(RabbitMqHostSettings)));
                    cfg.ConcurrentMessageLimit = 10;
                    cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
                });
            });

            services.AddMassTransitHostedService();

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.Culture = new CultureInfo("ru-RU");
            });

            services.AddSwaggerGen(options =>
            {
                options.CustomOperationIds(e => $"{Regex.Replace(e.RelativePath, "{|}", "")}{e.HttpMethod}");
                options.SwaggerDoc("v1", new OpenApiInfo { Title = GetType().Namespace, Version = "v1" });
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
                options.SchemaFilter<ExampleSchemaFilter>();

            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(s => s.SerializeAsV2 = true);
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geolocation.App v1"));
            }

            using var service = app.ApplicationServices.CreateScope();
            service.ServiceProvider.GetService<GeolocationContext>()!.Database.Migrate();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
