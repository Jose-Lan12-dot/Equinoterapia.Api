using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class addTableArchivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "acta_nacimiento",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "certificado_medico",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "comprobante_domicilio",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "curp",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "diagnostico_medico",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "fotografia",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "ine_tutor",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "orden",
                table: "formulario_pregunta");

            migrationBuilder.AlterColumn<string>(
                name: "tutor",
                table: "paciente",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "telefono_tutor",
                table: "paciente",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "paciente",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha_nacimiento",
                table: "paciente",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "genero",
                table: "paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "archivo",
                columns: table => new
                {
                    archivo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paciente_id = table.Column<int>(type: "int", nullable: false),
                    diagnostico_medico = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    certificado_medico = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    acta_nacimiento = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    curp = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    comprobante_domicilio = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ine_tutor = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    fotografia = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivo", x => x.archivo_id);
                    table.ForeignKey(
                        name: "FK_archivo_paciente_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "paciente",
                        principalColumn: "paciente_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "formulario_pregunta",
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
                name: "IX_archivo_paciente_id",
                table: "archivo",
                column: "paciente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivo");

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "formulario_pregunta",
                keyColumn: "pregunta_id",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "genero",
                table: "paciente");

            migrationBuilder.AlterColumn<string>(
                name: "tutor",
                table: "paciente",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "telefono_tutor",
                table: "paciente",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "paciente",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha_nacimiento",
                table: "paciente",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "acta_nacimiento",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "certificado_medico",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "comprobante_domicilio",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "curp",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "diagnostico_medico",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "fotografia",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ine_tutor",
                table: "paciente",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "orden",
                table: "formulario_pregunta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
