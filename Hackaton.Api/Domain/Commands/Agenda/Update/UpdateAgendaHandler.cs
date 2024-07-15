using FluentValidation;
using Hackaton.Api.Repository.Interface;
using MediatR;

namespace Hackaton.Api.Domain.Commands.Agenda.Update
{
    public class UpdateAgendaHandler : IRequestHandler<UpdateAgendaCommand, bool>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IAgendaRepository _agendamentoRepository;
        private readonly IValidator<UpdateAgendaCommand> _validator;

        public UpdateAgendaHandler(IMedicoRepository medicoRepository, IAgendaRepository agendamentoRepository
            ,IPacienteRepository pacienteRepository, IValidator<UpdateAgendaCommand> validator)
        {
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _agendamentoRepository = agendamentoRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(UpdateAgendaCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var agendamento = await _agendamentoRepository.GetAgendamentoAsync(command.Id, null, null, cancellationToken);

            agendamento.DataAgendamento = command.NovaDataAgendamento;
            
            await _agendamentoRepository.UpdateAsync(agendamento, cancellationToken);

            return true;
        }
    }
}
