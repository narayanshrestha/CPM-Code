using Blazored.SessionStorage;
using BlazorWasm.Client;
using BlazorWasm.Client.Handlers;
using BlazorWasm.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthenticationHandler>();

string serverUrl = builder.Configuration["ServerUrl"] ?? "";

builder.Services.AddHttpClient("ApiEndPoint")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serverUrl))
    .AddHttpMessageHandler<AuthenticationHandler>();

//TODO: Juts for testing
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverUrl) });

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddBlazoredSessionStorageAsSingleton();

await builder.Build().RunAsync();
