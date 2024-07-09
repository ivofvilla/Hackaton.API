using MediatR;

namespace Hackaton.Api.Domain.Commands.Agenda.Create
{
    public class CreateAgendaCommand : IRequest<bool>
    {
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime DataAgendamento { get; set; }
    }
}
