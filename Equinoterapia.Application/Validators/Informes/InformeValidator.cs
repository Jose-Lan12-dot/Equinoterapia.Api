using Equinoterapia.Application.DTOs.Informes;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Informes
{
    public class InformeValidator : AbstractValidator<InformeDto>
    {
        public InformeValidator()
        {
            RuleFor(x => x.Mes)
                .InclusiveBetween(1, 12)
                .WithMessage("El mes debe estar entre 1 y 12.");
            RuleFor(x => x.numeroTarjeta)
                .NotNull().WithMessage("El número de tarjeta no puede estar nulo.");
            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("El informe debe contener al menos un detalle.");
            RuleForEach(x => x.Detalles)
                .SetValidator(new DetallesInformeValidator());

        }
    }
}
