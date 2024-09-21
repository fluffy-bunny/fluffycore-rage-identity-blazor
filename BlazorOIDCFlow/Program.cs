using BlazorOIDCFlow;
using BlazorOIDCFlow.Contracts;
using BlazorOIDCFlow.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var response = await httpClient.GetAsync("appsettings.json");
var json = await response.Content.ReadAsStringAsync();

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
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
