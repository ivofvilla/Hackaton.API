using MediatR;

namespace Hackaton.Api.Domain.Commands.Agenda.Delete
{
    public class DeleteAgendamentoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
