using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Pages.Disciplinas
{
    public class IndexModel : PageModel
    {
        private readonly IDisciplinaService _disciplinaService;

        public IndexModel(IDisciplinaService DisciplinaService)
        {
            _disciplinaService = DisciplinaService;
        }

        [BindProperty]
        public DisciplinaInputModel Disciplina { get; set; }

        public IList<Disciplina> Disciplinas { get; set; }

        public async Task OnGetAsync()
        {
            Disciplinas = await _disciplinaService.GetAllDisciplinaAsync();
        }

        public async Task<IActionResult> OnPostCadastrarDisciplinaAsync()
        {
            if (!ModelState.IsValid)
            {
                Disciplinas = await _disciplinaService.GetAllDisciplinaAsync(); // Recarrega os Disciplinas para exibir novamente na página
                return Page();
            }

            var novoDisciplina = new Disciplina { Nome = Disciplina.Nome };

            await _disciplinaService.CadastrardisciplinaAsync(novoDisciplina);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetCadastrarDisciplinaPartial()
        {
            return RedirectToPage("_CadastrarDisciplinaPartial", new DisciplinaInputModel());
        }
    }
}
