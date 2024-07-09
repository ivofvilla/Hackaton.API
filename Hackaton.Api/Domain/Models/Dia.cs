using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hackaton.Api.Domain.Models
{
    public enum Dia
    {
        [Display(Name = "Segunda-Feira")]
        Segunda = 2,
        [Display(Name = "Terça-Feira")]
        Terca = 3,
        [Display(Name = "Quarta-Feira")]
        Quarta = 4,
        [Display(Name = "Quinta-Feira")]
        Quinta = 5,
        [Display(Name = "Sexta-Feira")]
        Sexta = 6,

    }
}
