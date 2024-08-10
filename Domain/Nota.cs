namespace ELLPScore.Domain
{
    public class Nota
    {
        public int NotaID { get; set; }
        public int? TurmaID { get; set; }
        public Turma Turma { get; set; }

        public string Serie { get; set; }

        public int? AlunoID { get; set; }
        public Aluno Aluno { get; set; }

        public int? DisciplinaID { get; set; }
        public Disciplina Disciplina { get; set; }

        public string Periodo { get; set; }
        public decimal NotaValor { get; set; }
    }

}
