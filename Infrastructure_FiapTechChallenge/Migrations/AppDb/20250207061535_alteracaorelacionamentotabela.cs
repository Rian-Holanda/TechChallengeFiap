using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_FiapTechChallenge.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class alteracaorelacionamentotabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_HistoricoConsulta_IdHorarioDia",
                table: "tb_HistoricoConsulta");

            migrationBuilder.CreateIndex(
                name: "IX_tb_HistoricoConsulta_IdHorarioDia",
                table: "tb_HistoricoConsulta",
                column: "IdHorarioDia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tb_HistoricoConsulta_IdHorarioDia",
                table: "tb_HistoricoConsulta");

            migrationBuilder.CreateIndex(
                name: "IX_tb_HistoricoConsulta_IdHorarioDia",
                table: "tb_HistoricoConsulta",
                column: "IdHorarioDia",
                unique: true);
        }
    }
}
