using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIproyectoEquinoterapia.Migrations
{
    /// <inheritdoc />
    public partial class addModelFormularioPregunta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "consulta",
                table: "formulario_respuesta");

            migrationBuilder.RenameColumn(
                name: "formulario_respuesta_id",
                table: "formulario_respuesta",
                newName: "respuesta_id");

            migrationBuilder.AddColumn<int>(
                name: "pregunta_id",
                table: "formulario_respuesta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "formulario_pregunta",
                columns: table => new
                {
                    pregunta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formulario_pregunta", x => x.pregunta_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_formulario_respuesta_pregunta_id",
                table: "formulario_respuesta",
                column: "pregunta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_formulario_respuesta_formulario_pregunta_pregunta_id",
                table: "formulario_respuesta",
                column: "pregunta_id",
                principalTable: "formulario_pregunta",
                principalColumn: "pregunta_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formulario_respuesta_formulario_pregunta_pregunta_id",
                table: "formulario_respuesta");

            migrationBuilder.DropTable(
                name: "formulario_pregunta");

            migrationBuilder.DropIndex(
                name: "IX_formulario_respuesta_pregunta_id",
                table: "formulario_respuesta");

            migrationBuilder.DropColumn(
                name: "pregunta_id",
                table: "formulario_respuesta");

            migrationBuilder.RenameColumn(
                name: "respuesta_id",
                table: "formulario_respuesta",
                newName: "formulario_respuesta_id");

            migrationBuilder.AddColumn<string>(
                name: "consulta",
                table: "formulario_respuesta",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
