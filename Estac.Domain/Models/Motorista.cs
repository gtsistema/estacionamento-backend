using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}