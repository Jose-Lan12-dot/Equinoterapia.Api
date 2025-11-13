using Equinoterapia.Application.Interfaces;
using Equinoterapia.Application.Services;
using Equinoterapia.Application.Validators.Instructores;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



namespace Equinoterapia.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();


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






            return services;
        }
    }
}