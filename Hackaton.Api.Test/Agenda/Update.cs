﻿using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hackaton.Api.Domain.Commands.Agenda.Update;
using Hackaton.Api.Repository.Interface;
using Hackaton.Api.Domain.Models;

public class UpdateAgendaHandlerTests
{
    private readonly Mock<IMedicoRepository> _mockRepositorioMedico;
    private readonly Mock<IPacienteRepository> _mockRepositorioPaciente;
    private readonly Mock<IAgendaRepository> _mockRepositorioAgenda;
    private readonly Mock<IValidator<UpdateAgendaCommand>> _mockValidador;
    private readonly UpdateAgendaHandler _manipulador;

    public UpdateAgendaHandlerTests()
    {
        _mockRepositorioMedico = new Mock<IMedicoRepository>();
        _mockRepositorioPaciente = new Mock<IPacienteRepository>();
        _mockRepositorioAgenda = new Mock<IAgendaRepository>();
        _mockValidador = new Mock<IValidator<UpdateAgendaCommand>>();
        _manipulador = new UpdateAgendaHandler(_mockRepositorioMedico.Object, _mockRepositorioAgenda.Object, _mockRepositorioPaciente.Object, _mockValidador.Object);
    }

    [Fact]
    public async Task Handle_DeveRetornarFalso_QuandoValidacaoFalhar()
    {
        // Arrange
        var comando = new UpdateAgendaCommand { Id = 1, NovaDataAgendamento = DateTime.Now.AddDays(1) };
        _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Id", "Erro") }));

        // Act
        var resultado = await _manipulador.Handle(comando);

        // Assert
        resultado.Should().BeFalse();
        _mockRepositorioAgenda.Verify(repo => repo.GetAgendamentoAsync(It.IsAny<int>(), null, null, null, It.IsAny<CancellationToken>()), Times.Never);
        _mockRepositorioAgenda.Verify(repo => repo.UpdateAsync(It.IsAny<Agenda>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DeveRetornarFalso_QuandoAgendamentoNaoForEncontrado()
    {
        // Arrange
        var comando = new UpdateAgendaCommand { Id = 1, NovaDataAgendamento = DateTime.Now.AddDays(1) };
        _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());
        _mockRepositorioAgenda.Setup(repo => repo.GetAgendamentoAsync(comando.Id, null, null, null, It.IsAny<CancellationToken>()))
                              .ReturnsAsync((Agenda)null);

        // Act
        var resultado = await _manipulador.Handle(comando);

        // Assert
        resultado.Should().BeFalse();
        _mockRepositorioAgenda.Verify(repo => repo.GetAgendamentoAsync(comando.Id, null, null, null, It.IsAny<CancellationToken>()), Times.Once);
        _mockRepositorioAgenda.Verify(repo => repo.UpdateAsync(It.IsAny<Agenda>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DeveRetornarVerdadeiro_QuandoAgendamentoForEncontradoEAtualizado()
    {
        // Arrange
        var comando = new UpdateAgendaCommand { Id = 1, NovaDataAgendamento = DateTime.Now.AddDays(1) };
        var agendamento = new Agenda(1, 1, DateTime.Now) { Id = comando.Id };

        _mockValidador.Setup(v => v.ValidateAsync(comando, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());
        _mockRepositorioAgenda.Setup(repo => repo.GetAgendamentoAsync(comando.Id, null, null, null, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(agendamento);
        _mockRepositorioAgenda.Setup(repo => repo.UpdateAsync(agendamento, It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask);

        // Act
        var resultado = await _manipulador.Handle(comando);

        // Assert
        resultado.Should().BeTrue();
        agendamento.DataAgendamento.Should().Be(comando.NovaDataAgendamento);
        _mockRepositorioAgenda.Verify(repo => repo.GetAgendamentoAsync(comando.Id, null, null, null, It.IsAny<CancellationToken>()), Times.Once);
        _mockRepositorioAgenda.Verify(repo => repo.UpdateAsync(agendamento, It.IsAny<CancellationToken>()), Times.Once);
    }
}