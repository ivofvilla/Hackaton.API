using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Hackaton.Api.Domain.Commands.Login.Create;
using Hackaton.Api.Repository.Interface;


namespace Hackaton.Api.Test.Login
{
    public class CreateUsuarioHandleTests
    {
        private readonly Mock<ILoginRepository> _mockLoginRepository;
        private readonly Mock<IValidator<CreateUsuarioCommand>> _mockValidator;
        private readonly Mock<IPacienteRepository> _mockPacienteRepository;
        private readonly Mock<IMedicoRepository> _mockMedicoRepository;
        private readonly CreateUsuarioHandle _handler;

        public CreateUsuarioHandleTests()
        {
            _mockLoginRepository = new Mock<ILoginRepository>();
            _mockValidator = new Mock<IValidator<CreateUsuarioCommand>>();
            _mockPacienteRepository = new Mock<IPacienteRepository>();
            _mockMedicoRepository = new Mock<IMedicoRepository>();
            _handler = new CreateUsuarioHandle(_mockLoginRepository.Object, _mockValidator.Object, _mockPacienteRepository.Object, _mockMedicoRepository.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalso_QuandoValidacaoFalhar()
        {
            // Arrange
            var command = new CreateUsuarioCommand { Email = "email@example.com", Senha = "senha123" };
            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Email", "Erro") }));

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            bool resultadoBool = resultado is null;
            resultadoBool.Should().BeTrue();
            _mockLoginRepository.Verify(repo => repo.LoginAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveRetornarVerdadeiro_QuandoLoginForBemSucedido()
        {
            // Arrange
            var paciente = new Models.Paciente("Mario Bros", "email@paciente.com.br", DateTime.Now);
            paciente.Id = 456;

            var command = new CreateUsuarioCommand { Email = "drmario@medico.com", Senha = "senha123" };
            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            _mockLoginRepository.Setup(repo => repo.LoginAsync(command.Email, command.Senha, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new Models.Login { Id = 1, Medico = true, Email = command.Email });
            _mockMedicoRepository.Setup(repo => repo.GetAsync(null, null, command.Email, null, null, It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(new List<Models.Medico> { new Models.Medico { Id = 123 } });
            _mockPacienteRepository.Setup(repo => repo.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(paciente);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            bool resultadoBool = resultado is not null;
            resultadoBool.Should().BeTrue();
            _mockLoginRepository.Verify(repo => repo.LoginAsync(command.Email, command.Senha, It.IsAny<CancellationToken>()), Times.Once);
            _mockMedicoRepository.Verify(repo => repo.GetAsync(null, null, command.Email, null, null, It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoLoginForMalSucedido()
        {
            // Arrange
            var command = new CreateUsuarioCommand { Email = "email@example.com", Senha = "senha123" };
            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            _mockLoginRepository.Setup(repo => repo.LoginAsync(command.Email, command.Senha, It.IsAny<CancellationToken>()))
                                .ReturnsAsync((Models.Login)null);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            bool resultadoBool = resultado is null;
            resultadoBool.Should().BeTrue();
            _mockLoginRepository.Verify(repo => repo.LoginAsync(command.Email, command.Senha, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}