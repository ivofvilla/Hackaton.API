using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System;
using System.Collections.Generic;

namespace Hackaton.Models
{
    [Table(name: "Paciente", Schema = "dbo")]
    public class Paciente
    {
        [Key, Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("DataNascimento", TypeName = "datetime")]
        public DateTime DataNascimento { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Agenda> Agendas { get; set; } = new List<Agenda>();

        public Paciente(string nome, string email, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = true;
        }
    }
}
