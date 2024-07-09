using FluentValidation;

namespace Hackaton.Api.Domain.Commands.Agenda.Create
{
    public class CreateAgendaValidator : AbstractValidator<CreateAgendaCommand>
    {
        public CreateAgendaValidator()
        {
            RuleFor(command => command.IdMedico)
                .NotEmpty().WithMessage("O campo Médico é obrigatório.");

            RuleFor(command => command.IdPaciente)
                .NotEmpty().WithMessage("O campo Paciente é obrigatória.");

            RuleFor(command => command.DataAgendamento)
                .NotEmpty().WithMessage("O campo Data do agendamento é obrigatória.");
        }
    }
}
