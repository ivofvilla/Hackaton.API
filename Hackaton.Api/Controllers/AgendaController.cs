using Hackaton.Api.Domain.Commands.Agenda.Create;
using Hackaton.Api.Domain.Commands.Agenda.Delete;
using Hackaton.Api.Domain.Commands.Agenda.Update;
using Hackaton.Api.Domain.Commands.Agenda.UpdateGerenciar;
using Hackaton.Api.Domain.Queries.Agenda.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hackaton.Api.Controllers
{
    public class AgendaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AgendaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/agenda/agendar")]
        public async Task<IActionResult> Criar(CreateAgendaCommand agenda, CancellationToken cancellation)
        {
            var result = await _mediator.Send(agenda, cancellation);

            if (result != null)
            {
                return Created();
            }

            return BadRequest("Erro ao cadastrar o Agendamento!");

        }

        [HttpPut]
        [Route("api/agenda/reagendar")]
        public async Task<IActionResult> Alterar(UpdateAgendaCommand agenda, CancellationToken cancellation)
        {
            var result = await _mediator.Send(agenda, cancellation);

            if (result != null)
            {
                return NoContent();
            }

            return BadRequest("Erro ao atualizar o Agendamento!");

        }


        [HttpPut]
        [Route("api/agenda/gerenciar")]
        public async Task<IActionResult> Alterar(UpdateAgendaGerenciarCommand agenda, CancellationToken cancellation)
        {
            var result = await _mediator.Send(agenda, cancellation);

            if (result != null)
            {
                return NoContent();
            }

            return BadRequest("Erro ao atualizar o Agendamento!");

        }

        [HttpGet]
        [Route("api/agenda/agenda")]
        public async Task<IActionResult> Medicos(GetAgendaQuery agenda, CancellationToken cancellation)
        {
            var result = await _mediator.Send(agenda, cancellation);

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return Ok(JsonConvert.SerializeObject(result, jsonSettings));

        }

        [HttpDelete]
        [Route("api/medico/cancelar/{id}")]
        public async Task<IActionResult> Remover(int Id, CancellationToken cancellation)
        {
            var command = new DeleteAgendamentoCommand { Id = Id };
            await _mediator.Send(command, cancellation);
            return Ok("Médico apagado com sucesso!");
        }
    }
}
