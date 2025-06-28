using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naitv1.Migrations
{
    /// <inheritdoc />
    public partial class correccionAppdbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades",
                column: "AnfitrionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_AnfitrionId",
                table: "Actividades",
                column: "AnfitrionId",
                unique: true);
        }
    }
}
