using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.Disciplinas
{
    public class IndexModel : PageModel
    {
        private readonly IDisciplinaService _disciplinaService;

        public IndexModel(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        

        public IList<Disciplina> Disciplinas { get; set; }

        public void OnGetAsync()
        {
            Disciplinas = _disciplinaService.GetAllDisciplinas();
        }

        public IActionResult OnPostDelete(int id)
        {
            var disciplina = _disciplinaService.GetAllDisciplinas().FirstOrDefault(t => t.DisciplinaID == id);

            if (disciplina == null)
            {
                return Page();
            }

            var success = _disciplinaService.ExcluirDisciplina(disciplina.DisciplinaID, out string erro);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, erro);
                return Page();
            }

            Disciplinas = _disciplinaService.GetAllDisciplinas();
            return Page();
        }

    }
}
