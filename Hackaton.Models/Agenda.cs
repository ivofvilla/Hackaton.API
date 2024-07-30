using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Hackaton.Models
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
        
        [Column("Ativo")]
        public bool Ativo { get; set; }

        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }

        public Agenda(int IdPaciente, int IdMedico, DateTime DataAgendamento)
        {
            this.IdPaciente = IdPaciente;
            this.IdMedico = IdMedico;
            this.DataAgendamento = DataAgendamento;
            this.Ativo = true;
        }
    }
}