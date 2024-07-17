using Hackaton.Api.Data;
using Hackaton.Api.Domain.Models;
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

        public async Task CreateAsync(Login login, CancellationToken cancellationToken)
        {
            await _context.Login.AddAsync(login, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> LoginAsync(string email, string senha, CancellationToken cancellationToken)
        {
            return _context.Login.AnyAsync(w => w.Email.Equals(email) && w.Senha.Equals(senha));
        }
    }
}