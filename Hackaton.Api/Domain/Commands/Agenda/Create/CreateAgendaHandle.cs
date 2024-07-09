using FluentValidation;
using Hackaton.Api.Repository.Interface;
using MediatR;
using System.Reflection;

namespace Hackaton.Api.Domain.Commands.Agenda.Create
{
    public class CreateAgendaHandle : IRequestHandler<CreateAgendaCommand, bool>
    {
        private readonly IAgendaRepository _agendamentoRepository;
        private readonly IValidator<CreateAgendaCommand> _validator;

        public CreateAgendaHandle(IAgendaRepository agendamentoRepository, IValidator<CreateAgendaCommand> validator)
        {
            _agendamentoRepository = agendamentoRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateAgendaCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var agendamento = new Models.Agenda(command.IdPaciente, command.IdMedico, command.DataAgendamento, true);

            await _agendamentoRepository.CreateAsync(agendamento, cancellationToken);

            return true;
        }
    }
}
