using FluentValidation;
using Hackaton.Api.Domain.Commands.Medico.Delete;
using Hackaton.Api.Repository.Interface;
using MediatR;

namespace Hackaton.Api.Domain.Commands.Agenda.Delete
{
    public class DeleteAgendamentoHandle : IRequestHandler<DeleteAgendamentoCommand, bool>
    {
        private readonly IAgendaRepository _agendamentoRepository;
        private readonly IValidator<DeleteAgendamentoCommand> _validator;

        public DeleteAgendamentoHandle(IAgendaRepository agendamentoRepository, IValidator<DeleteAgendamentoCommand> validator)
        {
            _agendamentoRepository = agendamentoRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteAgendamentoCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var agendamento = await _agendamentoRepository.GetByIdAsync(command.Id, cancellationToken);
            if (agendamento == null)
            {
                return false;
            }

            agendamento.Ativo = false;

            await _agendamentoRepository.DeleteLogicAsync(agendamento, cancellationToken);

            return true;
        }
    }
}
