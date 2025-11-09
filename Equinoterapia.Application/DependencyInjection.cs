using Microsoft.Extensions.DependencyInjection;
using Equinoterapia.Application.Services;
using Equinoterapia.Application.Interfaces;



namespace Equinoterapia.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // 3. REGISTROS DE SERVICIOS DE APLICACIÓN
            services.AddScoped<ICaballoService, CaballoService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IArchivoService, ArchivoService>();
            services.AddScoped<IInicioSesionService, InicioSesionService>();
            services.AddScoped<ISesionService, SesionService>();
            services.AddScoped<IAsistenciaService, AsistenciaService>();
            services.AddScoped<IEvaluacionService, EvaluacionService>();

            services.AddScoped<IRespuestasService, RespuestasService>();

            // Aquí registraremos AutoMapper, MediatR, etc. en el futuro

            return services;
        }
    }
}