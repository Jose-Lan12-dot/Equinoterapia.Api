using Equinoterapia.Application.DTOs.Sesiones;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Sesiones
{
    public class AddSesionValidator : AbstractValidator<AddSesionDto>
    {
        public AddSesionValidator()
        {
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es requerida.");

            RuleFor(x => x.Hora)
                .NotEmpty().WithMessage("La hora es requerida.");

            RuleFor(x => x.InstructorId)
                .GreaterThan(0).WithMessage("El ID del instructor es requerido.");

            RuleFor(x => x.CaballoId)
                .GreaterThan(0).WithMessage("El ID del caballo es requerido.");


            RuleForEach(x => x.PacienteIds)
                .GreaterThan(0).WithMessage("Un ID de paciente en la lista no es válido.");
        }
    }
}