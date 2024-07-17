using Hackaton.Api.Data;
using Hackaton.Api.Domain.Models;
using Hackaton.Api.Repository.Interface;
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
            IQueryable<Agenda>? agendas = _context.Agenda;

            if (Id != null)
            {
                return await _context.Agenda.ToListAsync();
            }

            if (IdMedico is not null)
            {
                agendas = agendas.Where(w => w.IdMedico == IdMedico);
            }

            if (IdPaciente is not null)
            {
                agendas = agendas.Where(w => w.IdPaciente == IdPaciente);
            }
            if (EhMedico is not null)
            {
                agendas = agendas.Where(w => w.Ativo == EhMedico);
            }
            if (DataAgendamento is not null)
            {
                agendas = agendas.Where(w => w.DataAgendamento == DataAgendamento);
            }
            
            return await agendas.ToListAsync(cancellationToken);
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