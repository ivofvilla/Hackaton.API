using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hackaton.Api.Domain.Commands.Agenda.Delete;
using Hackaton.Api.Domain.Models;
using Hackaton.Api.Repository.Interface;

namespace Hackaton.Api.Test.Agenda
{
    public class DeleteAgendamentoHandleTests
    {
        private readonly Mock<IAgendaRepository> _mockRepositorioAgenda;
        private readonly Mock<IValidator<DeleteAgendamentoCommand>> _mockValidador;
        private readonly DeleteAgendamentoHandle _manipulador;

        public DeleteAgendamentoHandleTests()
        {
            _mockRepositorioAgenda = new Mock<IAgendaRepository>();
            _mockValidador = new Mock<IValidator<DeleteAgendamentoCommand>>();
            _manipulador = new DeleteAgendamentoHandle(_mockRepositorioAgenda.Object, _mockValidador.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalso_QuandoValidacaoFalhar()
        {
            // Arrange
            var comando = new DeleteAgendamentoCommand { Id = 1 };
            _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Id", "Erro") }));

            // Act
            var resultado = await _manipulador.Handle(comando);

            // Assert
            resultado.Should().BeFalse();
            _mockRepositorioAgenda.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockRepositorioAgenda.Verify(repo => repo.DeleteLogicAsync(It.IsAny<Domain.Models.Agenda>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalso_QuandoAgendamentoNaoForEncontrado()
        {
            // Arrange
            var comando = new DeleteAgendamentoCommand { Id = 1 };
            _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            _mockRepositorioAgenda.Setup(repo => repo.GetByIdAsync(comando.Id, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync((Domain.Models.Agenda)null);

            // Act
            var resultado = await _manipulador.Handle(comando);

            // Assert
            resultado.Should().BeFalse();
            _mockRepositorioAgenda.Verify(repo => repo.GetByIdAsync(comando.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositorioAgenda.Verify(repo => repo.DeleteLogicAsync(It.IsAny<Domain.Models.Agenda>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarVerdadeiro_QuandoAgendamentoForEncontradoEDeletado()
        {
            // Arrange
            var comando = new DeleteAgendamentoCommand { Id = 1 };
            var agendamento = new Domain.Models.Agenda(1, 1, DateTime.Now) { Id = comando.Id, Ativo = true };

            _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            _mockRepositorioAgenda.Setup(repo => repo.GetByIdAsync(comando.Id, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(agendamento);
            _mockRepositorioAgenda.Setup(repo => repo.DeleteLogicAsync(agendamento, It.IsAny<CancellationToken>()))
                                  .Returns(Task.CompletedTask);

            // Act
            var resultado = await _manipulador.Handle(comando);

            // Assert
            resultado.Should().BeTrue();
            agendamento.Ativo.Should().BeFalse();
            _mockRepositorioAgenda.Verify(repo => repo.GetByIdAsync(comando.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepositorioAgenda.Verify(repo => repo.DeleteLogicAsync(agendamento, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}