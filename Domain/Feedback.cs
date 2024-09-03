namespace ELLPScore.Domain
{
    public class Feedback
    {
        public int ID { get; set; }

        public string FeedbackTexto { get; set; }

        public int? AlunoID { get; set; }
        public Aluno Aluno { get; set; }

        public int? ProfessorID { get; set; }
        public Professor Professor { get; set; }
    }

}
