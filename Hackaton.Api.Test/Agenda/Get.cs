using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Hackaton.Api.Domain.Queries.Agenda.Get;
using Hackaton.Api.Repository.Interface;
using Hackaton.Api.Domain.Models;
using System.Collections.Generic;
using FluentAssertions;

namespace Hackaton.Api.Test.Agenda
{
    public class GetAgendaHandleTests
    {
        private readonly Mock<IAgendaRepository> _mockRepositorioAgenda;
        private readonly GetAgendaHandle _manipulador;

        public GetAgendaHandleTests()
        {
            _mockRepositorioAgenda = new Mock<IAgendaRepository>();
            _manipulador = new GetAgendaHandle(_mockRepositorioAgenda.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarResultadosCorretamente()
        {
            // Arrange
            var query = new GetAgendaQuery
            {
                Id = 1,
                IdMedico = 2,
                IdPaciente = 3,
                DataAgendamento = DateTime.Now,
                EhMedico = true
            };

            var agendamentos = new List<Domain.Models.Agenda>
        {
            new Domain.Models.Agenda(3, 2, DateTime.Now) { Id = 1 }
        };

            _mockRepositorioAgenda.Setup(repo => repo.GetAsync(query.Id, query.IdMedico, query.IdPaciente, query.DataAgendamento, query.EhMedico, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(agendamentos);

            // Act
            var resultado = await _manipulador.Handle(query, CancellationToken.None);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Agendamentos.Should().BeEquivalentTo(agendamentos);
            _mockRepositorioAgenda.Verify(repo => repo.GetAsync(query.Id, query.IdMedico, query.IdPaciente, query.DataAgendamento, query.EhMedico, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoExistemAgendamentos()
        {
            // Arrange
            var query = new GetAgendaQuery
            {
                Id = 1,
                IdMedico = 2,
                IdPaciente = 3,
                DataAgendamento = DateTime.Now,
                EhMedico = true
            };

            var agendamentos = new List<Domain.Models.Agenda>();

            _mockRepositorioAgenda.Setup(repo => repo.GetAsync(query.Id, query.IdMedico, query.IdPaciente, query.DataAgendamento, query.EhMedico, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(agendamentos);

            // Act
            var resultado = await _manipulador.Handle(query, CancellationToken.None);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Agendamentos.Should().BeEquivalentTo(agendamentos);
            _mockRepositorioAgenda.Verify(repo => repo.GetAsync(query.Id, query.IdMedico, query.IdPaciente, query.DataAgendamento, query.EhMedico, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}