using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hackaton.Api.Domain.Models
{
    [Table(name: "Agenda", Schema = "dbo")]
    public class Agenda
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("IdPaciente")]
        public int IdPaciente { get; set; }
        [Column("IdMedico")]
        public int IdMedico { get; set; }
        [Column("DataAgendamento")]
        public DateTime DataAgendamento { get; set; }
        [Column("EhMedico")]
        public bool EhMedico { get; set; }
        [Column("Ativo")]
        public bool Ativo { get; set; }

        public Agenda()
        {
        }

        public Agenda(int IdPaciente, int IdMedico, DateTime DataAgendamento, bool ehMedico)
        {
            this.IdPaciente = IdPaciente;
            this.IdMedico = IdMedico;
            this.DataAgendamento = DataAgendamento;
            this.EhMedico = ehMedico;
            this.Ativo = true;
        }


    }
}