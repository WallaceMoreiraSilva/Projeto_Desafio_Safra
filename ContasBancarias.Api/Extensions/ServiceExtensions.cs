using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ContasBancarias.Configurations;
using ContasBancarias.Domain.Interfaces.Api;
using ContasBancarias.Domain.Services.Api;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Api.Application.Queries.BuscarPorId;
using ContasBancarias.Api.Application.Queries.BuscarTodos;
using ContasBancarias.Application.Commands.Requests;
using MediatR;

namespace ContasBancarias.Extensions
{
    public static class ServiceExtensions
    {
        private const string SwaggerPath = "{0}/swagger.json";

        public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseSwagger().UseSwaggerUI(c =>
            {
                GetApiVersions(configuration).ForEach(apiVersion =>
                    c.SwaggerEndpoint(string.Format(SwaggerPath, apiVersion), apiVersion));
            });

            return builder;
        }     

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options => { ConfigureSwaggerGeneration(options, configuration); });

            return services;
        }

        public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File($"{Directory.GetCurrentDirectory()}\\Logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog(dispose: true);
                builder.AddConsole();
            });

            return services;
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {           
            services.AddMediatR(typeof(AtivarContaBancariaRequest).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(InativarContaBancariaRequest).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(InserirContaBancariaRequest).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(AtualizarContaBancariaRequest).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreditarContaBancariaRequest).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(BuscarTodosQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(BuscarPorContaBancariaQuery).GetTypeInfo().Assembly);
            
            services.AddMediatR(typeof(AtivarContaBancariaNotification).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(InativarContaBancariaNotification).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(InserirContaBancariaNotification).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(AtualizarContaBancariaNotification).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreditarContaBancariaNotification).GetTypeInfo().Assembly);           
            services.AddMediatR(typeof(ErroNotification).GetTypeInfo().Assembly);          
        }

        private static List<string> GetApiVersions(IConfiguration configuration)
           => configuration.GetSection(SwaggerConfigurations.SwaggerSectionName).GetChildren().Select(x => x.Key).ToList();

        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.ReportApiVersions = true;
                x.AssumeDefaultVersionWhenUnspecified = true;
            }).AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'VVV";
                x.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        private static void ConfigureSwaggerGeneration(SwaggerGenOptions options, IConfiguration configuration)
        {
            IncludeSwaggerDocs(options, configuration);
            IncludeComments(options);
        }

        private static void IncludeSwaggerDocs(SwaggerGenOptions options, IConfiguration configuration)
        {
            var swaggerDocsDictionary = configuration.GetSection(SwaggerConfigurations.SwaggerSectionName).GetChildren()
               .ToDictionary(x => x.Key, x => configuration.GetSection(x.Path).Get<OpenApiInfo>());

            foreach (var doc in swaggerDocsDictionary)
                options.SwaggerDoc(doc.Key, doc.Value);
        }

        public static IServiceCollection AddDefaultServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiServiceConfiguration>(configuration.GetSection(nameof(ApiServiceConfiguration)));

            services.AddSingleton<IApiService, ApiService>(x =>
                new ApiService(x.GetRequiredService<IOptions<ApiServiceConfiguration>>().Value));

            return services;
        }

        private static void IncludeComments(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";

            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        }
    }
}
