using api.Services;
using api.Soap;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// REST services
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IGreetingService, GreetingService>();

// SOAP / CoreWCF
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, ServiceMetadataBehavior>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7020")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("PermitirFrontend");

app.UseAuthorization();

app.MapControllers();

// SOAP endpoint
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<GreetingSoapService>();
    serviceBuilder.AddServiceEndpoint<GreetingSoapService, IGreetingSoapService>(
        new BasicHttpBinding(),
        "/Soap/GreetingService.svc");
});

var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

app.Run();