using System.ComponentModel.DataAnnotations;

namespace Estac.Domain.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        [Required]
        public string PlacaCavalo { get; set; }
        public string PlacaCarreta1 { get; set; }
        public string PlacaCarreta2 { get; set; }
        public string PlacaCarreta3 { get; set; }
    }
}
