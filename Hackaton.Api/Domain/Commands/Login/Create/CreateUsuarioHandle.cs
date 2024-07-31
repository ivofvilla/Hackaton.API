using FluentValidation;
using Hackaton.Api.Repository.Interface;
using MediatR;
using System.Reflection;

namespace Hackaton.Api.Domain.Commands.Login.Create
{
    public class CreateUsuarioHandle : IRequestHandler<CreateUsuarioCommand, LoginResult?>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IValidator<CreateUsuarioCommand> _validator;

        public CreateUsuarioHandle(ILoginRepository loginRepository, IValidator<CreateUsuarioCommand> validator, IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository)
        {
            _loginRepository = loginRepository;
            _validator = validator;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<LoginResult?> Handle(CreateUsuarioCommand command, CancellationToken cancellationToken = default)
        {
            int idUsuario = 0;
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return null;
            }

            var login = await _loginRepository.LoginAsync(command.Email, command.Senha, cancellationToken);

            if (login is null)
            {
                return null;
            }

            if (login.Medico)
            {
                var medicos = await _medicoRepository.GetAsync(null, null, login.Email, null, null, cancellationToken);
                idUsuario = medicos.FirstOrDefault().Id;
            }
            else
            {
                var paciente = await _pacienteRepository.GetByEmailAsync(login.Email, cancellationToken);
                idUsuario = paciente.Id;
            }

            return new LoginResult { Id = idUsuario, Medico = login.Medico};
        }
    }
}
