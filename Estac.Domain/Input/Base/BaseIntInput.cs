using System;

namespace Estac.Domain.Input.Base
{
    public class BaseIntInput
    {
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}