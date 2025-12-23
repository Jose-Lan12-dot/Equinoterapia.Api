using Equinoterapia.Application.DTOs.Caballos;
using Equinoterapia.Domain.Entities; // Necesario para el enum
using FluentValidation;

namespace Equinoterapia.Application.Validators.Caballos
{
    public class UpdateCaballoValidator : AbstractValidator<UpdateCaballoDto>
    {
        public UpdateCaballoValidator()
        {

            RuleFor(x => x.Placa)
                .Length(10).WithMessage("La placa debe tener 10 caracteres.")
                .NotEmpty().WithMessage("La placa no puede estar vacía.")
                .When(x => x.Placa is not null);

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .When(x => x.Nombre is not null);

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado no puede estar vacío.")
                .Must(BeValidEstado).WithMessage("El estado debe ser 'Disponible', 'En Tratamiento', 'En Descanso' o 'Retirado'.")
                .When(x => x.Estado is not null);

        }

        private bool BeValidEstado(string? estado)
        {
            return Enum.TryParse<EstadoCaballo>(estado, true, out _);
        }
    }
}