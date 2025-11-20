using Equinoterapia.Application.DTOs.Informes;
using FluentValidation;
namespace Equinoterapia.Application.Validators.Informes
{
    public class DetallesInformeValidator : AbstractValidator<DetalleDto>
    {

        public DetallesInformeValidator()
        {
            RuleFor(x => x.Situacion)
                .NotNull().WithMessage("Los avances no pueden ser nulos.");
            RuleFor(x => x.Acciones)
                .NotNull().WithMessage("Los avances no pueden ser nulos.");
            RuleFor(x => x.Avances)
                .NotNull().WithMessage("Los avances no pueden ser nulos.");
            RuleFor(x => x.Tramite)
                .NotNull().WithMessage("Los avances no pueden ser nulos.");
            RuleFor(x => x.Observaciones)
                .NotNull().WithMessage("Los avances no pueden ser nulos.");
        }


    }
}
