using SapBroker;
using SapBroker.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

if (settings.Emulator)
{
    builder.Services.AddScoped<ISapService, SapEmulator>();
}
else
{
    builder.Services.AddScoped<ISapService, SapService>();
}

var app = builder.Build();

var loggerConfig = new LoggerConfiguration();
loggerConfig = loggerConfig.WriteTo.Console();
Log.Logger = loggerConfig.CreateLogger();

app.MapGet("/api/order", (ISapService sapService, int days = 1) =>
{
    var orders = sapService.RetrieveOrders(days);
    return orders;
});

app.Run();
