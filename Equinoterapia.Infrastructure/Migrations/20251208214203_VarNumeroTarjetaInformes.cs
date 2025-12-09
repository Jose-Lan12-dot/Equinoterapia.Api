using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equinoterapia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VarNumeroTarjetaInformes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "aiencia_id",
                table: "asistencia",
                newName: "asistencia_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_nacimiento",
                table: "paciente",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");


            migrationBuilder.DropColumn(
                name: "Foto",
                table: "inicioSesion");


            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "inicioSesion",
                type: "image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "numero_tarjeta",
                table: "informe_mensual",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero_tarjeta",
                table: "informe_mensual");

            migrationBuilder.RenameColumn(
                name: "asistencia_id",
                table: "asistencia",
                newName: "aistencia_id");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha_nacimiento",
                table: "paciente",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.DropColumn(
                    name: "Foto",
                    table: "inicioSesion");

         
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "inicioSesion",
                type: "nvarchar(max)", 
                nullable: true);
        }
    }
}
