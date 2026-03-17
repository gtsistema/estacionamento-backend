using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class NovoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gts");

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false),
                    NomeRazaoSocial = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    NomeFantasia = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Documento = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                schema: "gts",
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
                name: "VeiculoMarca",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoMarca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estacionamento",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    CapacidadeVeiculo = table.Column<int>(type: "int", nullable: true),
                    TamanhoTerreno = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    ResposanvelLegal = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    ResponsavelCpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    PossuiSeguranca = table.Column<bool>(type: "bit", nullable: true),
                    PossuiBanheiro = table.Column<bool>(type: "bit", nullable: true),
                    TipoCobranca = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    CobrancaPorcentagem = table.Column<byte>(type: "tinyint", nullable: true),
                    CobrancaValor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estacionamento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "gts",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motorista",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    CNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ValidadeCNH = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motorista_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "gts",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaContato",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    TipoContato = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaContato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaContato_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "gts",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaEndereco",
                schema: "gts",
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
                        principalSchema: "gts",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaPapel",
                schema: "gts",
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
                        principalSchema: "gts",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportadora",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportadora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportadora_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "gts",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoModelo",
                schema: "gts",
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
                    table.PrimaryKey("PK_VeiculoModelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoModelo_VeiculoMarca_VeiculoMarcaId",
                        column: x => x.VeiculoMarcaId,
                        principalSchema: "gts",
                        principalTable: "VeiculoMarca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContaBancaria",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstacionamentoId = table.Column<int>(type: "int", nullable: false),
                    Titular = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Agencia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AgenciaDigito = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Conta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContaDigito = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    TipoConta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ChavePix = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaBancaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaBancaria_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalSchema: "gts",
                        principalTable: "Estacionamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstacionamentoFoto",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstacionamentoId = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    TamanhoBytes = table.Column<long>(type: "bigint", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacionamentoFoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstacionamentoFoto_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalSchema: "gts",
                        principalTable: "Estacionamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                schema: "gts",
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
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_VeiculoModelo_VeiculoModeloId",
                        column: x => x.VeiculoModeloId,
                        principalSchema: "gts",
                        principalTable: "VeiculoModelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotoristaVeiculo",
                schema: "gts",
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
                        principalSchema: "gts",
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotoristaVeiculo_Veiculo",
                        column: x => x.VeiculoId,
                        principalSchema: "gts",
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VagaVeiculo",
                schema: "gts",
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
                        principalSchema: "gts",
                        principalTable: "Vaga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VagaVeiculo_Veiculo",
                        column: x => x.VeiculoId,
                        principalSchema: "gts",
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoDetalhe",
                schema: "gts",
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
                    table.PrimaryKey("PK_VeiculoDetalhe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoDetalhe_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalSchema: "gts",
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoPlaca",
                schema: "gts",
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
                    table.PrimaryKey("PK_VeiculoPlaca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoPlaca_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalSchema: "gts",
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancaria_EstacionamentoId",
                schema: "gts",
                table: "ContaBancaria",
                column: "EstacionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamento_PessoaId",
                schema: "gts",
                table: "Estacionamento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstacionamentoFoto_EstacionamentoId",
                schema: "gts",
                table: "EstacionamentoFoto",
                column: "EstacionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_PessoaId",
                schema: "gts",
                table: "Motorista",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaVeiculo_MotoristaId_VeiculoId",
                schema: "gts",
                table: "MotoristaVeiculo",
                columns: new[] { "MotoristaId", "VeiculoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaVeiculo_VeiculoId",
                schema: "gts",
                table: "MotoristaVeiculo",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Documento",
                schema: "gts",
                table: "Pessoa",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaContato_PessoaId",
                schema: "gts",
                table: "PessoaContato",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaContato_PessoaId_Numero",
                schema: "gts",
                table: "PessoaContato",
                columns: new[] { "PessoaId", "Numero" });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEndereco_PessoaId",
                schema: "gts",
                table: "PessoaEndereco",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaPapel_PessoaId",
                schema: "gts",
                table: "PessoaPapel",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportadora_PessoaId",
                schema: "gts",
                table: "Transportadora",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_Ativo",
                schema: "gts",
                table: "Vaga",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_VagaVeiculo_DataEntrada",
                schema: "gts",
                table: "VagaVeiculo",
                column: "DataEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_VagaVeiculo_VagaId",
                schema: "gts",
                table: "VagaVeiculo",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaVeiculo_VeiculoId",
                schema: "gts",
                table: "VagaVeiculo",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_VeiculoModeloId",
                schema: "gts",
                table: "Veiculo",
                column: "VeiculoModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoDetalhe_VeiculoId",
                schema: "gts",
                table: "VeiculoDetalhe",
                column: "VeiculoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoMarca_Id",
                schema: "gts",
                table: "VeiculoMarca",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoModelo_VeiculoMarcaId",
                schema: "gts",
                table: "VeiculoModelo",
                column: "VeiculoMarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoPlaca_VeiculoId",
                schema: "gts",
                table: "VeiculoPlaca",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaBancaria",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "EstacionamentoFoto",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "MotoristaVeiculo",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "PessoaContato",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "PessoaEndereco",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "PessoaPapel",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "Transportadora",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "VagaVeiculo",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "VeiculoDetalhe",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "VeiculoPlaca",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "Estacionamento",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "Motorista",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "Vaga",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "Veiculo",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "VeiculoModelo",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "VeiculoMarca",
                schema: "gts");
        }
    }
}
