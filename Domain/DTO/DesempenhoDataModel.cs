namespace ELLPScore.Domain.DTO
{
    public class DesempenhoDataModel
    {
        public List<DesempenhoPorDisciplina> DesempenhoPorDisciplina { get; set; }
        public List<AlunosVsPeriodo> AlunosVsPeriodo { get; set; }
        public List<Aprovacoes> Aprovacoes { get; set; }
    }

    public class DesempenhoPorDisciplina
    {
        public string Disciplina { get; set; }
        public int Nota { get; set; }
    }

    public class AlunosVsPeriodo
    {
        public string Periodo { get; set; }
        public int Nota { get; set; }
    }

    public class Aprovacoes
    {
        public string Periodo { get; set; }
        public int Aprovado { get; set; }
    }

}
