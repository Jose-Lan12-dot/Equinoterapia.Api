using Equinoterapia.Application.DTOs.Instructores;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Instructores
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorDto>
    {
        public AddInstructorValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es requerido.")
                .Matches(@"^\d{10}$").WithMessage("El teléfono solo debe contener 10 dígitos numéricos.");
        }
    }
}