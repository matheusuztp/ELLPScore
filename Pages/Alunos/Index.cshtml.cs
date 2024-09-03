using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Pages.Alunos
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoService _alunoService;

        public IndexModel(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [BindProperty]
        public AlunoInputModel Aluno { get; set; }

        public IList<Aluno> Alunos { get; set; }

        public void OnGetAsync()
        {
            Alunos = _alunoService.GetAllAlunos();
        }

        public IActionResult OnPostDelete(int id)
        {
            var aluno = _alunoService.GetAllAlunos().FirstOrDefault(t => t.AlunoID == id);

            if (aluno == null)
            {
                return Page();
            }

            var success = _alunoService.ExcluirAluno(id, out var erro);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, erro);
                return Page();
            }

            Alunos = _alunoService.GetAllAlunos();
            return Page();
        }
    }
}
