using System.ComponentModel;

namespace Gp.Domain.Models.Enuns
{
    public enum TipoDespesa
    {
        [Description("Fatura de Luz")]
        Luz = 1,

        [Description("Parto do Bebê")]
        Parto = 2,

        [Description("Fatura de Água")]
        Agua = 3,

        [Description("Aluguel")]
        Aluguel = 4,

        [Description("Consorcio")]
        Consorcio = 5,

        [Description("Plano de Saúde")]
        PlanoSaude = 6,

        [Description("Manutenção do Carro")]
        ManutencaoCarro = 7,

        [Description("Manutenção da Casa")]
        ManutencaoCasa = 8,

        [Description("Remédios")]
        Remedios = 9,

        [Description("Gastos em Viagem")]
        Viagem = 10,

        [Description("Investimentos")]
        Investimentos = 11,

        [Description("Compra de Carro")]
        ComprarCarro = 12,

        [Description("Compra de Casa")]
        ComprarCasa = 12,

        [Description("Alimentação")]
        Alimentacao = 13,

        [Description("Combustível")]
        Combustivel = 14,

        [Description("Cartão de Credito")]
        CartaoCredito = 15,

        [Description("Outros")]
        Outros = 16,

        [Description("Compras Paraguai")]
        ComprasParaguai = 17
    }
}
