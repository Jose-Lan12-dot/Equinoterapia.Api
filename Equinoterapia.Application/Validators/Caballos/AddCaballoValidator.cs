using Equinoterapia.Application.DTOs.Caballos;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Caballos
{
    public class AddCaballoValidator : AbstractValidator<AddCaballoDto>
    {
        public AddCaballoValidator()
        {
            RuleFor(x => x.Placa)
                .NotEmpty().WithMessage("La placa es requerido.")
                .Length(10).WithMessage("La placa debe tener 10 caracteres.")
                .NotEmpty().WithMessage("La placa es requerida.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.");

            RuleFor(x => x.FechaHerraje)
                .NotEmpty().WithMessage("La fecha de herraje es requerida.");

            RuleForEach(x => x.Padecimientos)
            .SetValidator(new HistorialMedicoValidator());
        }
    }
}