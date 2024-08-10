namespace ELLPScore.Domain
{
    public class AlunoDisciplina
    {
        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }

        public int DisciplinaID { get; set; }
        public Disciplina Disciplina { get; set; }
    }

}
