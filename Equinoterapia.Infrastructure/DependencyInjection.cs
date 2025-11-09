using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Interfaces;
using Equinoterapia.Infrastructure.Persistence;
using Equinoterapia.Infrastructure.Repositories;
using Equinoterapia.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;





namespace Equinoterapia.Infrastructure
    {
        public static class DependencyInjection
        {
            public static IServiceCollection AddInfrastructureServices(
                this IServiceCollection services,
                IConfiguration configuration)
            {

////////////SERVICIO DBCONTEXT Y MIGRACIONES
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            ));

////////////SERVICIO JWT
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtKey = configuration["Jwt:Key"];
                    var jwtIssuer = configuration["Jwt:Issuer"]; 
                    var jwtAudience = configuration["Jwt:Audience"]; 

                    if (string.IsNullOrEmpty(jwtKey) ||
                        string.IsNullOrEmpty(jwtIssuer) ||
                        string.IsNullOrEmpty(jwtAudience))
                    {
                        throw new InvalidOperationException("Configuración de JWT (Key, Issuer o Audience) no encontrada o vacía.");
                    }

                    options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,         
                    ValidAudience = jwtAudience,    
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });


            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPacienteQueryRepository, PacienteRepository>();
            services.AddScoped<IArchivoRepository, ArchivoRepository>();

            services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
            services.AddScoped<IEvaluacionQueryRepository, EvaluacionRepository>();
            services.AddScoped<IRespuestaRepository, RespuestaRepository>();

            services.AddScoped<ICaballoRepository, CaballoRepository>();
            services.AddScoped<ICaballoQueryRepository, CaballoRepository>();

            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IInstructorQueryRepository, InstructorRepository>();


            services.AddScoped<ISesionRepository, SesionRepository>();
            services.AddScoped<ISesionQueryRepository, SesionRepository>();
            services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
            services.AddScoped<IAsistenciaQueryRepository, AsistenciaRepository>();


            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>(); 
            services.AddSingleton<IEncryptionService, EncryptionService>(); 


            return services;
        }
    }
}