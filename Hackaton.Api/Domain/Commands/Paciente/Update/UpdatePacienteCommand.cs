using MediatR;

namespace Hackaton.Api.Domain.Commands.Paciente.Update
{
    public class UpdatePacienteCommand : IRequest<bool>
    {
        public UpdatePacienteCommand(int id) => Id = id;

        public int Id { get; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
