using System.ComponentModel.DataAnnotations;

namespace Gp.Domain.Models.Enuns
{
    public enum TipoDespesa
    {
        [Display(Name = "Fatura de Luz")]
        Luz = 1,

        [Display(Name = "Fatura de Gás")]
        Gás = 2,

        [Display(Name = "Fatura de Água")]
        Agua = 3,

        [Display(Name = "Aluguel")]
        Aluguel = 4,

        [Display(Name = "Condominio")]
        Condominio = 5,

        [Display(Name = "Plano de Saúde")]
        PlanoSaude = 6,

        [Display(Name = "Manutenção do Carro")]
        ManutencaoCarro = 7,

        [Display(Name = "Manutenção da Casa")]
        ManutencaoCasa = 8,

        [Display(Name = "Remédios")]
        Remedios = 9,

        [Display(Name = "Gastos em Viagem")]
        Viagem = 10,

        [Display(Name = "Investimentos")]
        Investimentos = 11,

        [Display(Name = "Compra de Carro")]
        ComprarCarro = 12,

        [Display(Name = "Compra de Casa")]
        ComprarCasa = 12,

        [Display(Name = "Alimentação")]
        Alimentacao = 13,

        [Display(Name = "Combustível")]
        Combustivel = 14,

        [Display(Name = "Cartao de Credito")]
        CartaoCredito = 15,

        [Display(Name = "Outros")]
        Outros = 16,

        [Display(Name = "Compras Paraguai")]
        ComprasParaguai = 17,
    }
}
