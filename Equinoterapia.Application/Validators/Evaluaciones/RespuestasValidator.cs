using Equinoterapia.Application.DTOs.Evaluaciones;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Evaluaciones
{
    public class RespuestasValidator : AbstractValidator<RespuestasDto>
    {
        public RespuestasValidator()
        {
            RuleFor(x => x.PreguntaId)
                .NotEmpty().WithMessage("La pregunta id no puede estar vacía.")
                .GreaterThan(0).WithMessage("El ID de la pregunta debe ser válido.");

            RuleFor(x => x.Respuesta)
                .NotEmpty().WithMessage("La respuesta no puede estar vacía.");
        }
    }
}