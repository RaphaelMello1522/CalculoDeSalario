using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Cargo_CargoId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Salary_People_PeopleId",
                table: "Salary");

            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salary",
                table: "Salary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo");

            migrationBuilder.RenameTable(
                name: "Salary",
                newName: "Salaries");

            migrationBuilder.RenameTable(
                name: "Cargo",
                newName: "Cargos");

            migrationBuilder.RenameIndex(
                name: "IX_Salary_PeopleId",
                table: "Salaries",
                newName: "IX_Salaries_PeopleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Cargos_CargoId",
                table: "People",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_People_PeopleId",
                table: "Salaries",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Cargos_CargoId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_People_PeopleId",
                table: "Salaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos");

            migrationBuilder.RenameTable(
                name: "Salaries",
                newName: "Salary");

            migrationBuilder.RenameTable(
                name: "Cargos",
                newName: "Cargo");

            migrationBuilder.RenameIndex(
                name: "IX_Salaries_PeopleId",
                table: "Salary",
                newName: "IX_Salary_PeopleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salary",
                table: "Salary",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidatosVagaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CardImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoVaga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeVaga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagas_People_CandidatosVagaId",
                        column: x => x.CandidatosVagaId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_CandidatosVagaId",
                table: "Vagas",
                column: "CandidatosVagaId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Cargo_CargoId",
                table: "People",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_People_PeopleId",
                table: "Salary",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
