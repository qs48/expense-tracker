using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using MudBlazor;
using UserUI;
using UserUI.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5062/") // your UserService URL
});

builder.Services.AddMudServices();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<MudTheme>(new CustomMudTheme());

await builder.Build().RunAsync();