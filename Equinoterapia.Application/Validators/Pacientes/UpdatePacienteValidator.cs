using Equinoterapia.Application.DTOs.Pacientes;
using Equinoterapia.Domain.Entities; 
using FluentValidation;

namespace Equinoterapia.Application.Validators.Pacientes
{
    public class UpdatePacienteValidator : AbstractValidator<UpdatePacienteDto>
    {
        public UpdatePacienteValidator()
        {
            RuleFor(x => x.PacienteId)
                .NotEmpty().WithMessage("El PacienteId es requerido.")
                .GreaterThan(0).WithMessage("El PacienteId debe ser válido.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .When(x => x.Nombre is not null);

            RuleFor(x => x.Tutor)
                .NotEmpty().WithMessage("El tutor no puede estar vacío.")
                .MinimumLength(3).WithMessage("El tutor debe tener al menos 3 caracteres.")
                .When(x => x.Tutor is not null);


            RuleFor(x => x.TelefonoTutor)
                .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                .Length(10).WithMessage("El teléfono debe tener exactamente 10 dígitos.")
                .Matches(@"^\d$").WithMessage("El teléfono solo debe contener números.")
                .When(x => x.TelefonoTutor is not null);

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("El género no puede estar vacío.")
                .Must(BeValidGenero).WithMessage("El género debe ser 'Masculino', 'Femenino' o 'No Binario'.")
                .When(x => x.Genero is not null);

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado no puede estar vacío.")
                .Must(BeValidEstado).WithMessage("El estado debe ser 'Activo', 'Baja' o 'TratamientoCompleto'.")
                .When(x => x.Estado is not null);

            RuleFor(x => x.FechaNacimiento)
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                    .WithMessage("La fecha de nacimiento no puede ser futura.")
                .GreaterThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-150)))
                    .WithMessage("La fecha de nacimiento no puede ser hace más de 150 años.")
                .When(x => x.FechaNacimiento.HasValue);
        }



        private bool BeValidGenero(string? genero)
        {
            return Enum.TryParse<Generos>(genero, true, out _);
        }

        private bool BeValidEstado(string? estado)
        {
            return Enum.TryParse<EstadoPaciente>(estado, true, out _);
        }
    }
}