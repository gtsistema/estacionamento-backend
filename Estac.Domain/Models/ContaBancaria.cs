using Estac.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Domain.Models
{
    public class ContaBancaria : BaseInt
    {
        public int EstacionamentoId { get; set; }
        public string Titular { get; private set; }
        public string CpfCnpj { get; private set; }
        public string Banco { get; private set; }
        public string Agencia { get; private set; }
        public string AgenciaDigito { get; private set; }
        public string Conta { get; private set; }
        public string ContaDigito { get; private set; }
        public string TipoConta { get; private set; }
        public bool Ativa { get; private set; }
        public string ChavePix {  get; private set; }
        public Estacionamento Estacionamento { get; set; }
    }
}