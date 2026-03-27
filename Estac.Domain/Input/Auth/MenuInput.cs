
namespace Estac.Domain.Input.Auth
{
    public class MenuInput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public string Rota { get; set; }
        public List<SubMenuCreateInput> SubMenus { get; set; }
    }
}
