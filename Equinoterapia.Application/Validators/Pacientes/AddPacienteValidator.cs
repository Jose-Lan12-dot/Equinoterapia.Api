using Equinoterapia.Application.DTOs.Pacientes;
using Equinoterapia.Domain.Entities;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Pacientes
{
    public class AddPacienteValidator : AbstractValidator<AddPacienteDto>
    {
        public AddPacienteValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

            RuleFor(x => x.Tutor)
                .NotEmpty().WithMessage("El nombre del tutor es requerido.")
                .MinimumLength(3).WithMessage("El nombre del tutor debe tener al menos 3 caracteres.");

            RuleFor(x => x.TelefonoTutor)
                .NotEmpty().WithMessage("El teléfono es requerido.")
                .Length(10).WithMessage("El teléfono debe tener exactamente 10 dígitos.")
                .Matches(@"^\d$").WithMessage("El teléfono solo debe contener números.");

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("El género es requerido.")
                .Must(BeValidGenero).WithMessage("El género debe ser 'Masculino', 'Femenino' o 'NoBinario'.");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                    .WithMessage("La fecha de nacimiento no puede ser hoy o un día futuro.")
                .GreaterThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-150)))
                    .WithMessage("La fecha de nacimiento no puede ser hace más de 150 años.");
        }

        private bool BeValidGenero(string? genero)
        {
            return Enum.TryParse<Generos>(genero, true, out _);
        }
    }
}