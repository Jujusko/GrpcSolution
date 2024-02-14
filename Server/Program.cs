using Server;


Startup.ConfigApp(
    Startup.ConfigureHost(
        WebApplication.CreateBuilder(new WebApplicationOptions { Args = args })
    ).Build()
).Run();