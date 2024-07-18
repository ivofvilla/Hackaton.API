using Hackaton.Models;
using System.Threading;

namespace Hackaton.Api.Repository.Interface
{
    public interface IAgendaRepository
    {
        Task<IEnumerable<Agenda>?> GetByIdMedicoAsync(int idMedico, CancellationToken cancellationToken = default);
        Task<Agenda?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteLogicAsync(Agenda client, CancellationToken cancellationToken = default);
        Task<IEnumerable<Agenda>?> GetAsync(int? Id, int? IdMedico, int? IdPaciente, DateTime? DataAgendamento, bool? EhMedico, CancellationToken cancellationToken = default);
        Task CreateAsync(Agenda agenda, CancellationToken cancellation = default);
        Task UpdateAsync(Agenda agenda, CancellationToken cancellationToken = default);
        Task<Agenda?> GetAgendamentoAsync(int? Id, int? IdMedico, int? IdPaciente, DateTime? DataAgendamento, CancellationToken cancellationToken = default);
    }
}
