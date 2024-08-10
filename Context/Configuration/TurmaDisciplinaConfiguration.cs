using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TurmaDisciplinaConfiguration : IEntityTypeConfiguration<TurmaDisciplina>
{
    public void Configure(EntityTypeBuilder<TurmaDisciplina> builder)
    {
        builder.HasKey(td => new { td.TurmaID, td.DisciplinaID });

        builder.HasOne(td => td.Turma)
               .WithMany(t => t.TurmaDisciplinas)
               .HasForeignKey(td => td.TurmaID);

        builder.HasOne(td => td.Disciplina)
               .WithMany(d => d.TurmaDisciplinas)
               .HasForeignKey(td => td.DisciplinaID);
    }
}
