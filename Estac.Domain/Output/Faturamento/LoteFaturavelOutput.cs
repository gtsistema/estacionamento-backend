namespace Estac.Domain.Output.Faturamento
{
    /// <summary>
    /// Lote lido por keyset. O cursor evita OFFSET e COUNT, mantendo custo constante
    /// mesmo com novos movimentos sendo gravados durante o processamento.
    /// </summary>
    public class LoteFaturavelOutput
    {
        public IList<EntradaSaidaFaturavelOutput> Itens { get; set; } = new List<EntradaSaidaFaturavelOutput>();

        /// <summary>Repassar em UltimoId na próxima chamada. Null quando não há mais páginas.</summary>
        public int? ProximoCursor { get; set; }

        public bool PossuiMais { get; set; }
    }
}
