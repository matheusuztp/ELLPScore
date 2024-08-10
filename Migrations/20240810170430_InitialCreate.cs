using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELLPScore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    AlunoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Turma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Anotacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.AlunoID);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    DisciplinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.DisciplinaID);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    ProfessorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.ProfessorID);
                });

            migrationBuilder.CreateTable(
                name: "AlunoDisciplinas",
                columns: table => new
                {
                    AlunoID = table.Column<int>(type: "int", nullable: false),
                    DisciplinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoDisciplinas", x => new { x.AlunoID, x.DisciplinaID });
                    table.ForeignKey(
                        name: "FK_AlunoDisciplinas_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoDisciplinas_Disciplinas_DisciplinaID",
                        column: x => x.DisciplinaID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoID = table.Column<int>(type: "int", nullable: true),
                    ProfessorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "AlunoID");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "ProfessorID");
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    TurmaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoOuNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfessorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.TurmaID);
                    table.ForeignKey(
                        name: "FK_Turmas_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "ProfessorID");
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    NotaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaID = table.Column<int>(type: "int", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AlunoID = table.Column<int>(type: "int", nullable: true),
                    DisciplinaID = table.Column<int>(type: "int", nullable: true),
                    Periodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotaValor = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.NotaID);
                    table.ForeignKey(
                        name: "FK_Notas_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "AlunoID");
                    table.ForeignKey(
                        name: "FK_Notas_Disciplinas_DisciplinaID",
                        column: x => x.DisciplinaID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaID");
                    table.ForeignKey(
                        name: "FK_Notas_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "TurmaID");
                });

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

            migrationBuilder.CreateTable(
                name: "TurmaDisciplinas",
                columns: table => new
                {
                    TurmaID = table.Column<int>(type: "int", nullable: false),
                    DisciplinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaDisciplinas", x => new { x.TurmaID, x.DisciplinaID });
                    table.ForeignKey(
                        name: "FK_TurmaDisciplinas_Disciplinas_DisciplinaID",
                        column: x => x.DisciplinaID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaDisciplinas_Turmas_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turmas",
                        principalColumn: "TurmaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDisciplinas_DisciplinaID",
                table: "AlunoDisciplinas",
                column: "DisciplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AlunoID",
                table: "Feedbacks",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ProfessorID",
                table: "Feedbacks",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_AlunoID",
                table: "Notas",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_DisciplinaID",
                table: "Notas",
                column: "DisciplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_TurmaID",
                table: "Notas",
                column: "TurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAlunos_AlunoID",
                table: "TurmaAlunos",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplinas_DisciplinaID",
                table: "TurmaDisciplinas",
                column: "DisciplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorID",
                table: "Turmas",
                column: "ProfessorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoDisciplinas");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "TurmaAlunos");

            migrationBuilder.DropTable(
                name: "TurmaDisciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
