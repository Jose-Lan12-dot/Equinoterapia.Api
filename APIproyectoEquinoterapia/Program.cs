using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.Data.Services;
using APIproyectoEquinoterapia.Data.Services.Paciente_;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// SERCICIOS

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CaballoService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<InstructorService>();
builder.Services.AddScoped<FormularioService>();
builder.Services.AddScoped<ArchivoService>();
builder.Services.AddScoped<InicioSesionService>();

//AUTENTICACION JWT (TOKEN)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "FrontEquinoterapia",
        ValidAudience = "FrontEquinoterapia",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("clave-super-secreta-para-token"))
    };
});



//CONEXIONN A LA BASE DE DATOS
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://localhost:7183")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AllowAll");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseAuthentication();
app.UseAuthorization();



