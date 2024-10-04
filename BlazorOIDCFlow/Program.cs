using BlazorOIDCFlow;
using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Net;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
//var response = await httpClient.GetAsync("appsettings.json");
var response = await httpClient.GetAsync("/api/appsettings");

var json = await response.Content.ReadAsStringAsync();
AppSettings appSettings = await System.Text.Json.JsonSerializer.DeserializeAsync<AppSettings>(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
builder.Services.AddSingleton(appSettings);

var applicationEnvironment = appSettings.ApplicationEnvironment;

Console.WriteLine($"Application Environment: {applicationEnvironment}");
Console.WriteLine($"Base API URL: {appSettings.BaseApiUrl}");



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
