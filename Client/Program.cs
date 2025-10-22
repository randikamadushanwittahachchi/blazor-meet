using Client;
using ClientLibrary.Authentication.CustomHttpHandler;
using ClientLibrary.Constant.HttpClientConstant;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implimentations;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implimentations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();

builder.Services.AddHttpClient(HttpClientConstant.SystemApiClientName, _ => 
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    _.BaseAddress = new Uri(baseUrl ?? throw new InvalidOperationException("Base URL not configured")); 

}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Helper
    // Add LocalStorage/ Serializa/ GetHttpClients
builder.Services.AddScoped<IGetHttpClients,IGetHttpClients>();
builder.Services.AddScoped<ILocalStorage, LocalStorage>();
builder.Services.AddTransient(typeof(ISerialization<>), typeof(Serialization<>));

//Service
// UserAccount
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
