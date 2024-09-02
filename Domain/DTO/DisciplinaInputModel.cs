using System.ComponentModel.DataAnnotations;

namespace ELLPScore.Domain.DTO
{
    public class DisciplinaInputModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public string Nome { get; set; }
    }
}
