﻿using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Hackaton.Models;

namespace Hackaton.Api.Repository.Interface
{
    public interface ILoginRepository
    {
        Task CreateAsync(Login login, CancellationToken cancellationToken);
        Task<bool> LoginAsync(string email, string senha, CancellationToken cancellationToken);
        Task SalvarAsync(CancellationToken cancellationToken);
    }
}
