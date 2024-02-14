using Client.Configurations;
using Microsoft.OpenApi.Models;

namespace Client;

public static class Startup
{

    internal static WebApplicationBuilder ConfigureHost(WebApplicationBuilder builder)
    {

        builder.Services.Configure<GrpcConfiguration>(builder.Configuration.GetSection("GrpcConfiguration"));

// Add services to the container.
        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestTask", Version = "v1" });
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

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        return app;
    }
}