using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caballo",
                columns: table => new
                {
                    caballo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    placa = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_herraje = table.Column<DateOnly>(type: "date", nullable: false),
                    padecimientos = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caballo", x => x.caballo_id);
                });

            migrationBuilder.CreateTable(
                name: "inicioSesion",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdKronos = table.Column<int>(type: "int", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPerfil = table.Column<int>(type: "int", nullable: false),
                    Perfil = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "instructor",
                columns: table => new
                {
                    instructor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructor", x => x.instructor_id);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    paciente_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    tutor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    telefono_tutor = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paciente__46FEF656E35EEE7A", x => x.paciente_id);
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
                name: "tipo_archivo",
                columns: table => new
                {
                    tipo_archivo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_archivo", x => x.tipo_archivo_id);
                });

            migrationBuilder.CreateTable(
                name: "sesion",
                columns: table => new
                {
                    sesion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    instructor_id = table.Column<int>(type: "int", nullable: false),
                    caballo_id = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    hora = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sesion", x => x.sesion_id);
                    table.ForeignKey(
                        name: "FK_sesion_caballo_caballo_id",
                        column: x => x.caballo_id,
                        principalTable: "caballo",
                        principalColumn: "caballo_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sesion_instructor_instructor_id",
                        column: x => x.instructor_id,
                        principalTable: "instructor",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "archivos",
                columns: table => new
                {
                    archivo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    tipo_archivo_id = table.Column<int>(type: "int", nullable: false),
                    archivo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos", x => x.archivo_id);
                    table.ForeignKey(
                        name: "FK_archivos_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archivos_tipo_archivo_tipo_archivo_id",
                        column: x => x.tipo_archivo_id,
                        principalTable: "tipo_archivo",
                        principalColumn: "tipo_archivo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cita",
                columns: table => new
                {
                    cita_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sesion_id = table.Column<int>(type: "int", nullable: false),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    asistio = table.Column<bool>(type: "bit", nullable: true)
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

            migrationBuilder.InsertData(
                table: "tipo_archivo",
                columns: new[] { "tipo_archivo_id", "extension", "nombre" },
                values: new object[,]
                {
                    { 1, ".pdf", "DiagnosticoMedico" },
                    { 2, ".pdf", "CertificadoMedico" },
                    { 3, ".pdf", "ActaNacimiento" },
                    { 4, ".pdf", "Curp" },
                    { 5, ".pdf", "ComprobanteDomicilio" },
                    { 6, ".pdf", "IneTutor" },
                    { 7, ".jpg", "Fotografia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivos_paciente_id",
                table: "archivos",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_archivos_tipo_archivo_id",
                table: "archivos",
                column: "tipo_archivo_id");

            migrationBuilder.CreateIndex(
                name: "IX_cita_paciente_id",
                table: "cita",
                column: "paciente_id");

            migrationBuilder.CreateIndex(
                name: "IX_cita_sesion_id",
                table: "cita",
                column: "sesion_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_sesion_caballo_id",
                table: "sesion",
                column: "caballo_id");

            migrationBuilder.CreateIndex(
                name: "IX_sesion_instructor_id",
                table: "sesion",
                column: "instructor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivos");

            migrationBuilder.DropTable(
                name: "cita");

            migrationBuilder.DropTable(
                name: "inicioSesion");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "tipo_archivo");

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
