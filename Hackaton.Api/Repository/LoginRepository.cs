using Hackaton.Api.Data;
using Hackaton.Models;
using Hackaton.Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Hackaton.Api.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContextClass _context;

        public LoginRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task CreateAsync(Login login, CancellationToken cancellationToken)
        {
            await _context.Login.AddAsync(login, cancellationToken);
        }

        public Task<bool> LoginAsync(string email, string senha, CancellationToken cancellationToken)
        {
            return _context.Login.AnyAsync(w => w.Email.Equals(email) && w.Senha.Equals(senha));
        }

        public async Task SalvarAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}