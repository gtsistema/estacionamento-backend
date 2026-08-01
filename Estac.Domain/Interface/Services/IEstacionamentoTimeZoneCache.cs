namespace Estac.Domain.Interface.Services
{
    /// <summary>
    /// Cache em memória do TimeZoneId por estacionamento (preenchido no login e atualizado no CRUD).
    /// Evita ir ao banco a cada conversão de data.
    /// </summary>
    public interface IEstacionamentoTimeZoneCache
    {
        void Set(int estacionamentoId, string timeZoneId);
        bool TryGet(int estacionamentoId, out string timeZoneId);
        void Remove(int estacionamentoId);
    }
}
