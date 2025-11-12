using Equinoterapia.Application.DTOs.Pacientes;
using Equinoterapia.Application.Validators.Archivos;
using FluentValidation;

namespace Equinoterapia.Application.Validators.Pacientes
{
    public class AddPacienteConArchivosValidator : AbstractValidator<AddPacienteConArchivosDto>
    {
        public AddPacienteConArchivosValidator()
        {
            RuleFor(x => x.Paciente)
                .NotNull().WithMessage("Los datos del paciente son requeridos.");

            RuleFor(x => x.Paciente)
                .SetValidator(new AddPacienteValidator());


            RuleForEach(x => x.Archivos)
                .SetValidator(new AddArchivoValidator())
                .When(x => x.Archivos is not null);

        }
    }
}
