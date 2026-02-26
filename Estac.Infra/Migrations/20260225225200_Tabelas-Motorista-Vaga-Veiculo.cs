using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TabelasMotoristaVagaVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespesaLancamento");

            migrationBuilder.DropTable(
                name: "DespesaPagamento");

            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "ReceitaLancamento");

            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Orcamento");

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false),
                    NomeRazaoSocial = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    NomeFantasia = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Documento = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VEICULOMARCA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULOMARCA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    CNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ValidadeCNH = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorista_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    TipoEndereco = table.Column<int>(type: "int", nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaEndereco_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaPapel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    TipoPapel = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaPapel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaPapel_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaTelefone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    TipoTelefone = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaTelefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaTelefone_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VEICULOMODELO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoMarcaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULOMODELO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VEICULOMODELO_VEICULOMARCA_VeiculoMarcaId",
                        column: x => x.VeiculoMarcaId,
                        principalTable: "VEICULOMARCA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VEICULO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Cor = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    VeiculoModeloId = table.Column<int>(type: "int", nullable: true),
                    VeiculoDetalheId = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VEICULO_VEICULOMODELO_VeiculoModeloId",
                        column: x => x.VeiculoModeloId,
                        principalTable: "VEICULOMODELO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotoristaVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotoristaId = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoristaVeiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotoristaVeiculo_Motorista",
                        column: x => x.MotoristaId,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotoristaVeiculo_Veiculo",
                        column: x => x.VeiculoId,
                        principalTable: "VEICULO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VagaVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VagaId = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaVeiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaVeiculo_Vaga_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vaga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VagaVeiculo_Veiculo",
                        column: x => x.VeiculoId,
                        principalTable: "VEICULO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VEICULODETALHE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    Uf = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    NomeProprietario = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CpfCnpjProprietario = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: true),
                    KmAtual = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    KmRastreador = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CapacidadeCombustivel = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    CapacidadeArla = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    MEDIAMINIMA = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MediaMaxima = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    VeiculoTerceiro = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Observacoes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULODETALHE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VEICULODETALHE_VEICULO_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "VEICULO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VEICULOPLACA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULOPLACA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VEICULOPLACA_VEICULO_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "VEICULO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_PessoaId",
                table: "Motorista",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaVeiculo_MotoristaId_VeiculoId",
                table: "MotoristaVeiculo",
                columns: new[] { "MotoristaId", "VeiculoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaVeiculo_VeiculoId",
                table: "MotoristaVeiculo",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Documento",
                table: "Pessoa",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEndereco_PessoaId",
                table: "PessoaEndereco",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaPapel_PessoaId",
                table: "PessoaPapel",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaPapel_PessoaId_TipoPapel",
                table: "PessoaPapel",
                columns: new[] { "PessoaId", "TipoPapel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaTelefone_PessoaId",
                table: "PessoaTelefone",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaTelefone_PessoaId_Numero",
                table: "PessoaTelefone",
                columns: new[] { "PessoaId", "Numero" });

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_Ativo",
                table: "Vaga",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_VagaVeiculo_DataEntrada",
                table: "VagaVeiculo",
                column: "DataEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_VagaVeiculo_VagaId",
                table: "VagaVeiculo",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaVeiculo_VeiculoId",
                table: "VagaVeiculo",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_VEICULO_VeiculoModeloId",
                table: "VEICULO",
                column: "VeiculoModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_VEICULODETALHE_VeiculoId",
                table: "VEICULODETALHE",
                column: "VeiculoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VEICULOMARCA_Id",
                table: "VEICULOMARCA",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VEICULOMODELO_VeiculoMarcaId",
                table: "VEICULOMODELO",
                column: "VeiculoMarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_VEICULOPLACA_VeiculoId",
                table: "VEICULOPLACA",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotoristaVeiculo");

            migrationBuilder.DropTable(
                name: "PessoaEndereco");

            migrationBuilder.DropTable(
                name: "PessoaPapel");

            migrationBuilder.DropTable(
                name: "PessoaTelefone");

            migrationBuilder.DropTable(
                name: "VagaVeiculo");

            migrationBuilder.DropTable(
                name: "VEICULODETALHE");

            migrationBuilder.DropTable(
                name: "VEICULOPLACA");

            migrationBuilder.DropTable(
                name: "Motorista");

            migrationBuilder.DropTable(
                name: "Vaga");

            migrationBuilder.DropTable(
                name: "VEICULO");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "VEICULOMODELO");

            migrationBuilder.DropTable(
                name: "VEICULOMARCA");

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataLimite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaoPossuiDataLimite = table.Column<bool>(type: "bit", nullable: false),
                    ValorPrevistoDespesa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorPrevistoReceita = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalPagoDespesas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotalPagoReceitas = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrcamentoId = table.Column<long>(type: "bigint", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MesReferente = table.Column<int>(type: "int", nullable: false),
                    PorcentagemPaga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoDespesa = table.Column<int>(type: "int", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesa_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrcamentoId = table.Column<long>(type: "bigint", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MesReferente = table.Column<int>(type: "int", nullable: false),
                    PorcentagemRecebido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoReceita = table.Column<int>(type: "int", nullable: false),
                    ValorRecebido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receita_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaLancamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DespesaId = table.Column<long>(type: "bigint", nullable: false),
                    Cartao = table.Column<int>(type: "int", nullable: true),
                    CompraParcelado = table.Column<bool>(type: "bit", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeParcelado = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaLancamento_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesaPagamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DespesaId = table.Column<long>(type: "bigint", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaPagamento_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaLancamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceitaId = table.Column<long>(type: "bigint", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaLancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceitaLancamento_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_OrcamentoId",
                table: "Despesa",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaLancamento_DespesaId",
                table: "DespesaLancamento",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaPagamento_DespesaId",
                table: "DespesaPagamento",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_OrcamentoId",
                table: "Receita",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaLancamento_ReceitaId",
                table: "ReceitaLancamento",
                column: "ReceitaId");
        }
    }
}
