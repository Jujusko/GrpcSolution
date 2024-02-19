using System.Net;
using Client.Extensions.SerilogEnricher;
using Client.ServiceInterfaces;
using Client.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace Client;

public static class Startup
{
    internal static WebApplicationBuilder ConfigureHost(WebApplicationBuilder builder)
    {
        // Конфигурация логгера
        builder.Host.UseSerilog((context, lc) =>
        {
            lc
                .Enrich.WithCaller()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .ReadFrom.Configuration(context.Configuration);
        });

        // Конфигурация Kestrel
        builder.WebHost.ConfigureKestrel((_, opt) =>
            {
                var appHost = builder.Configuration.GetValue<string>("App:Host");
                var appPort = builder.Configuration.GetValue<int>("App:Ports:Http1");
                opt.Limits.MinRequestBodyDataRate = null;
                opt.Listen(IPAddress.Parse(appHost ?? "0.0.0.0"), appPort, listenOptions =>
                {
                    Log.Information(
                        "The application [{AppName}] is successfully started at [{StartTime}] (UTC) | protocol gRPC (http1)",
                        AppDomain.CurrentDomain.FriendlyName,
                        DateTime.UtcNow.ToString("F"));

                    listenOptions.Protocols = HttpProtocols.Http1;
                });
                opt.AllowAlternateSchemes = true;
            }
        );

        builder.Services.AddTransient<IServer, GrpcServerService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            //c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestTask", Version = "v1" });
        });
        return builder;
    }

    internal static WebApplication ConfigApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.MapGet("/", () => "http://localhost:30004/swagger");

        app.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
        app.Run();
        return app;
    }
}