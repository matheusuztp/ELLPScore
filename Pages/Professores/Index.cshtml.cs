using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Pages.Professores
{
    public class IndexModel : PageModel
    {
        private readonly IProfessorService _professorService;

        public IndexModel(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [BindProperty]
        public ProfessorInputModel Professor { get; set; }

        public IList<Professor> Professores { get; set; }

        public async Task OnGetAsync()
        {
            Professores = await _professorService.GetAllProfessorAsync();
        }

        public async Task<IActionResult> OnPostCadastrarProfessorAsync()
        {
            if (!ModelState.IsValid)
            {
                Professores = await _professorService.GetAllProfessorAsync(); // Recarrega os alunos para exibir novamente na página
                return Page();
            }

            var professor = new Professor { UserName = Professor.Email, Email = Professor.Email, Nome = Professor.Nome, EmailConfirmed = true, NormalizedEmail = Professor.Email };
            

            var result = await _professorService.CadastrarProfessorAsync(professor, Professor.Password);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetCadastrarProfessorPartial()
        {
            return RedirectToPage("_CadastrarProfessorPartial", new ProfessorInputModel());
        }
    }
}
