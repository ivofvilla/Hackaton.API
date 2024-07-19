using MediatR;

namespace Hackaton.Api.Domain.Commands.Login.Create
{
    public class CreateUsuarioCommand : IRequest<int?>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
