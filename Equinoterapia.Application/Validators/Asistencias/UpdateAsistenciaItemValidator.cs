using Equinoterapia.Application.DTOs.Asistencias;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Asistencias
{
    public class UpdateAsistenciaItemValidator : AbstractValidator<UpdateAsistenciaItemDto>
    {
        public UpdateAsistenciaItemValidator()
        {
            RuleFor(x => x.AsistenciasId)
                .GreaterThan(0).WithMessage("El ID de la asistencia es requerido.");
        }
    }
}