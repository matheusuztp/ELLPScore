namespace ELLPScore.Context.Configuration
{
    using ELLPScore.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.AlunoID);

            builder.Property(a => a.Nome).IsRequired().HasMaxLength(255);
            builder.Property(a => a.CPF).IsRequired().HasMaxLength(11);
            builder.Property(a => a.Email).HasMaxLength(255);
            builder.Property(a => a.Matricula).HasMaxLength(50);

            builder.HasMany(a => a.AlunoDisciplinas)
                   .WithOne(ad => ad.Aluno)
                   .HasForeignKey(ad => ad.AlunoID);

            builder.HasMany(a => a.Notas)
                   .WithOne(n => n.Aluno)
                   .HasForeignKey(n => n.AlunoID);

            builder.HasMany(a => a.Feedbacks)
                   .WithOne(f => f.Aluno)
                   .HasForeignKey(f => f.AlunoID);

            builder.HasMany(a => a.TurmaAlunos)
                   .WithOne(ta => ta.Aluno)
                   .HasForeignKey(ta => ta.AlunoID);
        }
    }

}
