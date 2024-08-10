using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Context.Configuration
{
    public class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.HasKey(d => d.DisciplinaID);

            builder.Property(d => d.Nome).IsRequired().HasMaxLength(255);

            builder.HasMany(d => d.AlunoDisciplinas)
                   .WithOne(ad => ad.Disciplina)
                   .HasForeignKey(ad => ad.DisciplinaID);

            builder.HasMany(d => d.Notas)
                   .WithOne(n => n.Disciplina)
                   .HasForeignKey(n => n.DisciplinaID);

            builder.HasMany(d => d.TurmaDisciplinas)
                   .WithOne(td => td.Disciplina)
                   .HasForeignKey(td => td.DisciplinaID);
        }
    }
}
