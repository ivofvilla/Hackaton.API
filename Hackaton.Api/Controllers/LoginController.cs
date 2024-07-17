using Hackaton.Api.Domain.Commands.Login.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Hackaton.Api.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/Login")]
        public async Task<IActionResult> Login([FromBody] CreateUsuarioCommand usuario, CancellationToken cancellation)
        {
            var result = await _mediator.Send(usuario, cancellation);

            if (result)
            {
                return Ok("Login Efetuado!");
            }

            return BadRequest("Erro ao efetuar login!");
        }
    }
}
