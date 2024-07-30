using MediatR;

namespace Hackaton.Api.Domain.Commands.Agenda.Update
{
    public class UpdateAgendaCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime NovaDataAgendamento { get; set; }
        public int Hora { get; set; }
    }
}
