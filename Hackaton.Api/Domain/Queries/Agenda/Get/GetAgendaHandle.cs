using Hackaton.Api.Repository.Interface;
using MediatR;

namespace Hackaton.Api.Domain.Queries.Agenda.Get
{
    public class GetAgendaHandle : IRequestHandler<GetAgendaQuery, GetAgendaResult>
    {
        private readonly IAgendaRepository _agendaRepository;

        public GetAgendaHandle(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<GetAgendaResult> Handle(GetAgendaQuery query, CancellationToken cancellationToken)
        {
            var result = await _agendaRepository.GetAsync(null, query.IdMedico, query.IdPaciente, query.DataAgendamento, query.EhMedico, cancellationToken);
            var agenda = new GetAgendaResult();
            agenda.Agendamentos = result;

            return agenda;
        }
    }
}
