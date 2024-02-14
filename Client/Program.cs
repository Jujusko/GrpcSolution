// using Client.Configurations;
// using Client.Mappers;
// using Microsoft.AspNetCore;
// using Microsoft.OpenApi.Models;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.Configure<GrpcConfiguration>(builder.Configuration.GetSection("GrpcConfiguration"));
//
// // Add services to the container.
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestTask", Version = "v1" });
// });
// builder.Services.AddAutoMapper(typeof(ClientMapper));
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();

using Client;

Startup.ConfigApp(
    Startup.ConfigureHost(
        WebApplication.CreateBuilder(new WebApplicationOptions { Args = args })
    ).Build()
).Run();