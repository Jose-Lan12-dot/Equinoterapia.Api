using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equinoterapia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModuloInformes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "informe_mensual",
                columns: table => new
                {
                    informe_mensual_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mes = table.Column<int>(type: "int", nullable: false),
                    anio = table.Column<int>(type: "int", nullable: false),
                    fecha_modificaccion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_informe_mensual", x => x.informe_mensual_id);
                });

            migrationBuilder.CreateTable(
                name: "detalle_informe",
                columns: table => new
                {
                    detalle_informe_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    informe_mensual_id = table.Column<int>(type: "int", nullable: false),
                    NumeroOrden = table.Column<int>(type: "int", nullable: false),
                    situacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avances = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tramite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_informe", x => x.detalle_informe_id);
                    table.ForeignKey(
                        name: "FK_detalle_informe_informe_mensual_informe_mensual_id",
                        column: x => x.informe_mensual_id,
                        principalTable: "informe_mensual",
                        principalColumn: "informe_mensual_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalle_informe_informe_mensual_id",
                table: "detalle_informe",
                column: "informe_mensual_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_informe");

            migrationBuilder.DropTable(
                name: "informe_mensual");
        }
    }
}
