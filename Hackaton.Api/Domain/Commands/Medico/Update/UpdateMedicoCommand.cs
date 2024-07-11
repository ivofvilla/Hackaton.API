using Hackaton.Api.Domain.Models;
using MediatR;
using System.Security.Cryptography;

namespace Hackaton.Api.Domain.Commands.Medico.Update
{
    public class UpdateMedicoCommand : IRequest<bool>
    {
        public UpdateMedicoCommand(int id) => Id = id;

        public int Id { get; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
