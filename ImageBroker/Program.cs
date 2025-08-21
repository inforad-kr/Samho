using ImageBroker;
using ImageBroker.Models;
using ImageBroker.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

if (settings.SapEmulator)
{
    builder.Services.AddScoped<ISapService, SapEmulator>();
}
else
{
    builder.Services.AddScoped<ISapService, SapService>();
}
builder.Services.AddScoped<UploadService>();
builder.Services.AddHttpClient("Pacs", httpClient =>
{
    httpClient.BaseAddress = new(settings.PacsUrl);
    httpClient.BaseAddress = new(httpClient.BaseAddress, "api/v1/");
    httpClient.DefaultRequestHeaders.Authorization = new("Bearer", settings.PacsToken);
});

var app = builder.Build();

var loggerConfig = new LoggerConfiguration();
loggerConfig = loggerConfig.WriteTo.Console();
Log.Logger = loggerConfig.CreateLogger();

app.MapGet("/", () => "Image Broker is running...");

app.MapPost("/api/study", async (UploadService uploadService, StudyRef studyRef) =>
{
    return await uploadService.UploadImages(studyRef);
});

app.Run();
