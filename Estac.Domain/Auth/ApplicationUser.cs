using Estac.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Estac.Domain.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int? PessoaId { get; set; }
        public int? TransportadoraId { get; set; }
        public int? EstacionadoId { get; set; }
        public string FullName { get; set; }
        public bool TemporaryPassword { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public string ImagemUrl { get; set; }
        public Transportadora Transportadora { get; set; }
        public Estacionamento Estacionamento { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}