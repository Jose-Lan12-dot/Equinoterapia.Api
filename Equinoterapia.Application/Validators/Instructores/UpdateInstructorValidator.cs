using Equinoterapia.Application.DTOs.Instructores;
using Equinoterapia.Domain.Entities;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Instructores
{
    public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorDto>
    {
        public UpdateInstructorValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .When(x => x.Nombre is not null);

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                .Matches(@"^\d{10}$").WithMessage("El teléfono solo debe contener 10 dígitos numéricos.")
                .When(x => x.Telefono is not null);

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado no puede estar vacío.")
                .Must(BeValidEstado).WithMessage("El estado proporcionado no es válido.")
                .When(x => x.Estado is not null);
        }

        private bool BeValidEstado(string? estado)
        {
            return Enum.TryParse<EstadoInstructor>(estado, true, out _);
        }
    }
}