using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.Professores
{
    public class IndexModel : PageModel
    {
        private readonly IProfessorService _professorService;

        public IndexModel(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        public IList<Professor> Professores { get; set; }

        public void OnGetAsync()
        {
            Professores = _professorService.GetAllProfessoresAsync().GetAwaiter().GetResult();
        }

        public IActionResult OnPostDelete(int id)
        {
            var professor = _professorService.GetAllProfessoresAsync().GetAwaiter().GetResult().FirstOrDefault(t => t.Id == id);

            if (professor == null)
            {
                return Page();
            }

            var success = _professorService.ExcluirProfessorAsync(professor);

            if (!success.IsCompleted)
            {
                ModelState.AddModelError(string.Empty, "Erro ao excluir professor");
                return Page();
            }

            Professores = _professorService.GetAllProfessoresAsync().GetAwaiter().GetResult();
            return Page();
        }

    }
}
