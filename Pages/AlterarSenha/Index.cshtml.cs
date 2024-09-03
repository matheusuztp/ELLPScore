using ELLPScore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Pages.AlterarSenha
{
    public class IndexModel : PageModel
    {
        private readonly IProfessorService _professorService;

        [BindProperty]
        public int ProfessorId { get; set; }

        [BindProperty]
        public string NovaSenha { get; set; }

        public IndexModel(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        public IActionResult OnGet(int id)
        {
            ProfessorId = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var professor = await _professorService.GetProfessorByIdAsync(ProfessorId);

            if (professor == null)
            {
                return NotFound();
            }

            var result = await _professorService.AlterarSenhaAsync(professor, NovaSenha);

            if (result.Succeeded)
            {
                return RedirectToPage("/Professores/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
