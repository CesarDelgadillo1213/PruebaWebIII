using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class AddSprintsToProyecto1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Sprint_SprintId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "SprintId",
                table: "Tareas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Sprint_SprintId",
                table: "Tareas",
                column: "SprintId",
                principalTable: "Sprint",
                principalColumn: "SprintId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Sprint_SprintId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "SprintId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Sprint_SprintId",
                table: "Tareas",
                column: "SprintId",
                principalTable: "Sprint",
                principalColumn: "SprintId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
