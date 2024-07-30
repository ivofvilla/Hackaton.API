using Hackaton.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Hackaton.Worker.Repository.Interface;
using Microsoft.Extensions.Logging;
using Hackaton.Worker;

namespace Hackaton.Api.Test.Worker
{
    public class WorkerTest
    {

        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IServiceProvider> _serviceProvider;
        private readonly Mock<ILogger<Hackaton.Worker.Worker>> _mockLogger;
        private readonly Hackaton.Worker.Worker _worker;


        public WorkerTest()
        {
            _emailServiceMock = new Mock<IEmailService>();
            _serviceProvider = new Mock<IServiceProvider>();
            _mockLogger = new Mock<ILogger<Hackaton.Worker.Worker>>();
            _worker = new Hackaton.Worker.Worker(_serviceProvider.Object, _mockLogger.Object, _emailServiceMock.Object);
        }

        [Fact]
        public async Task Worker_Executa_E_Envia_Email()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextClass>()
                .UseInMemoryDatabase(databaseName: "HakatonDatabaseTeste")
                .Options;

            using var context = new DbContextClass(options);

            var medico1 = new Models.Medico("dr Mario", "mario@medico.com", DateTime.Now.AddYears(-50), "exemplo1", "Nefrologista");
            var medico2 = new Models.Medico("dr Luigi", "luigi@medico.com", DateTime.Now.AddYears(-50), "exemplo2", "Proctologista");

            context.Medico.AddRange(medico1, medico2);

            var paciente1 = new Models.Paciente("Nome1", "paciente1@example.com", DateTime.Now.AddYears(-30));
            var paciente2 = new Models.Paciente("Nome2", "paciente2@example.com", DateTime.Now.AddYears(-25));

            context.Paciente.AddRange(paciente1, paciente2);

            var agenda1 = new Models.Agenda(1, 1, DateTime.Now.AddDays(1));
            var agenda2 = new Models.Agenda(2, 2, DateTime.Now.AddDays(1));

            context.Agenda.AddRange(agenda1, agenda2);

            await context.SaveChangesAsync();

            // Criação do ServiceProvider com o DbContext em memória
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(context)
                .AddSingleton(_emailServiceMock.Object)
                .BuildServiceProvider();

            // Atualize o worker para usar o serviceProvider
            var worker = new Hackaton.Worker.Worker(serviceProvider.GetService<IServiceProvider>(), serviceProvider.GetService<ILogger<Hackaton.Worker.Worker>>(), serviceProvider.GetService<IEmailService>());

            // Act
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
            await worker.StartAsync(cancellationTokenSource.Token);

            // Assert
            _emailServiceMock.Verify(e => e.EnviarEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Worker_Ignora_Agendamentos_Que_Nao_Estao_No_Proximo_Dia_Util()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextClass>()
                .UseInMemoryDatabase(databaseName: "HakatonDatabaseTestev2")
                .Options;

            using var context = new DbContextClass(options);

            var agenda1 = new Models.Agenda(1, 1, DateTime.Now.AddDays(1));
            agenda1.Id = 1;

            var agenda2 = new Models.Agenda(2, 2, DateTime.Now.AddDays(1));
            agenda2.Id = 2;

            var paciente1 = new Models.Paciente("Nome1", "paciente1@example.com", DateTime.Now.AddYears(-30));
            paciente1.Id = 1;
            var paciente2 = new Models.Paciente("Nome2", "paciente1@example.com", DateTime.Now.AddYears(-25));
            paciente2.Id = 2;

            context.Medico.AddRange(
                new Models.Medico { Id = 1, Nome = "Dr. Medico1" },
                new Models.Medico { Id = 2, Nome = "Dr. Medico2" }
            );

            await context.SaveChangesAsync();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(context)
                .AddSingleton(_emailServiceMock.Object)
                .BuildServiceProvider();

            var worker = new Hackaton.Worker.Worker(serviceProvider.GetService<IServiceProvider>(), serviceProvider.GetService<ILogger<Hackaton.Worker.Worker>>(), serviceProvider.GetService<IEmailService>());


            // Act
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
            await worker.StartAsync(cancellationTokenSource.Token);

            // Assert
            _emailServiceMock.Verify(e => e.EnviarEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Worker_Lida_Com_Agendamentos_Sem_Pacientes_Ou_Medicos()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextClass>()
                .UseInMemoryDatabase(databaseName: "HakatonDatabaseTeste")
                .Options;

            using var context = new DbContextClass(options);

            var agenda = new Models.Agenda(999,999, DateTime.Now.AddDays(1));
            agenda.Id = 1;

            context.Agenda.Add(agenda);

            await context.SaveChangesAsync();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(context)
                .AddSingleton(_emailServiceMock.Object)
                .BuildServiceProvider();

            // Act
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
            await _worker.StartAsync(cancellationTokenSource.Token);

            // Assert
            _emailServiceMock.Verify(e => e.EnviarEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Worker_Nao_Envia_Emails_Quando_Nao_Ha_Agendamentos_No_Proximo_Dia_Util()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextClass>()
                .UseInMemoryDatabase(databaseName: "HakatonDatabaseTeste")
                .Options;

            using var context = new DbContextClass(options);

            await context.SaveChangesAsync();

            var emailServiceMock = new Mock<IEmailService>();
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(context)
                .AddSingleton(emailServiceMock.Object)
                .BuildServiceProvider();

            // Act
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
            await _worker.StartAsync(cancellationTokenSource.Token);

            // Assert
            emailServiceMock.Verify(e => e.EnviarEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Worker_Lida_Com_Excecoes_Ao_Acessar_Banco_De_Dados()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DbContextClass>()
                .UseInMemoryDatabase(databaseName: "HakatonDatabaseTestev5")
                .Options;

            using var context = new DbContextClass(options);

            var paciente1 = new Models.Paciente("Nome1", "paciente1@example.com", DateTime.Now.AddYears(-30));
            paciente1.Id = 1;

            var agenda1 = new Models.Agenda(1, 1, DateTime.Now.AddDays(1));
            agenda1.Id = 1;

            context.Agenda.Add(agenda1);

            context.Paciente.Add(paciente1);
            context.Medico.Add(new Models.Medico { Id = 1, Nome = "Dr. Medico1" }); ;

            await context.SaveChangesAsync();

            var emailServiceMock = new Mock<IEmailService>();
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(context)
                .AddSingleton(emailServiceMock.Object)
                .BuildServiceProvider();

            // Simular exceção ao acessar o banco de dados
            var mockDbContext = new Mock<DbContextClass>(options);
            mockDbContext.Setup(db => db.Agenda).Throws(new Exception("Erro ao acessar o banco de dados"));



            // Act
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
            await _worker.StartAsync(cancellationTokenSource.Token);

            // Assert
            emailServiceMock.Verify(e => e.EnviarEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()), Times.Never);
        }


    }
}
