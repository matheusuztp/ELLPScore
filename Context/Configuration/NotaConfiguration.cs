using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Context.Configuration
{
    public class NotaConfiguration : IEntityTypeConfiguration<Nota>
    {
        public void Configure(EntityTypeBuilder<Nota> builder)
        {
            builder.HasKey(n => n.NotaID);

            builder.Property(n => n.Serie).HasMaxLength(10);
            builder.Property(n => n.NotaValor).HasColumnType("decimal(5,2)");

            builder.HasOne(n => n.Aluno)
                   .WithMany(a => a.Notas)
                   .HasForeignKey(n => n.AlunoID);

            builder.HasOne(n => n.Disciplina)
                   .WithMany(d => d.Notas)
                   .HasForeignKey(n => n.DisciplinaID);

            builder.HasOne(n => n.Turma)
                   .WithMany(t => t.Notas)
                   .HasForeignKey(n => n.TurmaID);
        }
    }
}
