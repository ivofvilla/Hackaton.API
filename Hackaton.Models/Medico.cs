using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Hackaton.Models
{
    [Table(name: "Medico", Schema = "dbo")]
    public class Medico
    {
        [Key, Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("DataNascimento", TypeName = "datetime")]
        public DateTime DataNascimento { get; set; }

        [Column("CRM")]
        public string CRM { get; set; }

        [Column("Especialidade")]
        public string Especialidade { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Agenda> Agendas { get; set; } = new List<Agenda>();

        public Medico() { }

        public Medico(string nome, string email, DateTime dataNascimento, string crm, string especialidade)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            CRM = crm;
            Especialidade = especialidade;
            Ativo = true;
        }
    }
}