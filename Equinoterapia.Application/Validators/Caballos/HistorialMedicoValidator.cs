using FluentValidation;
using Equinoterapia.Application.DTOs.Caballos;

namespace Equinoterapia.Application.Validators.Caballos
{
    public class HistorialMedicoValidator : AbstractValidator<HistorialMedicoDto>
    {
        public HistorialMedicoValidator()
        {
            RuleFor(x => x.PadecimientoId)
                .GreaterThan(0).WithMessage("El ID del padecimiento debe ser válido.");

            RuleFor(x => x.Observaciones)
                .MaximumLength(500).WithMessage("Las observaciones no pueden exceder los 500 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Observaciones));

            RuleFor(x => x.HistorialId)
                .GreaterThanOrEqualTo(0).WithMessage("El ID del historial no puede ser negativo.");
        }
    }
}