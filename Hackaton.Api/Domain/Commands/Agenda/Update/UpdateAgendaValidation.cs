using FluentValidation;
using Hackaton.Api.Domain.Commands.Agenda.Create;

namespace Hackaton.Api.Domain.Commands.Agenda.Update
{
    public class UpdateAgendaValidation : AbstractValidator<UpdateAgendaCommand>
    {
        public UpdateAgendaValidation()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("O agendamento é obrigatório.");
            RuleFor(command => command.NovaDataAgendamento)
                .NotEmpty().WithMessage("O campo Nova Data do agendamento é obrigatória.");
        }
    }
}
