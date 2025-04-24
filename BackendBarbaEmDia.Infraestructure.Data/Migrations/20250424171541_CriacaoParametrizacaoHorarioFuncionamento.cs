using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendBarbaEmDia.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoParametrizacaoHorarioFuncionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Travamentos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParametrizacaoHorarioFuncionamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrizacaoHorarioFuncionamento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ParametrizacaoHorarioFuncionamento",
                columns: new[] { "Id", "DiaSemana", "HoraFim", "HoraInicio" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, 2, new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 3, 3, new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 4, 4, new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 5, 5, new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 6, 6, new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 7, 0, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametrizacaoHorarioFuncionamento");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Travamentos");
        }
    }
}
