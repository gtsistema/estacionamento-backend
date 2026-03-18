using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estac.Domain.Models.Auth
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? PessoaId { get; set; }
        public int? TransportadoraId { get; set; }
        public int? EstacionadoId { get; set; }
        public string FullName { get; set; }
        public bool TemporaryPassword { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public string ImagemUrl { get; set; }
        [NotMapped]
        public Transportadora Transportadora { get; set; }
        [NotMapped]
        public Estacionamento Estacionamento { get; set; }
        [NotMapped]
        public Pessoa Pessoa { get; set; }
    }
}