using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Context.Configuration
{
    public class AlunoDisciplinaConfiguration : IEntityTypeConfiguration<AlunoDisciplina>
    {
        public void Configure(EntityTypeBuilder<AlunoDisciplina> builder)
        {
            builder.HasKey(ad => new { ad.AlunoID, ad.DisciplinaID });

            builder.HasOne(ad => ad.Aluno)
                   .WithMany(a => a.AlunoDisciplinas)
                   .HasForeignKey(ad => ad.AlunoID);

            builder.HasOne(ad => ad.Disciplina)
                   .WithMany(d => d.AlunoDisciplinas)
                   .HasForeignKey(ad => ad.DisciplinaID);
        }
    }
}
