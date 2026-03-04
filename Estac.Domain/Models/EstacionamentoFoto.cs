using Estac.Domain.Models.Base;
using System;

namespace Estac.Domain.Models
{
    public class EstacionamentoFoto : BaseInt
    {
        public int EstacionamentoId { get; set; }
        public byte[] Foto { get; set; }
        public string ContentType { get; set; }
        public bool Principal { get; set; }
        public long TamanhoBytes { get; set; }
        public int? Ordem { get; set; }
        public Estacionamento Estacionamento { get; set; }
    }
}