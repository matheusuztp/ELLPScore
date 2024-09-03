using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELLPScore.Migrations
{
    /// <inheritdoc />
    public partial class removeturmaalunos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurmaAlunos");

            migrationBuilder.DropColumn(
                name: "Turma",
                table: "Alunos");

            migrationBuilder.AddColumn<int>(
                name: "TurmaID",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaID",
                table: "Alunos",
                column: "TurmaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_TurmaID",
                table: "Alunos",
                column: "TurmaID",
                principalTable: "Turmas",
                principalColumn: "TurmaID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_TurmaID",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_TurmaID",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "TurmaID",
                table: "Alunos");

            migrationBuilder.AddColumn<string>(
                name: "Turma",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TurmaAlunos",
                columns: table => new
                {
                    TurmaID = table.Column<int>(type: "int", nullable: false),
                    AlunoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAlunos", x => new { x.TurmaID, x.AlunoID });
                    table.ForeignKey(
                        name: "FK_TurmaAlunos_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaAlunos_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "TurmaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAlunos_AlunoID",
                table: "TurmaAlunos",
                column: "AlunoID");
        }
    }
}
