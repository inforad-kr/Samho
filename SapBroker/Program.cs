using SapBroker;
using SapBroker.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddHttpClient();
if (settings.Sap.Emulator)
{
    builder.Services.AddScoped<ISapService, SapEmulator>();
}
else
{
    builder.Services.AddScoped<ISapService, SapService>();
}
builder.Services.AddScoped<IXPacsService, XPacsService>();

var app = builder.Build();

var loggerConfig = new LoggerConfiguration();
loggerConfig = loggerConfig.WriteTo.Console();
Log.Logger = loggerConfig.CreateLogger();

app.MapGet("/api/order", (ISapService sapService) =>
{
    var orders = sapService.RetrieveOrders();
    return orders;
});

app.MapPost("/api/order/transfer", async (ISapService sapService, IXPacsService xpacsService) =>
{
    var orders = sapService.RetrieveOrders();
    await xpacsService.SendOrders(orders);
});

app.Run();
