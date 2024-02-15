using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Viamatica.Blog.WASM;
using Viamatica.Blog.WASM.Servicios;
using Viamatica.Blog.WASM.Servicios.IServicio;
using Viamatica.Blog.WASM.Servicios.IServicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Agregar servicios
builder.Services.AddScoped<IPublicacionService, PublicacionService>();
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();

//Agregar la autenticación y autorización
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>( s => s.GetRequiredService<AuthStateProvider>());

//Para usar el localstorage del navegador
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
