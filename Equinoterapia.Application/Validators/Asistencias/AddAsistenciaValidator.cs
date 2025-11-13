using Equinoterapia.Application.DTOs.Asistencias;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Asistencias
{
    public class AddAsistenciaValidator : AbstractValidator<AddAsistenciaDto>
    {
        public AddAsistenciaValidator()
        {

            RuleFor(x => x.SesionId)
                .GreaterThan(0).WithMessage("El ID de la sesión es requerido.");


            RuleFor(x => x.PacientesId)
                .NotEmpty().WithMessage("La lista de pacientes no puede estar vacía.");


            RuleForEach(x => x.PacientesId)
                .GreaterThan(0).WithMessage("Un ID de paciente en la lista no es válido.");
        }
    }
}