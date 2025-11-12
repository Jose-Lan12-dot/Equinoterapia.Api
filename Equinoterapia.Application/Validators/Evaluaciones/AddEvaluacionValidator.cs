using Equinoterapia.Application.DTOs.Evaluaciones;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Evaluaciones
{
    public class AddEvaluacionValidator : AbstractValidator<AddEvaluacionDto>
    {
        public AddEvaluacionValidator()
        {
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es requerida.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha no puede ser futura.");

            RuleFor(x => x.Respuestas)
                .NotEmpty().WithMessage("La lista de respuestas no puede estar vacía.");

            RuleForEach(x => x.Respuestas)
                .SetValidator(new RespuestasValidator());
        }
    }
}