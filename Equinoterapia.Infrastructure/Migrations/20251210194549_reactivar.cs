using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equinoterapia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reactivar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_ultimo_registro",
                table: "paciente",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_ultimo_registro",
                table: "paciente");
        }
    }
}
