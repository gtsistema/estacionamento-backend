using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    ValorPrevistoDespesa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorPrevistoReceita = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotalPagoDespesas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotalPagoReceitas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataLimite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NaoPossuiDataLimite = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryPassword = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDespesa = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentagemPaga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrcamentoId = table.Column<long>(type: "bigint", nullable: false),
                    MesReferente = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    TipoReceita = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorRecebido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoRestante = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentagemRecebido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrcamentoId = table.Column<long>(type: "bigint", nullable: false),
                    MesReferente = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    CompraParcelado = table.Column<bool>(type: "bit", nullable: false),
                    QuantidadeParcelado = table.Column<int>(type: "int", nullable: false),
                    Pagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cartao = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DespesaId = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DespesaId = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceitaId = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Orcamento",
                columns: new[] { "Id", "Ano", "DataAtualizacao", "DataCriacao", "DataLimite", "Descricao", "NaoPossuiDataLimite", "ValorPrevistoDespesa", "ValorPrevistoReceita", "ValorTotalPagoDespesas", "ValorTotalPagoReceitas" },
                values: new object[,]
                {
                    { 1L, 2025, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(5723), new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despesas Mensais - 2025", false, null, null, 0m, 0m },
                    { 2L, 2025, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(5731), new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volta para Cuiabá", false, null, null, 0m, 0m },
                    { 3L, 0, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(5732), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compra da Moto", true, null, null, 0m, 0m },
                    { 4L, 0, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(5733), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fundo de Reserva", true, null, null, 0m, 0m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EmpresaId", "FullName", "ImagemUrl", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TemporaryPassword", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9552c4b8-e60e-42a6-8f97-96b8f8edd555"), 0, "1a503903-b30e-4c13-9e8a-2ec6038c475c", "jeancpcorrea@gmail.com", true, null, null, null, false, false, null, "JEANCPCORREA@GMAIL.COM", "jeancpcorrea@gmail.com", "@Admin123456", "65981324241", true, "LDPU32DFTNBNBJ5JQ2R7VXEHNCGE6UER", false, false, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("078a8c6e-a76d-46e8-ba75-3e9476c0a35c"), new Guid("9552c4b8-e60e-42a6-8f97-96b8f8edd555") });

            migrationBuilder.InsertData(
                table: "Despesa",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Descricao", "MesReferente", "OrcamentoId", "PorcentagemPaga", "SaldoRestante", "TipoDespesa", "ValorPago", "ValorTotal" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1514), "Cartão de Credito", 0, 1L, 0m, 0m, 1, 0m, 0m },
                    { 2L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1546), "Aluguel", 5, 1L, 0m, 0m, 4, 0m, 1050.00m },
                    { 3L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1567), "Combustível", 5, 1L, 0m, 0m, 14, 0m, 0m },
                    { 4L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1585), "Remédios", 5, 1L, 0m, 0m, 9, 0m, 0m },
                    { 5L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1601), "Fatura de Luz", 5, 1L, 0m, 0m, 15, 0m, 0m },
                    { 7L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1622), "Alimentação", 5, 1L, 0m, 0m, 13, 0m, 0m },
                    { 8L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(1637), "Consorcio", 5, 1L, 0m, 0m, 5, 0m, 0m }
                });

            migrationBuilder.InsertData(
                table: "Receita",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Descricao", "MesReferente", "OrcamentoId", "PorcentagemRecebido", "SaldoRestante", "TipoReceita", "ValorRecebido", "ValorTotal" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(4101), "Salário Jean", 0, 2L, 0m, 0m, 1, 0m, 0m },
                    { 2L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(4122), "Flex Benner", 0, 2L, 0m, 0m, 6, 0m, 0m },
                    { 3L, null, new DateTime(2025, 4, 29, 16, 21, 10, 347, DateTimeKind.Local).AddTicks(4139), "Bpc Lorenzo", 0, 2L, 0m, 0m, 2, 0m, 0m }
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Orcamento");
        }
    }
}
