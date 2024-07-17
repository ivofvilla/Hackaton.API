using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Hackaton.Api.Repository.Interface;
using Hackaton.Api.Domain.Commands.Agenda.Create;
using FluentAssertions;

namespace Hackaton.Api.Test.Agenda
{
    public class CreateAgendaHandleTests
    {
        private readonly Mock<IAgendaRepository> _mockRepositorioAgenda;
        private readonly Mock<IValidator<CreateAgendaCommand>> _mockValidador;
        private readonly CreateAgendaHandle _manipulador;

        public CreateAgendaHandleTests()
        {
            _mockRepositorioAgenda = new Mock<IAgendaRepository>();
            _mockValidador = new Mock<IValidator<CreateAgendaCommand>>();
            _manipulador = new CreateAgendaHandle(_mockRepositorioAgenda.Object, _mockValidador.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalso_QuandoValidacaoFalhar()
        {
            // Arrange
            var comando = new CreateAgendaCommand { IdPaciente = 1, IdMedico = 1, DataAgendamento = DateTime.Now };
            _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("IdPaciente", "Erro") }));

            // Act
            var resultado = await _manipulador.Handle(comando);

            // Assert
            resultado.Should().BeFalse();
            _mockRepositorioAgenda.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Models.Agenda>(), It.IsAny<CancellationToken>()), Times.Never); ;
        }

        [Fact]
        public async Task Handle_DeveRetornarVerdadeiro_QuandoValidacaoForBemSucedida()
        {
            // Arrange
            var comando = new CreateAgendaCommand { IdPaciente = 1, IdMedico = 1, DataAgendamento = DateTime.Now };
            _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            _mockRepositorioAgenda.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Models.Agenda>(), It.IsAny<CancellationToken>()))
                                 .Returns(Task.CompletedTask);

            // Act
            var resultado = await _manipulador.Handle(comando);

            // Assert
            resultado.Should().BeTrue();
            _mockRepositorioAgenda.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Models.Agenda>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
