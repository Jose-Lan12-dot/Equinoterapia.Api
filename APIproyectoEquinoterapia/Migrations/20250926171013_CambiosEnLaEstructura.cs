using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEnLaEstructura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Caballo",
                newName: "caballo"
            );

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "instructor"
            );

            migrationBuilder.RenameTable(
                name: "Paciente",
                newName: "paciente"
            );

            migrationBuilder.RenameTable(
                name: "Sesion",
                newName: "sesion"
            );

            migrationBuilder.RenameTable(
                name: "Cita",
                newName: "cita"
            );

            migrationBuilder.DropTable(
                name: "Usuario"
            );

            migrationBuilder.DropColumn(
                name: "faltas",
                table: "paciente"
            );

            migrationBuilder.CreateIndex(
                name: "IX_cita_paciente_id",
                table: "cita",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_cita_sesion_id",
                table: "cita",
                column: "sesion_id");

            migrationBuilder.CreateIndex(
                name: "IX_sesion_instructor_id",
                table: "sesion",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "IX_sesion_placa",
                table: "sesion",
                column: "placa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "caballo",
                newName: "Caballo"
            );

            migrationBuilder.RenameTable(
                name: "instructor",
                newName: "Instructor"
            );

            migrationBuilder.RenameTable(
                name: "paciente",
                newName: "Paciente"
            );

            migrationBuilder.RenameTable(
                name: "sesion",
                newName: "Sesion"
            );

            migrationBuilder.RenameTable(
                name: "cita",
                newName: "Cita"
            );

            migrationBuilder.AddColumn<int>(
                name: "faltas",
                table: "Paciente",
                type: "int",
                nullable: true,
                defaultValue: 0
            );

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    contrasena = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.usuario_id);
                });
        }
    }
}
