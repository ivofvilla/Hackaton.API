using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;

namespace Hackaton.Api.Repository.Interface
{
    public interface ILoginRepository
    {
        Task<bool> CreateAsync(string email, string senha, CancellationToken cancellationToken);
    }
}
