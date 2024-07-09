using System.Reflection;

namespace Hackaton.Api.Domain.Queries.Agenda.Get
{
    public class GetAgendaResult
    {
        public IEnumerable<Models.Agenda>? Agendamentos { get; set; }

        public GetAgendaResult()
        {
            Agendamentos = new List<Models.Agenda>();
        }
    }
}
