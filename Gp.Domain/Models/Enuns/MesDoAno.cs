using System.ComponentModel.DataAnnotations;

namespace Gp.Domain.Models.Enuns
{
    public enum MesDoAno
    {
        [Display(Name = "Janeiro")]
        Janeiro = 1,
        [Display(Name = "Fevereiro")]
        Fevereiro,
        [Display(Name = "Março")]
        Março,
        [Display(Name = "Abril")]
        Abril,
        [Display(Name = "Maio")]
        Maio,
        [Display(Name = "Junho")]
        Junho,
        [Display(Name = "Julho")]
        Julho,
        [Display(Name = "Agosto")]
        Agosto,
        [Display(Name = "Setembro")]
        Setembro,
        [Display(Name = "Outubro")]
        Outubro,
        [Display(Name = "Novembro")]
        Novembro,
        [Display(Name = "Dezembro")]
        Dezembro
    }
}
