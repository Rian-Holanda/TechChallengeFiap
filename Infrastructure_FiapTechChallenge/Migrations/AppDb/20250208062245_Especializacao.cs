using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_FiapTechChallenge.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class Especializacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Especializacao",
                table: "tb_Medico",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especializacao",
                table: "tb_Medico");
        }
    }
}
