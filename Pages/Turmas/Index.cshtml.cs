using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.Turmas
{
    public class IndexModel : PageModel
    {
        private readonly ITurmaService _turmaService;

        public IndexModel(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [BindProperty]
        public TurmaInputModel Turma { get; set; }

        public IList<Turma> Turmas { get; set; }

        public void OnGetAsync()
        {
            Turmas = _turmaService.GetAllTurmas();
        }

        public IActionResult OnPostDelete(int id)
        {
            var turma = _turmaService.GetAllTurmas().FirstOrDefault(t => t.TurmaID == id);

            if (turma == null)
            {
                return Page();
            }

            var success = _turmaService.ExcluirTurma(id, out var erro);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, erro);
                return Page();
            }

            Turmas = _turmaService.GetAllTurmas();
            return Page();
        }

    }
}
