using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class CuartaReEstructuracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Elimina la tabla cita
            migrationBuilder.DropTable(
                name: "cita");

            // Crea la nueva tabla asistencia
            migrationBuilder.CreateTable(
                name: "asistencia",
                columns: table => new
                {
                    aiencia_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    sesion_id = table.Column<int>(type: "int", nullable: false),
                    asistio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asistencia", x => x.aiencia_id);
                    table.ForeignKey(
                        name: "FK_asistencia_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_asistencia_sesion_sesion_id",
                        column: x => x.sesion_id,
                        principalTable: "sesion",
                        principalColumn: "sesion_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_asistencia_paciente_id",
                table: "asistencia",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_asistencia_sesion_id",
                table: "asistencia",
                column: "sesion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Elimina la tabla asistencia
            migrationBuilder.DropTable(
                name: "asistencia");

            // Vuelve a crear la tabla cita con la estructura original
            migrationBuilder.CreateTable(
                name: "cita",
                columns: table => new
                {
                    cita_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    sesion_id = table.Column<int>(type: "int", nullable: false),
                    asistio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cita", x => x.cita_id);
                    table.ForeignKey(
                        name: "FK_cita_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cita_sesion_sesion_id",
                        column: x => x.sesion_id,
                        principalTable: "sesion",
                        principalColumn: "sesion_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cita_paciente_id",
                table: "cita",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_cita_sesion_id",
                table: "cita",
                column: "sesion_id");
        }
    }
}
