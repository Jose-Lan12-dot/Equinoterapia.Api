using Equinoterapia.Application.DTOs.Sesiones;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Sesiones
{
    public class UpdateSesionValidator : AbstractValidator<UpdateSesionDto>
    {
        public UpdateSesionValidator()
        {

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es requerida.")
                .When(x => x.Fecha.HasValue);

            RuleFor(x => x.Hora)
                .NotEmpty().WithMessage("La hora es requerida.")
                .When(x => x.Hora.HasValue);

            RuleFor(x => x.InstructorId)
                .GreaterThan(0).WithMessage("El ID del instructor debe ser válido.")
                .When(x => x.InstructorId.HasValue);

            RuleFor(x => x.CaballoId)
                .GreaterThan(0).WithMessage("El ID del caballo debe ser válido.")
                .When(x => x.CaballoId.HasValue);
        }
    }
}