using FluentValidation;
using Hackaton.Api.Repository.Interface;
using MediatR;
using System.Reflection;

namespace Hackaton.Api.Domain.Commands.Paciente.Create
{
    public class CreatePacienteHandler : IRequestHandler<CreatePacienteCommand, bool>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IValidator<CreatePacienteCommand> _validator;

        public CreatePacienteHandler(IPacienteRepository pacienteRepository, IValidator<CreatePacienteCommand> validator)
        {
            _pacienteRepository = pacienteRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreatePacienteCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var paciente = new Models.Paciente(command.Nome, command.Email, command.Senha, command.DataNascimento);

            await _pacienteRepository.CreateAsync(paciente, cancellationToken);

            return true;
        }
    }
}
