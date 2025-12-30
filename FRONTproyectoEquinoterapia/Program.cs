using BlazorDownloadFile;
using Blazored.LocalStorage;
using FRONTproyectoEquinoterapia;
using FRONTproyectoEquinoterapia.Models;

using FRONTproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Radzen;
using System.Globalization;




var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");






// 🌍 Configurar cultura en español (España o México)
var cultura = new CultureInfo("es-MX"); // o "es-ES"
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;
CultureInfo.CurrentCulture = cultura;
CultureInfo.CurrentUICulture = cultura;



//Radzen services registrados

builder.Services.AddScoped<UsuarioSesionService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

//registro nuevo de servicio AuthenticationStateProvider, CustomAuthStateProvider>
builder.Services.AddScoped<InicioSesionService>();
builder.Services.AddScoped<InformeService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<EvaluacionService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<PacienteService>();//registro de paciente
builder.Services.AddScoped<InstructorService>();//regisro de instructor
builder.Services.AddScoped<ArchivoService>(); 
builder.Services.AddScoped<CaballoService>();
builder.Services.AddScoped<SesionesService>();
builder.Services.AddScoped<AsistenciasService>();
builder.Services.AddBlazorDownloadFile();
builder.Services.AddScoped<IExportService, ExportService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<CustomAuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<CustomAuthStateProvider>());


builder.Services.AddScoped<AuthMessageHandler>();


builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7280/");
    //client.BaseAddress = new Uri("https://aplicaciones.utsistemas.gob.mx/apiequinoterapia/api/");
    //client.BaseAddress = new Uri("https://aplicaciones.utsistemas.gob.mx/apiequinoterapia/");
})
.AddHttpMessageHandler<AuthMessageHandler>();



await builder.Build().RunAsync();
