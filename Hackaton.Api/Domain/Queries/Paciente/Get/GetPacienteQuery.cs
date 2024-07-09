using MediatR;

namespace Hackaton.Api.Domain.Queries.Paciente.Get
{
    public class GetPacienteQuery : IRequest<GetPacienteResult?>
    {
        public int Id { get; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
