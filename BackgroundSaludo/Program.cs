using BackgroundSaludo;

var builder = Host.CreateApplicationBuilder(args);

if (OperatingSystem.IsWindows())
{
    builder.Services.AddWindowsService(options =>
    {
        options.ServiceName = "BackgroundSaludoService";
    });
}

if (OperatingSystem.IsLinux())
{
    builder.Services.AddSystemd();
}

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddHttpClient<Worker>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();