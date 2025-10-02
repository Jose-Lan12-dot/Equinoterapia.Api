using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class ModeloFormularioYAtributoEstadoPacientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "formulario",
                columns: table => new
                {
                    formulario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formulario", x => x.formulario_id);
                    table.ForeignKey(
                        name: "FK_formulario_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateTable(
                name: "formulario_respuesta",
                columns: table => new
                {
                    formulario_respuesta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    formulario_id = table.Column<int>(type: "int", nullable: false),
                    consulta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    respuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formulario_respuesta", x => x.formulario_respuesta_id);
                    table.ForeignKey(
                        name: "FK_formulario_respuesta_formulario_formulario_id",
                        column: x => x.formulario_id,
                        principalTable: "formulario",
                        principalColumn: "formulario_id",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateIndex(
                name: "IX_formulario_paciente_id",
                table: "formulario",
                column: "paciente_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_formulario_respuesta_formulario_id",
                table: "formulario_respuesta",
                column: "formulario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formulario_respuesta");

            migrationBuilder.DropTable(
                name: "formulario");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "paciente");
        }
    }
}
