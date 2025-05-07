using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using OrdenesCompra.JPazos.FrontEnd;
using OrdenesCompra.JPazos.FrontEnd.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Limpiar el token del almacenamiento local
await builder.Services.BuildServiceProvider().GetRequiredService<IJSRuntime>().InvokeVoidAsync("localStorage.removeItem", "authToken");

// Configurar la URL base correcta del servidor API
var configuration = builder.Configuration;
var apiBaseUrl = configuration["ApiSettings:BaseUrl"];

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SecurityService>();
builder.Services.AddScoped<OrdenService>();

// Configurar HttpClient con los handlers personalizados
builder.Services.AddHttpClient<SecurityService>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<OrdenService>(client => client.BaseAddress = new Uri(apiBaseUrl));



await builder.Build().RunAsync();
