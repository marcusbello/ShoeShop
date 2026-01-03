using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShoeShop.UI.Blazor;
using ShoeShop.UI.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Resolve API base address from configuration (wwwroot/appsettings*.json) using key "ApiBaseAddress".
// Fallback to the host environment base address when not specified.
var apiBaseAddress = builder.Configuration["ApiBaseAddress"];
if (string.IsNullOrWhiteSpace(apiBaseAddress))
{
    apiBaseAddress = builder.HostEnvironment.BaseAddress;
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
