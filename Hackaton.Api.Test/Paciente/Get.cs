using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Hackaton.Api.Domain.Queries.Paciente.Get;
using Hackaton.Api.Repository.Interface;
using Hackaton.Api.Domain.Models;
using System.Collections.Generic;

public class GetPacienteHandleTests
{
    private readonly Mock<IPacienteRepository> _mockPacienteRepository;
    private readonly GetPacienteHandle _handler;

    public GetPacienteHandleTests()
    {
        _mockPacienteRepository = new Mock<IPacienteRepository>();
        _handler = new GetPacienteHandle(_mockPacienteRepository.Object);
    }

    [Fact]
    public async Task Handle_DeveRetornarPaciente_QuandoEncontrado()
    {
        // Arrange
        var query = new GetPacienteQuery { Id = 1 };
        var pacienteRetornado = new Paciente { Id = 1, Nome = "João Silva", Email = "joao@example.com", DataNascimento = new DateTime(1985, 3, 10) };
        var listaPacientes = new List<Paciente> { pacienteRetornado };
        _mockPacienteRepository.Setup(repo => repo.GetByIdAsync(query.Id.Value, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(listaPacientes);

        // Act
        var resultado = await _handler.Handle(query, CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Pacientes.Should().ContainSingle();
        resultado.Pacientes.Should().ContainEquivalentOf(pacienteRetornado);
    }

    [Fact]
    public async Task Handle_DeveRetornarNull_QuandoNaoEncontrado()
    {
        // Arrange
        var query = new GetPacienteQuery { Id = 999 }; // ID que não existe
        _mockPacienteRepository.Setup(repo => repo.GetByIdAsync(query.Id.Value, It.IsAny<CancellationToken>()))
                               .ReturnsAsync((List<Paciente>)null);

        // Act
        var resultado = await _handler.Handle(query, CancellationToken.None);

        // Assert
        resultado.Should().BeNull();
    }
}
