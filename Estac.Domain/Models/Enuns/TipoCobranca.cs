using System;

namespace Estac.Domain.Models.Enuns
{
    public enum TipoCobranca : byte
    {
        Gratuito = 0,
        Porcentagem = 1,
        Mensal = 2,
        ValorFechado = 3
    }
}
