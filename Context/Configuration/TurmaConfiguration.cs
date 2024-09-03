using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.HasKey(t => t.TurmaID);

        builder.Property(t => t.CodigoOuNome).HasMaxLength(50);

        builder.HasOne(t => t.Professor)
               .WithMany(p => p.Turmas)
               .HasForeignKey(t => t.ProfessorID);

        builder.HasMany(t => t.Notas)
               .WithOne(n => n.Turma)
               .HasForeignKey(n => n.TurmaID);

        builder.HasMany(t => t.TurmaDisciplinas)
               .WithOne(td => td.Turma)
               .HasForeignKey(td => td.TurmaID);
    }
}
