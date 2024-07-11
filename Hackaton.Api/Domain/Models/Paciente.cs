using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Hackaton.Api.Domain.Models
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
        [Column("Senha")]
        public string Senha { get; set; }
        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }
        public virtual IEnumerable<Agenda> Agendas { get; set; } = new List<Agenda>();

        public Paciente(string nome, string email, string senha, DateTime dataNascimento)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataNascimento = dataNascimento;
        }

        public void AdicionarAgenda(IEnumerable<Agenda> Agendas) => this.Agendas = Agendas;  
    }
}
