using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackaton.Api.Domain.Models
{

    [Table(name: "DiasTrabalho", Schema = "dbo")]
    public class DiasTrabalho
    {

        [Key, Column("Id")]
        public int Id { get; set; }
        [Key, Column("IdMedico")]
        public int IdMedico { get; set; }
        [Key, Column("Dia")]
        public int Dia {  get; set; }
    }
}
