using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class TerceraReEstructuracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivos_tipos_archivos_archivo_permitido_id",
                table: "archivos");

            migrationBuilder.RenameColumn(
                name: "archivo_permitido_id",
                table: "archivos",
                newName: "tipo_id");

            migrationBuilder.RenameIndex(
                name: "IX_archivos_archivo_permitido_id",
                table: "archivos",
                newName: "IX_archivos_tipo_id");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha",
                table: "evaluacion",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_archivos_tipos_archivos_tipo_id",
                table: "archivos",
                column: "tipo_id",
                principalTable: "tipos_archivos",
                principalColumn: "tipo_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivos_tipos_archivos_tipo_id",
                table: "archivos");

            migrationBuilder.RenameColumn(
                name: "tipo_id",
                table: "archivos",
                newName: "archivo_permitido_id");

            migrationBuilder.RenameIndex(
                name: "IX_archivos_tipo_id",
                table: "archivos",
                newName: "IX_archivos_archivo_permitido_id");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha",
                table: "evaluacion",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_archivos_tipos_archivos_archivo_permitido_id",
                table: "archivos",
                column: "archivo_permitido_id",
                principalTable: "tipos_archivos",
                principalColumn: "tipo_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
