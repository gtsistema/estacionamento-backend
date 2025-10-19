using System.ComponentModel;

namespace Estac.Domain.Models.Enuns
{
    public enum TipoDespesa
    {
        [Description("Cartão de Credito")]
        CartaoCredito = 1,

        [Description("Fundo de Reserva")]
        FundoDeReserva = 2,

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

        [Description("Fatura de Luz")]
        Luz = 15,

        [Description("Outros")]
        Outros = 16,

        [Description("Compras Paraguai")]
        ComprasParaguai = 17,

        [Description("Faculdade Carol")]
        FaculdadeCarol = 18,

        [Description("Impostos")]
        Impostos = 19,

        [Description("Manutenção Moto")]
        ManutencaoMoto = 20,

        [Description("Lazer")]
        Lazer = 21,

        [Description("Emprestismo")]
        Emprestismo = 22,

        [Description("Financimento Carro")]
        FinancimentoCarro = 23,

        [Description("Seguros")]
        Seguros = 24,
    }
}
