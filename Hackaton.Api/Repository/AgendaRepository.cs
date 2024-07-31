using Hackaton.Api.Data;
using Hackaton.Api.Repository.Interface;
using Hackaton.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Api.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly DbContextClass _context;

        public AgendaRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task DeleteLogicAsync(Agenda client, CancellationToken cancellationToken = default)
        {
            _context.Agenda.Update(client);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Agenda>?> GetAsync(int? Id, int? IdMedico, int? IdPaciente, DateTime? DataAgendamento, bool? EhMedico, CancellationToken cancellationToken = default)
        {
            IQueryable<Agenda>? agendas = _context.Agenda.AsNoTracking()
                                                 .Include(k => k.Paciente).AsNoTracking()
                                                 .Include(k => k.Medico).AsNoTracking();

            if (IdMedico is not null)
                agendas = agendas.Where(k => k.Medico.Id == IdMedico && k.Ativo == true);

            if (IdPaciente is not null)
                agendas = agendas.Where(k => k.IdPaciente == IdPaciente && k.Ativo == true);

            if (DataAgendamento is not null)
                agendas = agendas.Where(w => w.DataAgendamento == DataAgendamento);
            
            return await agendas.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Agenda>?> GetAsyncMedico(int? Id, int? IdMedico, int? IdPaciente, CancellationToken cancellationToken = default)
        {
            IQueryable<Agenda>? agendas = _context.Agenda;

            if (IdMedico is not null)
            {
                agendas = agendas.AsNoTracking().Include(k => k.Paciente).AsNoTracking().Where(k => k.IdMedico == IdMedico && k.Ativo == true);
            }

            if (IdMedico is not null)
            {
                agendas = agendas.AsNoTracking().Include(k => k.Paciente).AsNoTracking().Where(k => k.IdPaciente == IdPaciente && k.Ativo == true);
            }

            return await agendas.ToListAsync();
        }

        public async Task CreateAsync(Agenda agenda, CancellationToken cancellation = default)
        {
            _context.Agenda.Add(agenda);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task<IEnumerable<Agenda>?> GetByIdMedicoAsync(int idMedico, CancellationToken cancellationToken = default)
        {
            return await _context.Agenda.Where(w => w.IdMedico == idMedico).ToListAsync();
        }

        public async Task<Agenda?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Agenda.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Agenda agenda, CancellationToken cancellationToken = default)
        {
            _context.Agenda.Update(agenda);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Agenda?> GetAgendamentoAsync(int? Id, int? IdMedico, int? IdPaciente, DateTime? DataAgendamento, CancellationToken cancellationToken = default)
        {
            IQueryable<Agenda>? agendas = _context.Agenda;

            if (Id is not null)
            {
                agendas = agendas.Where(w => w.Id == Id);
            }

            if (IdMedico is not null)
            {
                agendas = agendas.Where(w => w.IdMedico == IdMedico);
            }

            if (IdPaciente is not null)
            {
                agendas = agendas.Where(w => w.IdPaciente == IdPaciente);
            }

            if (DataAgendamento is not null)
            {
                agendas = agendas.Where(w => w.DataAgendamento == DataAgendamento);
            }

            return await agendas.FirstOrDefaultAsync(cancellationToken);
        }

    }
}