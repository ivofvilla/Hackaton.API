using FluentValidation;
using Hackaton.Api.Domain.Commands.Agenda.Update;

namespace Hackaton.Api.Domain.Commands.Agenda.UpdateGerenciar
{
    public class UpdateAgendaGerenciarValidation : AbstractValidator<UpdateAgendaCommand>
    {
        public UpdateAgendaGerenciarValidation()
        {
            RuleFor(command => command.IdMedico)
                .NotEmpty().WithMessage("O campo Médico é obrigatório.");

            RuleFor(command => command.IdPaciente)
                .NotEmpty().WithMessage("O campo Paciente é obrigatória.");

            RuleFor(command => command.DataAgendamento)
                .NotEmpty().WithMessage("O campo Data do agendamento é obrigatória.");
            RuleFor(command => command.DataAgendamento)
                .NotEmpty().WithMessage("O campo Nova Data do agendamento é obrigatória.");
        }
    }
}
