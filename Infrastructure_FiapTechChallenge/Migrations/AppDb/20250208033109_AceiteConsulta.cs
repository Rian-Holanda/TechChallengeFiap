using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_FiapTechChallenge.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AceiteConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMedico",
                table: "tb_HorarioDia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ConsultaAprovada",
                table: "tb_Consulta",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMedico",
                table: "tb_HorarioDia");

            migrationBuilder.DropColumn(
                name: "ConsultaAprovada",
                table: "tb_Consulta");
        }
    }
}
