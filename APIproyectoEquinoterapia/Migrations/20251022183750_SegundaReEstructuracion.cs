using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class SegundaReEstructuracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivos_tipo_archivo_tipo_archivo_id",
                table: "archivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipo_archivo",
                table: "tipo_archivo");

            migrationBuilder.RenameTable(
                name: "tipo_archivo",
                newName: "tipos_archivos");

            migrationBuilder.RenameColumn(
                name: "tipo_archivo_id",
                table: "archivos",
                newName: "archivo_permitido_id");

            migrationBuilder.RenameIndex(
                name: "IX_archivos_tipo_archivo_id",
                table: "archivos",
                newName: "IX_archivos_archivo_permitido_id");

            migrationBuilder.RenameColumn(
                name: "tipo_archivo_id",
                table: "tipos_archivos",
                newName: "tipo_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_archivos",
                table: "tipos_archivos",
                column: "tipo_id");

            migrationBuilder.InsertData(
                table: "tipos_archivos",
                columns: new[] { "tipo_id", "extension", "Nombre" },
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

            migrationBuilder.AddForeignKey(
                name: "FK_archivos_tipos_archivos_archivo_permitido_id",
                table: "archivos",
                column: "archivo_permitido_id",
                principalTable: "tipos_archivos",
                principalColumn: "tipo_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivos_tipos_archivos_archivo_permitido_id",
                table: "archivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_archivos",
                table: "tipos_archivos");

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "tipo_id",
                keyValue: 7);

            migrationBuilder.RenameTable(
                name: "tipos_archivos",
                newName: "tipo_archivo");

            migrationBuilder.RenameColumn(
                name: "archivo_permitido_id",
                table: "archivos",
                newName: "tipo_archivo_id");

            migrationBuilder.RenameIndex(
                name: "IX_archivos_archivo_permitido_id",
                table: "archivos",
                newName: "IX_archivos_tipo_archivo_id");

            migrationBuilder.RenameColumn(
                name: "tipo_id",
                table: "tipo_archivo",
                newName: "tipo_archivo_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipo_archivo",
                table: "tipo_archivo",
                column: "tipo_archivo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_archivos_tipo_archivo_tipo_archivo_id",
                table: "archivos",
                column: "tipo_archivo_id",
                principalTable: "tipo_archivo",
                principalColumn: "tipo_archivo_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
