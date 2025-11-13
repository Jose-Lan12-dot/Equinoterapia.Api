using Equinoterapia.Application.DTOs.Evaluaciones;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Evaluaciones
{
    public class UpdateEvaluacionValidator : AbstractValidator<UpdateEvaluacionDto>
    {
        public UpdateEvaluacionValidator()
        {
            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es requerida.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha no puede ser futura.")
                .When(x => x.Fecha.HasValue);

            RuleForEach(x => x.Respuestas)
                .SetValidator(new RespuestasValidator());
        }
    }
}