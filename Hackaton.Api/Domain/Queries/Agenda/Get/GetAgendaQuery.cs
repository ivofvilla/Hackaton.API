using MediatR;

namespace Hackaton.Api.Domain.Queries.Agenda.Get
{
    public class GetAgendaQuery : IRequest<GetAgendaResult?>
    {
        public DateTime DataAgendamento { get; set; }
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public bool EhMedico { get; set; }
    }
}
