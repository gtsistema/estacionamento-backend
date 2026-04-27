using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Estac.Service.Seed.Identity
{
    public class SeedMenu
    {
        public async Task ExecuteAsync(IServiceProvider services, IdentityContext context)
        {
            await Gravar(services, context);
        }

        private async Task Gravar(IServiceProvider services, IdentityContext context)
        {
            var service = services.GetRequiredService<IMenuServices>();

            var menusBase = new List<Func<int, MenuCreateInput>>
            {
                MenuDashboard,
                MenuMovimento,
                MenuRelatorio,
                MenuFinaceiro,
                MenuGerenciamento,
                MenuCadastro
            };

            var menusCadastrados = await context.Module.ToListAsync();

            var nomesExistentes = menusCadastrados
                .Select(x => x.Descricao.ToLower())
                .ToHashSet();

            var ordemAtual = await context.Module
                .Where(x => x.Ativo)
                .MaxAsync(x => (int?)x.Ordem) ?? 0;

            var menusParaGravar = menusBase
                .Where(menuFunc =>
                {
                    var nome = menuFunc(0).Nome.ToLower();
                    return !nomesExistentes.Contains(nome);
                })
                .Select(menuFunc => menuFunc(++ordemAtual))
                .ToList();

            foreach (var menu in menusParaGravar)
            {
                await service.Gravar(menu);
            }
        }

        private static MenuCreateInput MenuDashboard(int ordem)
        {
            return new MenuCreateInput
            {
                Nome = "Dashboard",
                Ordem = ordem,
                Ativo = true,
                Rota = "app/dashboard",
            };
        }

        private static MenuCreateInput MenuMovimento(int ordem)
        {
            return new MenuCreateInput
            {
                Nome = "Movimento",
                Ordem = ordem,
                Ativo = true,
                Rota = "app/movimento",
            };
        }

        private static MenuCreateInput MenuRelatorio(int ordem)
        {
            return new MenuCreateInput
            {
                Nome = "Relatorio",
                Ordem = ordem,
                Ativo = true,
                Rota = "app/relatorio",
            };
        }

        private static MenuCreateInput MenuFinaceiro(int ordem)
        {
            return new MenuCreateInput
            {
                Nome = "Financeiro",
                Ordem = ordem,
                Ativo = true,
                Rota = "app/financeiro",
            };
        }

        private static MenuCreateInput MenuGerenciamento(int ordem)
        {
            return new MenuCreateInput
            {
                Nome = "Gerenciamento",
                Ordem = ordem,
                Ativo = true,
                Rota = "app/gerenciamento",
                SubMenus = new List<SubMenuCreateInput>
                {
                    new SubMenuCreateInput
                    {
                        Nome = "perfil",
                        Ordem = 1,
                        Ativo = true,
                        Rota = "app/gerenciamento/perfil",
                        Permissions = new List<PermissionInput>
                        {
                            new PermissionInput
                            {
                                Ordem = 1,
                                Descricao = "Visualizar"
                            },
                            new PermissionInput
                            {
                                Ordem = 2,
                                Descricao = "Gravar"
                            },
                            new PermissionInput
                            {
                                Ordem = 3,
                                Descricao = "Alterar"
                            },
                            new PermissionInput
                            {
                                Ordem = 4,
                                Descricao = "Excluir"
                            }
                        }
                    },

                    new SubMenuCreateInput
                    {
                        Nome = "Menu",
                        Ordem = 3,
                        Ativo = true,
                        Rota = "app/gerenciamento/menu",
                        Permissions = new List<PermissionInput>
                        {
                            new PermissionInput
                            {
                                Ordem = 1,
                                Descricao = "Visualizar"
                            },
                            new PermissionInput
                            {
                                Ordem = 2,
                                Descricao = "Gravar"
                            },
                            new PermissionInput
                            {
                                Ordem = 3,
                                Descricao = "Alterar"
                            },
                            new PermissionInput
                            {
                                Ordem = 4,
                                Descricao = "Excluir"
                            }
                        }
                    },
                    new SubMenuCreateInput
                    {
                        Nome = "Usuario",
                        Ordem = 3,
                        Ativo = true,
                        Rota = "app/gerenciamento/usuario",
                        Permissions = new List<PermissionInput>
                        {
                            new PermissionInput
                            {
                                Ordem = 1,
                                Descricao = "Visualizar"
                            },
                            new PermissionInput
                            {
                                Ordem = 2,
                                Descricao = "Gravar"
                            },
                            new PermissionInput
                            {
                                Ordem = 3,
                                Descricao = "Alterar"
                            },
                            new PermissionInput
                            {
                                Ordem = 4,
                                Descricao = "Excluir"
                            }
                        }
                    }
                }
            };
        }

        private static MenuCreateInput MenuCadastro(int ordem)
        {
            return new MenuCreateInput
            {
                Nome = "Cadastro",
                Ordem = ordem,
                Ativo = true,
                Rota = "/cadastro",
                SubMenus = new List<SubMenuCreateInput>
                {
                    new SubMenuCreateInput
                    {
                        Nome = "Estacionamento",
                        Ordem = 1,
                        Ativo = true,
                        Rota = "app/cadastro/estacionamento",
                        Permissions = new List<PermissionInput>
                        {
                            new PermissionInput
                            {
                                Ordem = 1,
                                Descricao = "Visualizar"
                            },
                            new PermissionInput
                            {
                                Ordem = 2,
                                Descricao = "Gravar"
                            },
                            new PermissionInput
                            {
                                Ordem = 3,
                                Descricao = "Alterar"
                            },
                            new PermissionInput
                            {
                                Ordem = 4,
                                Descricao = "Excluir"
                            }
                        }
                    },

                    new SubMenuCreateInput
                    {
                        Nome = "Motorista",
                        Ordem = 3,
                        Ativo = true,
                        Rota = "app/cadastro/motorista",
                        Permissions = new List<PermissionInput>
                        {
                            new PermissionInput
                            {
                                Ordem = 1,
                                Descricao = "Visualizar"
                            },
                            new PermissionInput
                            {
                                Ordem = 2,
                                Descricao = "Gravar"
                            },
                            new PermissionInput
                            {
                                Ordem = 3,
                                Descricao = "Alterar"
                            },
                            new PermissionInput
                            {
                                Ordem = 4,
                                Descricao = "Excluir"
                            }
                        }
                    },

                    new SubMenuCreateInput
                    {
                        Nome = "Transportadora",
                        Ordem = 2,
                        Ativo = true,
                        Rota = "app/cadastro/transportadora",
                        Permissions = new List<PermissionInput>
                        {
                            new PermissionInput
                            {
                                Ordem = 1,
                                Descricao = "Visualizar"
                            },
                            new PermissionInput
                            {
                                Ordem = 2,
                                Descricao = "Gravar"
                            },
                            new PermissionInput
                            {
                                Ordem = 3,
                                Descricao = "Alterar"
                            },
                            new PermissionInput
                            {
                                Ordem = 4,
                                Descricao = "Excluir"
                            }
                        }
                    },
                }
            };
        } 
    }
}