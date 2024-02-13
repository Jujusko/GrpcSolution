using Microsoft.EntityFrameworkCore;
using Server.DAL;
using Server.Mapper;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(TestTaskMapper));
builder.Services.AddDbContext<TestTaskDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o =>
    {

    });

});

var app = builder.Build();

app.MapGrpcService<GrpcTestService>();
app.MapGrpcService<GrpcProductService>();

app.Run();
