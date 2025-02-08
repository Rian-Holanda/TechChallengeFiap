using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_FiapTechChallenge.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CampoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataConsulta",
                table: "tb_Consulta",
                newName: "DataMarcacaoConsulta");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataConsulta",
                table: "tb_HistoricoConsulta",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConsulta",
                table: "tb_HistoricoConsulta");

            migrationBuilder.RenameColumn(
                name: "DataMarcacaoConsulta",
                table: "tb_Consulta",
                newName: "DataConsulta");
        }
    }
}
