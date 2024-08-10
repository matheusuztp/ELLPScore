using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Context.Configuration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.ID);

            builder.HasOne(f => f.Aluno)
                   .WithMany(a => a.Feedbacks)
                   .HasForeignKey(f => f.AlunoID);

            builder.HasOne(f => f.Professor)
                   .WithMany(p => p.Feedbacks)
                   .HasForeignKey(f => f.ProfessorID);
        }
    }
}
