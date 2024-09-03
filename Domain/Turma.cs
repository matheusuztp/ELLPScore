namespace ELLPScore.Domain
{
    public class Turma
    {
        public int TurmaID { get; set; }
        public string CodigoOuNome { get; set; }

        public int? ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public ICollection<Nota> Notas { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
        public ICollection<TurmaDisciplina> TurmaDisciplinas { get; set; }
    }

}
