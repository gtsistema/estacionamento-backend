using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estac.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EntradaSaida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntradaSaida",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotoristaId = table.Column<int>(type: "int", nullable: false),
                    TransportadoraId = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    DataHoraEntrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraSaida = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioRegistroEntradaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioRegistroEntradaNome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    UsuarioFinalizacaoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioFinalizacaoNome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataHoraUltimaEntradaPatio = table.Column<DateTime>(type: "datetime", nullable: true),
                    TempoPermanenciaMinutos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TempoTotalSuspensaoMinutos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PermanenciaSuspensa = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DataHoraFinalizacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaSaida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradaSaida_Motorista_MotoristaId",
                        column: x => x.MotoristaId,
                        principalSchema: "gts",
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradaSaida_Transportadora_TransportadoraId",
                        column: x => x.TransportadoraId,
                        principalSchema: "gts",
                        principalTable: "Transportadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradaSaida_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalSchema: "gts",
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradaSaidaSuspensao",
                schema: "gts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntradaSaidaId = table.Column<int>(type: "int", nullable: false),
                    DataHoraInicioSuspensao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraFimSuspensao = table.Column<DateTime>(type: "datetime", nullable: true),
                    TempoSuspensaoMinutos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UsuarioSuspensaoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioSuspensaoNome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaSaidaSuspensao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradaSaidaSuspensao_EntradaSaida_EntradaSaidaId",
                        column: x => x.EntradaSaidaId,
                        principalSchema: "gts",
                        principalTable: "EntradaSaida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntradaSaida_MotoristaId",
                schema: "gts",
                table: "EntradaSaida",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaSaida_TransportadoraId",
                schema: "gts",
                table: "EntradaSaida",
                column: "TransportadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaSaida_VeiculoId",
                schema: "gts",
                table: "EntradaSaida",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaSaidaSuspensao_EntradaSaidaId",
                schema: "gts",
                table: "EntradaSaidaSuspensao",
                column: "EntradaSaidaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntradaSaidaSuspensao",
                schema: "gts");

            migrationBuilder.DropTable(
                name: "EntradaSaida",
                schema: "gts");
        }
    }
}
