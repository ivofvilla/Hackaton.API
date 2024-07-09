using FluentValidation;
using Hackaton.Api.Domain.Commands.Agenda.Update;
using Hackaton.Api.Repository.Interface;
using MediatR;
using System.Reflection;

namespace Hackaton.Api.Domain.Commands.Agenda.UpdateGerenciar
{
    public class UpdateAgendaGerenciarHandle : IRequestHandler<UpdateAgendaGerenciarCommand, bool>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IAgendaRepository _agendamentoRepository;
        private readonly IValidator<UpdateAgendaGerenciarCommand> _validator;

        public UpdateAgendaGerenciarHandle(IMedicoRepository medicoRepository, IAgendaRepository agendamentoRepository
            , IPacienteRepository pacienteRepository, IValidator<UpdateAgendaGerenciarCommand> validator)
        {
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _agendamentoRepository = agendamentoRepository;
            _validator = validator;
        }


        public async Task<bool> Handle(UpdateAgendaGerenciarCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var medico = await _medicoRepository.GetByIdAsync(command.IdMedico, cancellationToken);
            if (medico == null)
            {
                return false;
            }

            var paciente = await _medicoRepository.GetByIdAsync(command.IdPaciente, cancellationToken);
            if (paciente == null)
            {
                return false;
            }

            var agendamento = await _agendamentoRepository.GetAgendamentoAsync(command.IdPaciente, command.IdMedico, command.DataAgendamento, true);

            if (agendamento is null || (agendamento.DataAgendamento == command.NovaDataAgendamento && command.Encaixe))
            {
                var novoAgendamento = new Models.Agenda(command.IdPaciente, command.IdMedico, command.DataAgendamento, true);
                await _agendamentoRepository.CreateAsync(novoAgendamento);
            }
            else
            {
                agendamento.DataAgendamento = command.NovaDataAgendamento;
                await _agendamentoRepository.UpdateAsync(agendamento, cancellationToken);
            }

            return true;
        }
    }
}
