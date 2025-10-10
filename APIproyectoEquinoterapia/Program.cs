using APIproyectoEquinoterapia.Models;
using APIproyectoEquinoterapia.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CaballoService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<InstructorService>();
builder.Services.AddScoped<FormularioService>();
builder.Services.AddScoped<ArchivoService>();
builder.Services.AddScoped<InicioSesionService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<GastoConfDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("CompanyConnection")));
//builder.Services.AddDbContext<APIproyectoEquinoterapia.Models.AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoEquinoterapiaContext") ?? throw new InvalidOperationException("Connection string 'ProyectoEquinoterapiaContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
