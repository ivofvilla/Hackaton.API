using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackaton.Api.Domain.Models
{
    [Table(name: "Login", Schema = "dbo")]
    public class Login
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Senha")]
        public string Senha { get; set; }
        [Column("Ativo")]
        public bool Ativo { get; set; }
        [Column("Medico")]
        public bool Medico { get; set; }
        [Column("DataCadastro")]
        public DateTime DataCadastro { get; set; }
        [Column("DataUltimoLogin")]
        public DateTime DataUltimoLogin { get; set; }
    }
}
