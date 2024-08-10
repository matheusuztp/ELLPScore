namespace ELLPScore.Domain
{
    public class Disciplina
    {
        public int DisciplinaID { get; set; }
        public string Nome { get; set; }

        public ICollection<AlunoDisciplina> AlunoDisciplinas { get; set; }
        public ICollection<Nota> Notas { get; set; }
        public ICollection<TurmaDisciplina> TurmaDisciplinas { get; set; }
    }

}
