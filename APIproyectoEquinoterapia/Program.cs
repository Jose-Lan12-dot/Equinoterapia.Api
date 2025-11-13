using Equinoterapia.Application;
using Equinoterapia.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://localhost:7041") // Mover a appsettings
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// SERVICIOS - Logica de negocios
builder.Services.AddApplicationServices();
// SERVICIOS - Infraestructura (Externos)
builder.Services.AddInfrastructureServices(builder.Configuration);





var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
