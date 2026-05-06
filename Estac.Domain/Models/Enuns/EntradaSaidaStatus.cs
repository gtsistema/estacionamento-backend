using System.ComponentModel;

namespace Estac.Domain.Models.Enuns
{
    public enum EntradaSaidaStatus : byte
    {
        [Description("Em Aberto")]
        EmAberto = 0,

        [Description("Finalizado")]
        Finalizado = 1,

        [Description("Suspenso")]
        Suspenso = 2,

        [Description("Agendado")]
        Agendado = 3,

        [Description("Cancelado")]
        Cancelado = 4
    }
}
