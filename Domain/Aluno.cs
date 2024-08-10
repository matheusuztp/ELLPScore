namespace ELLPScore.Domain
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        public string Nome { get; set; }
        public string Serie { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public string Turma { get; set; }
        public string Matricula { get; set; }
        public string Anotacoes { get; set; }
        public string Email { get; set; }

        public ICollection<AlunoDisciplina> AlunoDisciplinas { get; set; }
        public ICollection<Nota> Notas { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<TurmaAluno> TurmaAlunos { get; set; }
    }

}
