using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naitv1.Migrations
{
    /// <inheritdoc />
    public partial class agregoCampo_y_set_appdbcontext_actividad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades");

            migrationBuilder.AddColumn<bool>(
                name: "Anfitrion",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "idActividadAnfitrion",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades",
                column: "AnfitrionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "Anfitrion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "idActividadAnfitrion",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades",
                column: "AnfitrionId");
        }
    }
}
