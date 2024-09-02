using System.ComponentModel.DataAnnotations;

namespace ELLPScore.Domain.DTO
{
    public class AlunoInputModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O nome não pode exceder 255 caracteres.")]
        public string Nome { get; set; }

        [CPFValidator(ErrorMessage = "O CPF é inválido.")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, ErrorMessage = "O CPF deve ter exatamente 11 caracteres.", MinimumLength = 11)]
        public string CPF { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(255, ErrorMessage = "O e-mail não pode exceder 255 caracteres.")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "A matrícula não pode exceder 50 caracteres.")]
        public string Matricula { get; set; }

        public string Serie { get; set; }
        
        [Range(1, 120, ErrorMessage = "A idade deve ser um número inteiro entre 1 e 120.")]
        [Required(ErrorMessage = "A idade é obrigatória.")]
        public int Idade { get; set; }

        public string Turma { get; set; }

        public string Anotacoes { get; set; }
    }
}
