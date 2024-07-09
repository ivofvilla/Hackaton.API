using MediatR;

namespace Hackaton.Api.Domain.Commands.Agenda.UpdateGerenciar
{
    public class UpdateAgendaGerenciarCommand : IRequest<bool>
    {
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime NovaDataAgendamento { get; set; }
        public bool Encaixe { get; set; }
    }
}
