using System.IO.Compression;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.OpenTelemetry;
using Server.DAL;
using Server.Extensions.SerilogEnricher;
using Server.Interceptor;
using Server.Services;

namespace Server;

public static class Startup
{
    internal static WebApplicationBuilder ConfigureHost(WebApplicationBuilder builder)
    {
        // Конфигурация логгера
        builder.Host.UseSerilog((context, lc) => lc
            .Enrich.WithCaller()
            .Enrich.WithResource(
                ("server", Environment.MachineName),
                ("app", AppDomain.CurrentDomain.FriendlyName)
            )
            .WriteTo.Console()
            .ReadFrom.Configuration(context.Configuration)
        );

        // Конфигурация Kestrel
        builder.WebHost.ConfigureKestrel((_, opt) =>
            {
                var appHost = builder.Configuration.GetValue<string>("App:Host");
                var appPort = builder.Configuration.GetValue<int>("App:Ports:Http2");


                opt.Limits.MinRequestBodyDataRate = null;

                opt.Listen(IPAddress.Parse(appHost ?? "0.0.0.0"), appPort, listenOptions =>
                {
                    Log.Information(
                        "The application [{AppName}] is successfully started at [{StartTime}] (UTC) | protocol gRPC (http2)",
                        AppDomain.CurrentDomain.FriendlyName,
                        DateTime.UtcNow.ToString("F"));

                    listenOptions.Protocols = HttpProtocols.Http2;
                });

                opt.AllowAlternateSchemes = true;
            }
        );


        // https://learn.microsoft.com/ru-ru/aspnet/core/grpc/configuration?view=aspnetcore-8.0
        builder.Services.AddGrpc(options =>
        {
            // Промежуточное ПО для работы с Azure (аутентификация)
            options.Interceptors.Add<ClientRequestInterceptor>();

            options.IgnoreUnknownServices = false;
            options.MaxReceiveMessageSize = null;
            options.MaxSendMessageSize = null;
            options.ResponseCompressionLevel = CompressionLevel.Optimal;
            options.ResponseCompressionAlgorithm = "gzip";
            options.EnableDetailedErrors = false;
        });

        // Получение данных для подключения к БД из конфига appsettings.json
        var pgsqlHost = builder.Configuration.GetValue<string>("Postgres:Host");
        var pgsqlPort = builder.Configuration.GetValue<string>("Postgres:Port");
        var pgsqlUser = builder.Configuration.GetValue<string>("Postgres:User");
        var pgsqlPassword = builder.Configuration.GetValue<string>("Postgres:Password");
        var pgsqlDb = builder.Configuration.GetValue<string>("Postgres:Database");

        // Формирование строки с данными для подключения к БД
        var connectionString =
            $"Host={pgsqlHost};Port={pgsqlPort};Database={pgsqlDb};Username={pgsqlUser};Password={pgsqlPassword};";


        builder.Services.AddDbContext<TestTaskDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString,
                options => { options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); });

            if (builder.Environment.IsDevelopment())
                opt.EnableSensitiveDataLogging();
        });

        builder.Services.AddTransient<DbTest>();

        return builder;
    }

    internal static WebApplication ConfigApp(WebApplication app)
    {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        if (serviceScope != null)
        {
            var db = serviceScope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
            db.Database.Migrate();

            // Parallel.ForEach(new List<int>{1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9}, new ParallelOptions
            // {
            //     MaxDegreeOfParallelism = 3
            // },  (_, ct) =>
            // {
            //
            //     var product = db.GetProduct()?.Result;
            //     if (product is not null)
            //     {
            //         Log.Information("Product [{Product}]",product.Name);
            //     }
            //     
            // });
        }

        app.MapGrpcService<GrpcProductService>();

        return app;
    }
}