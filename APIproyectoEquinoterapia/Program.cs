using Equinoterapia.Application;
using Equinoterapia.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var allowedOrigins = builder.Configuration
    .GetSection("CorsSettings:AllowedOrigins")
    .Get<string>();

if (string.IsNullOrEmpty(allowedOrigins))
{
    throw new InvalidOperationException("AllowedOrigins no está configurado en appsettings.json");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// SERVICIOS - Logica de negocios
builder.Services.AddApplicationServices();
// SERVICIOS - Infraestructura (Externos)
builder.Services.AddInfrastructureServices(builder.Configuration);





var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
