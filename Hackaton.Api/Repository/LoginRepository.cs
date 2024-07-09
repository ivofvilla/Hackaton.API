using Hackaton.Api.Data;
using Hackaton.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Api.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContextClass _context;

        public LoginRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(string email, string senha, CancellationToken cancellationToken)
        {
            bool logou = false;

            var resultadoPaciente =  await _context.Paciente.FirstOrDefaultAsync(w => w.Email.Equals(email) && w.Senha.Equals(senha));

            if(resultadoPaciente is null) {
                var resultadoMedico = await _context.Medico.FirstOrDefaultAsync(w => w.Email.Equals(email) && w.Senha.Equals(senha));
            }

            return logou;
        }
    }
}