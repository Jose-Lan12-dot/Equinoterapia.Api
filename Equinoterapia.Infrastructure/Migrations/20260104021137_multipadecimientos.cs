using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Equinoterapia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class multipadecimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "padecimientos",
                table: "caballo");

            migrationBuilder.AlterColumn<string>(
                name: "placa",
                table: "caballo",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "caballo",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "padecimiento",
                columns: table => new
                {
                    padecimiento_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    padecimiento_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_padecimiento", x => x.padecimiento_id);
                });

            migrationBuilder.CreateTable(
                name: "historial_medico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    caballo_id = table.Column<int>(type: "int", nullable: false),
                    padecimiento_id = table.Column<int>(type: "int", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historial_medico", x => x.id);
                    table.ForeignKey(
                        name: "FK_historial_medico_caballo_caballo_id",
                        column: x => x.caballo_id,
                        principalTable: "caballo",
                        principalColumn: "caballo_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historial_medico_padecimiento_padecimiento_id",
                        column: x => x.padecimiento_id,
                        principalTable: "padecimiento",
                        principalColumn: "padecimiento_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "padecimiento",
                columns: new[] { "padecimiento_id", "padecimiento_nombre" },
                values: new object[,]
                {
                    { 3, "Artritis" },
                    { 4, "Infección Respiratoria" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_historial_medico_caballo_id",
                table: "historial_medico",
                column: "caballo_id");

            migrationBuilder.CreateIndex(
                name: "IX_historial_medico_padecimiento_id",
                table: "historial_medico",
                column: "padecimiento_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historial_medico");

            migrationBuilder.DropTable(
                name: "padecimiento");

            migrationBuilder.AlterColumn<string>(
                name: "placa",
                table: "caballo",
                type: "char(10)",
                unicode: false,
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "caballo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AddColumn<string>(
                name: "padecimientos",
                table: "caballo",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
