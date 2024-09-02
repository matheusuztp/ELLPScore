using System.ComponentModel.DataAnnotations;

namespace ELLPScore.Domain.DTO
{
    public class TurmaInputModel
    {
        [Required(ErrorMessage = "O código/nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public string CodigoOuNome { get; set; }

        [Required(ErrorMessage = "A seleção de um professor é obrigatória.")]
        public int? ProfessorID { get; set; }

        //public  IList<Professor> Professores { get; set; }
    }
}
