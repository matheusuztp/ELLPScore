using Microsoft.AspNetCore.Identity;

namespace ELLPScore.Domain
{
    public class Professor : IdentityUser<int>
    {
        public int ProfessorID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }

}
