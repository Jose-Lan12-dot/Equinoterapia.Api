using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Equinoterapia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialBaseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivos");

            migrationBuilder.DropTable(
                name: "asistencia");

            migrationBuilder.DropTable(
                name: "inicioSesion");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "tipos_archivos");

            migrationBuilder.DropTable(
                name: "sesion");

            migrationBuilder.DropTable(
                name: "evaluacion");

            migrationBuilder.DropTable(
                name: "preguntas");

            migrationBuilder.DropTable(
                name: "caballo");

            migrationBuilder.DropTable(
                name: "instructor");

            migrationBuilder.DropTable(
                name: "paciente");
        }
    }
}
