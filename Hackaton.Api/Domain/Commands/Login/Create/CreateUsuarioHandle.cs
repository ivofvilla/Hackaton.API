using FluentValidation;
using Hackaton.Api.Repository.Interface;
using MediatR;
using System.Reflection;

namespace Hackaton.Api.Domain.Commands.Login.Create
{
    public class CreateUsuarioHandle : IRequestHandler<CreateUsuarioCommand, bool>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IValidator<CreateUsuarioCommand> _validator;

        public CreateUsuarioHandle(ILoginRepository loginRepository, IValidator<CreateUsuarioCommand> validator)
        {
            _loginRepository = loginRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return false;
            }

            return await _loginRepository.LoginAsync(command.Email, command.Senha, cancellationToken);
        }
    }
}
