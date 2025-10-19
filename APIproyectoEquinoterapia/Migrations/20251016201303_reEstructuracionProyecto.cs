using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class reEstructuracionProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formulario_respuesta");

            migrationBuilder.DropTable(
                name: "formulario");

            migrationBuilder.DropTable(
                name: "formulario_pregunta");

            migrationBuilder.CreateTable(
                name: "evaluacion",
                columns: table => new
                {
                    evaluacion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluacion", x => x.evaluacion_id);
                    table.ForeignKey(
                        name: "FK_evaluacion_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "preguntas",
                columns: table => new
                {
                    pregunta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntas", x => x.pregunta_id);
                });

            migrationBuilder.CreateTable(
                name: "respuestas",
                columns: table => new
                {
                    respuesta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    evaluacion_id = table.Column<int>(type: "int", nullable: false),
                    pregunta_id = table.Column<int>(type: "int", nullable: false),
                    respuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas", x => x.respuesta_id);
                    table.ForeignKey(
                        name: "FK_respuestas_evaluacion_evaluacion_id",
                        column: x => x.evaluacion_id,
                        principalTable: "evaluacion",
                        principalColumn: "evaluacion_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_respuestas_preguntas_pregunta_id",
                        column: x => x.pregunta_id,
                        principalTable: "preguntas",
                        principalColumn: "pregunta_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "preguntas",
                columns: new[] { "pregunta_id", "pregunta" },
                values: new object[,]
                {
                    { 1, "CONTACTO VISUAL" },
                    { 2, "SECUENCIA DE INDICACIONES" },
                    { 3, "UTILIZA COMO INSTRUMENTO AL ADULTO" },
                    { 4, "CONDUCTA DE SEÑALADO PROIMPERATIVAS (PEDIR PARA EL)" },
                    { 5, "CONDUCTA DE SEÑALADO PROTODECLARATIVAS (FINALIDAD DE COMPARTIR)" },
                    { 6, "ATENCION CONJUNTA DE CUENTOS, JUEGOS O CANCIONES" },
                    { 7, "MOSTRAR INTERES POR COMPARTIR OBJETOS" },
                    { 8, "IMITACIÓN MOTORA GRUESA" },
                    { 9, "IMITACIÓN MOTORA FINA" },
                    { 10, "IMITACIÓN VOCAL" },
                    { 11, "PRESENCIA DE LENGUAJE ORAL Y NIVEL" },
                    { 12, "ECOLALIAL" },
                    { 13, "COMPRENSIÓN DE ORDENES SENCILLAS" },
                    { 14, "JUEGO DE FICCIÓN O SIMBÓLICO" },
                    { 15, "FLEXIBILIDAD A LOS CAMBIOS" },
                    { 16, "NIVEL DE ACTIVIDAD MOTORA" },
                    { 17, "NIVEL DE ATENCIÓN (TIEMPO DE ATENCIÓN REALIZANDO LA ACTIVIDAD)" },
                    { 18, "MOTIVACIÓN (INTERES RESTRINGIDOS)" },
                    { 19, "PLANIFICACIÓN" },
                    { 20, "NIVEL DE IMPULSIVIDAD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_evaluacion_paciente_id",
                table: "evaluacion",
                column: "paciente_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_evaluacion_id",
                table: "respuestas",
                column: "evaluacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_pregunta_id",
                table: "respuestas",
                column: "pregunta_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "evaluacion");

            migrationBuilder.DropTable(
                name: "preguntas");

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
                name: "formulario_pregunta",
                columns: table => new
                {
                    pregunta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formulario_pregunta", x => x.pregunta_id);
                });

            migrationBuilder.CreateTable(
                name: "formulario_respuesta",
                columns: table => new
                {
                    respuesta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    formulario_id = table.Column<int>(type: "int", nullable: false),
                    pregunta_id = table.Column<int>(type: "int", nullable: false),
                    respuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formulario_respuesta", x => x.respuesta_id);
                    table.ForeignKey(
                        name: "FK_formulario_respuesta_formulario_formulario_id",
                        column: x => x.formulario_id,
                        principalTable: "formulario",
                        principalColumn: "formulario_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_formulario_respuesta_formulario_pregunta_pregunta_id",
                        column: x => x.pregunta_id,
                        principalTable: "formulario_pregunta",
                        principalColumn: "pregunta_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_formulario_respuesta_pregunta_id",
                table: "formulario_respuesta",
                column: "pregunta_id");
        }
    }
}
