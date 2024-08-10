using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Context.Configuration
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.HasKey(p => p.ProfessorID);

            builder.Property(p => p.Nome).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Email).HasMaxLength(255);
            builder.Property(p => p.Senha).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Feedbacks)
                   .WithOne(f => f.Professor)
                   .HasForeignKey(f => f.ProfessorID);

            builder.HasMany(p => p.Turmas)
                   .WithOne(t => t.Professor)
                   .HasForeignKey(t => t.ProfessorID);
        }
    }
}
