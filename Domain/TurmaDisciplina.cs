namespace ELLPScore.Domain
{
    public class TurmaDisciplina
    {
        public int TurmaID { get; set; }
        public Turma Turma { get; set; }

        public int DisciplinaID { get; set; }
        public Disciplina Disciplina { get; set; }
    }

}
