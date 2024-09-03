using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.CadastrarDisciplina
{
    public class IndexModel : PageModel
    {
        private readonly IDisciplinaService _disciplinaService;

        [BindProperty]
        public DisciplinaInputModel DisciplinaInput { get; set; }

        public IndexModel(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        public void OnGetAsync()
        {
            DisciplinaInput = new DisciplinaInputModel();
        }

        public IActionResult OnGetEdit(int id)
        {
            var disciplina = _disciplinaService.GetAllDisciplinas().FirstOrDefault(t => t.DisciplinaID == id);

            if (disciplina == null)
            {
                return NotFound();
            }

            DisciplinaInput = new DisciplinaInputModel
            {
                Id = disciplina.DisciplinaID,
                Nome = disciplina.Nome
            };

            return Page();
        }

        public IActionResult OnPostEdit(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var disciplinaUpdate = _disciplinaService.GetAllDisciplinas().FirstOrDefault(t => t.DisciplinaID == id);

            if (disciplinaUpdate == null)
            {
                return NotFound();
            }

            disciplinaUpdate.Nome = DisciplinaInput.Nome;
            disciplinaUpdate.DisciplinaID = DisciplinaInput.Id;

            if (_disciplinaService.AtualizarDisciplina(disciplinaUpdate, out var erro))
            {
                return RedirectToPage("/Disciplinas/Index");
            }

            ModelState.AddModelError(string.Empty, erro);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var novaDisciplina = new Disciplina
            {
                Nome = DisciplinaInput.Nome
            };

            if (!_disciplinaService.CadastrarDisciplina(novaDisciplina, out string erro))
            {
                ModelState.AddModelError(string.Empty, erro);
                return Page();
            }

            return RedirectToPage("/Disciplinas/Index");
        }

    }
}
