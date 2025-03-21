using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PassengerPortal.Client;
using PassengerPortal.Client.Services;
using Microsoft.Extensions.Logging;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7075/") });

builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();

