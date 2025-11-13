using Equinoterapia.Application.DTOs.Asistencias;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Asistencias
{
    public class UpdateAsistenciaValidator : AbstractValidator<UpdateAsistenciaDto>
    {
        public UpdateAsistenciaValidator()
        {
            RuleFor(x => x.Asistencias)
                .NotEmpty().WithMessage("La lista de asistencias no puede estar vacía.");

            RuleForEach(x => x.Asistencias)
                .SetValidator(new UpdateAsistenciaItemValidator());
        }
    }
}