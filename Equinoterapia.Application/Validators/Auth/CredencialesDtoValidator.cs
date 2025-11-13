using Equinoterapia.Application.DTOs; // O el namespace donde esté tu DTO
using Equinoterapia.Application.DTOs.Auth;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Auth
{
    public class CredencialesDtoValidator : AbstractValidator<CredencialesDto>
    {
        public CredencialesDtoValidator()
        {
            RuleFor(x => x.Usuario)
                .NotEmpty().WithMessage("El usuario es requerido.");

            RuleFor(x => x.contrasena)
                .NotEmpty().WithMessage("La contraseña es requerida.");
        }
    }
}