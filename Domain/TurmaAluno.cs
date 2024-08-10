namespace ELLPScore.Domain
{
    public class TurmaAluno
    {
        public int TurmaID { get; set; }
        public Turma Turma { get; set; }

        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }
    }

}
