using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELLPScore.Migrations
{
    /// <inheritdoc />
    public partial class Disciplinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Disciplinas",
            columns: new[] { "Nome" },
            values: new object[,]
            {
                { "Língua Portuguesa" },
                { "Matemática" },
                { "Ciências" },
                { "História" },
                { "Geografia" },
                { "Artes" },
                { "Educação Física" },
                { "Inglês" },
                { "Ensino Religioso" },
                { "Redação" },
                { "Física" },
                { "Química" },
                { "Biologia" },
                { "Sociologia" },
                { "Filosofia" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "Disciplinas",
            keyColumn: "Nome",
            keyValues: new object[]
            {
                "Língua Portuguesa",
                "Matemática",
                "Ciências",
                "História",
                "Geografia",
                "Artes",
                "Educação Física",
                "Inglês",
                "Ensino Religioso",
                "Redação",
                "Física",
                "Química",
                "Biologia",
                "Sociologia",
                "Filosofia"
            });
        }
    }
}
