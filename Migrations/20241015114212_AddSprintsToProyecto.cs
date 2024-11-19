using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class AddSprintsToProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Sprints_SprintId",
                table: "Tareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprints",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Sprints");

            migrationBuilder.RenameTable(
                name: "Sprints",
                newName: "Sprint");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Sprint",
                newName: "NombreSprint");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Sprint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "Sprint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprint",
                table: "Sprint",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_ProyectoId",
                table: "Sprint",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprint_Proyectos_ProyectoId",
                table: "Sprint",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "ProyectoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Sprint_SprintId",
                table: "Tareas",
                column: "SprintId",
                principalTable: "Sprint",
                principalColumn: "SprintId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprint_Proyectos_ProyectoId",
                table: "Sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Sprint_SprintId",
                table: "Tareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprint",
                table: "Sprint");

            migrationBuilder.DropIndex(
                name: "IX_Sprint_ProyectoId",
                table: "Sprint");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Sprint");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "Sprint");

            migrationBuilder.RenameTable(
                name: "Sprint",
                newName: "Sprints");

            migrationBuilder.RenameColumn(
                name: "NombreSprint",
                table: "Sprints",
                newName: "Nombre");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Sprints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Sprints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprints",
                table: "Sprints",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Sprints_SprintId",
                table: "Tareas",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "SprintId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
