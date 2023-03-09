using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Domain.Interfaces.Repository;
using ContasBancarias.Api.Infrastructure.Repository;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Api.Infrastructure.Sqlite.Interfaces;
using ContasBancarias.Api.Infrastructure.Sqlite.Configurations;
using ContasBancarias.Api.Application.Queries.BuscarPorId;
using ContasBancarias.Api.Application.Queries.BuscarTodos;
using ContasBancarias.Application.Commands.Requests;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ContasBancarias.Domain.Interfaces.Repository;
using ContasBancarias.Infrastructure.Repository;
using ContasBancarias.Infrastructure.Sqlite;
using ContasBancarias.Extensions;
using System.Reflection;
using System.Text.Json;
using System.Net.Mime;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog
builder.Services.AddSerilogConfiguration(builder.Configuration);

//sqLite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabase, Database>();

////MediatR
builder.Services.ConfigureMediatR();

//Repositories
builder.Services.AddSingleton<IContaBancariaRepository, ContaBancariaRepository>();
builder.Services.AddSingleton<ICreditarRepository, CreditarRepository>();

//HealthChecks
builder.Services.AddHealthChecks();

//Swagger
builder.Services.ConfigureSwagger(builder.Configuration);

//Api
builder.Services.ConfigureApiVersioning();

var app = builder.Build();

// sqlite
app.Services.GetService<IDatabase>().Setup();

//Cors
app.UseCors(x => x
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetIsOriginAllowed(origin => true)
   .AllowCredentials());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseConfiguredSwagger(builder.Configuration);

app.UseHealthChecks("/health");
app.UseHealthChecks("/health-json",
    new HealthCheckOptions()
    {
        ResponseWriter = async (context, report) =>
        {
            var result = JsonSerializer.Serialize(
                new
                {
                    timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    status = report.Status.ToString(),
                });

            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }
    });

app.Run();



