namespace Estac.Domain.Models.Enuns
{
    public enum StatusFatura : byte
    {
        AguardandoEnvio = 1,
        EmAberto = 2,
        Parcial = 3,
        Pago = 4,
        Vencido = 5,
        Cancelada = 6
    }
}
