using Blazored.LocalStorage;
using Client;
using ClientLibrary.Authentication.CustomAuthenticationStateProvider;
using ClientLibrary.Authentication.CustomHttpHandler;
using ClientLibrary.Constant.ClientConstant;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implimentations;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implimentations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//
// Root Componets
//
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//
// Helper Services(LocalStorage/ Serializa/ GetHttpClients)
//
builder.Services.AddScoped<ILocalStorage, LocalStorage>();
builder.Services.AddTransient(typeof(ISerialization<>), typeof(Serialization<>));
builder.Services.AddScoped<IGetHttpClients,GetHttpClients>();

//
// Services(UserAccout)
//
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

//
// Custom Http Handel
//
builder.Services.AddTransient<CustomHttpHandler>();

//
// Http handler
//
builder.Services.AddHttpClient(ClientConstant.SystemApiClientName, _ => 
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    _.BaseAddress = new Uri(baseUrl ?? throw new InvalidOperationException("Base URL not configured")); 

}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//
// Blazor Services
//
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
