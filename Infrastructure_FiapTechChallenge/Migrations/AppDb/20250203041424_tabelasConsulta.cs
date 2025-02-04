using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_FiapTechChallenge.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class tabelasConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Consulta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Consulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Consulta_tb_Medico_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "tb_Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_Consulta_tb_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "tb_Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Dia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Dia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Horario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Horario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_HorarioDia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHorarioInicio = table.Column<int>(type: "int", nullable: false),
                    IdHorarioFim = table.Column<int>(type: "int", nullable: false),
                    IdDia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_HorarioDia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_HorarioDia_tb_Dia_IdDia",
                        column: x => x.IdDia,
                        principalTable: "tb_Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_HistoricoConsulta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConsuta = table.Column<int>(type: "int", nullable: false),
                    IdHorarioDia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_HistoricoConsulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_HistoricoConsulta_tb_Consulta_IdConsuta",
                        column: x => x.IdConsuta,
                        principalTable: "tb_Consulta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_HistoricoConsulta_tb_HorarioDia_IdHorarioDia",
                        column: x => x.IdHorarioDia,
                        principalTable: "tb_HorarioDia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Consulta_IdMedico",
                table: "tb_Consulta",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Consulta_IdPaciente",
                table: "tb_Consulta",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_tb_HistoricoConsulta_IdConsuta",
                table: "tb_HistoricoConsulta",
                column: "IdConsuta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_HistoricoConsulta_IdHorarioDia",
                table: "tb_HistoricoConsulta",
                column: "IdHorarioDia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_HorarioDia_IdDia",
                table: "tb_HorarioDia",
                column: "IdDia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_HistoricoConsulta");

            migrationBuilder.DropTable(
                name: "tb_Horario");

            migrationBuilder.DropTable(
                name: "tb_Consulta");

            migrationBuilder.DropTable(
                name: "tb_HorarioDia");

            migrationBuilder.DropTable(
                name: "tb_Dia");
        }
    }
}
