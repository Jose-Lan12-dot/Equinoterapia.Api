
using FRONTproyectoEquinoterapia;
using FRONTproyectoEquinoterapia.Models;
using FRONTproyectoEquinoterapia.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Radzen;
using System.Globalization;
using Blazored.LocalStorage;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<LoginService>();

// 🌍 Configurar cultura en español (España o México)
var cultura = new CultureInfo("es-MX"); // o "es-ES"
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;
CultureInfo.CurrentCulture = cultura;
CultureInfo.CurrentUICulture = cultura;



//Radzen services registrados
builder.Services.AddScoped<InicioSesionService>();
builder.Services.AddScoped<InformeService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<EvaluacionService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7280/") });
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<PacienteService>();//registro de paciente
builder.Services.AddScoped<InstructorService>();//regisro de instructor
builder.Services.AddScoped<ArchivoService>(); // <--- obligatorio para poder inyectarlo
builder.Services.AddScoped<CaballoService>();
builder.Services.AddScoped<SesionesService>();
builder.Services.AddScoped<AsistenciasService>();



await builder.Build().RunAsync();
