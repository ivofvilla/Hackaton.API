using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hackaton.Api.Domain.Models
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
        [Column("Senha")]
        public string Senha { get; set; }
        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }
        [Column("CRM")]
        public string CRM { get; set; }
        [Column("Especialidade")]
        public string Especialidade { get; set; }
        [Column("Ativo")]
        public bool Ativo { get; set; }
        public IEnumerable<Agenda> Agendas { get; set; } = new List<Agenda>();

        public Medico()
        { 
        }

        public Medico(string nome, string email, string senha, DateTime dataNascimento, string CRM, string especialidade)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataNascimento = dataNascimento;
            this.CRM = CRM;
            this.Especialidade = especialidade;
            this.Ativo = true;
        }

        public void AdicionarAgenda(IEnumerable<Agenda> Agendas) => this.Agendas = Agendas;
    }
}