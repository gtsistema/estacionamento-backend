
namespace Estac.Domain.Input.Auth
{
    public class ModuloInput
    {
        public string Modulo { get; set; }

        public List<SubModuloInput> SubModulos { get; set; }
    }
    public class SubModuloInput
    {
        public string Nome { get; set; }

        public CrudPermissaoInput Permissoes { get; set; }
    }

    public class CrudPermissaoInput
    {
        public bool Visualizar { get; set; }
        public bool Criar { get; set; }
        public bool Alterar { get; set; }
        public bool Excluir { get; set; }
    }
}
