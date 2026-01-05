using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equinoterapia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mulevaluacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_evaluacion_paciente_id",
                table: "evaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_evaluacion_paciente_id",
                table: "evaluacion",
                column: "paciente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_evaluacion_paciente_id",
                table: "evaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_evaluacion_paciente_id",
                table: "evaluacion",
                column: "paciente_id",
                unique: true);
        }
    }
}
