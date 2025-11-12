using Equinoterapia.Application.DTOs.Archivos;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Archivos
{
    public class AddArchivoValidator : AbstractValidator<AddArchivoDto>
    {
        public AddArchivoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del archivo es requerido.");

            RuleFor(x => x.Archivo)
                .NotNull().WithMessage("El archivo es requerido.");

            RuleFor(x => x.Archivo.Length)
                .GreaterThan(0).WithMessage("El archivo no puede estar vacío.")
                .When(x => x.Archivo is not null);
        }
    }
}