using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Task.Client;
using MudBlazor.Services;
using Task.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<HttpClient>(s =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5027")
    };

    // Ajouter les en-têtes CORS nécessaires
    client.DefaultRequestHeaders.Add("Origin", builder.HostEnvironment.BaseAddress);
    
    return client;
});


await builder.Build().RunAsync();