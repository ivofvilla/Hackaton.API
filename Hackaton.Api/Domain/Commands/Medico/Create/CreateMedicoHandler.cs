using FluentValidation;
using Hackaton.Api.Domain.Commands.Paciente.Create;
using Hackaton.Api.Repository.Interface;
using MediatR;
using System.Reflection;

namespace Hackaton.Api.Domain.Commands.Medico.Create
{
    public class CreateMedicoHandler : IRequestHandler<CreateMedicoCommand, bool>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IValidator<CreateMedicoCommand> _validator;

        public CreateMedicoHandler(IMedicoRepository medicoRepository, IValidator<CreateMedicoCommand> validator)
        {
            _medicoRepository = medicoRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateMedicoCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            var paciente = new Models.Medico(command.Nome, command.Email, command.Senha, command.DataNascimento, command.CRM, command.Especialidade);

            await _medicoRepository.CreateAsync(paciente, cancellationToken);

            return true;
        }
    }
}
