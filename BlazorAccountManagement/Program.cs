using BlazorAccountManagement;
using BlazorAccountManagement.Contracts;
using BlazorAccountManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

var environment = builder.HostEnvironment.Environment;
Console.WriteLine($"Current environment: {environment}");

builder.Services.AddScoped<ModalService>();

var configService = new ConfigService(httpClient);


// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var json = await configService.GetAppSettingsAsync();

var configuration = new ConfigurationBuilder()
    .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
    .Build();

var applicationEnvironment = configuration.GetValue<string>("ApplicationEnvironment");
 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
if (applicationEnvironment == "Production")
{
    builder.Services.AddScoped<IRageApiService, ProdRageApiService>();
}
else
{
    builder.Services.AddScoped<IRageApiService, LocalRageApiService>();
}
builder.Services.AddScoped<RedirectService>();
builder.Services.AddSingleton<AppData>();

var host = builder.Build();
// Set the culture
var supportedCultures = new[] { new CultureInfo("en") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");
await host.RunAsync();
