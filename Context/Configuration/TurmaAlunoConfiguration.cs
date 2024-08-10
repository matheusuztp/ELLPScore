using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TurmaAlunoConfiguration : IEntityTypeConfiguration<TurmaAluno>
{
    public void Configure(EntityTypeBuilder<TurmaAluno> builder)
    {
        builder.HasKey(ta => new { ta.TurmaID, ta.AlunoID });

        builder.HasOne(ta => ta.Turma)
               .WithMany(t => t.TurmaAlunos)
               .HasForeignKey(ta => ta.TurmaID);

        builder.HasOne(ta => ta.Aluno)
               .WithMany(a => a.TurmaAlunos)
               .HasForeignKey(ta => ta.AlunoID);
    }
}
