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
        private readonly IProfessorService _professorService;

        public IndexModel(ITurmaService turmaService, IProfessorService professorService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
        }

        [BindProperty]
        public TurmaInputModel Turma { get; set; }

        public IList<Turma> Turmas { get; set; }
        public IList<Professor> Professores { get; set; }

        public void OnGetAsync()
        {
            Turmas = _turmaService.GetAllTurmas();
            Professores = _professorService.GetAllProfessorAsync().GetAwaiter().GetResult();
            ViewData["Professores"] = new SelectList(Professores, "Id", "Nome");
        }

        public async Task<IActionResult> OnPostCadastrarTurmaAsync()
        {
            if (!ModelState.IsValid)
            {
                Turmas = _turmaService.GetAllTurmas(); // Recarrega as turmas para exibir novamente na página
                Professores = _professorService.GetAllProfessorAsync().GetAwaiter().GetResult();
                ViewData["Professores"] = new SelectList(Professores, "Id", "Nome");
                return Page();
            }

            var novaTurma = new Turma
            {
                CodigoOuNome = Turma.CodigoOuNome,
                ProfessorID = Turma.ProfessorID
            };

            await _turmaService.CadastrarTurmaAsync(novaTurma);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetCadastrarTurmaPartial()
        {
            return RedirectToPage("_CadastrarTurmaPartial", new TurmaInputModel());
        }
    }
}
