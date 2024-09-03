using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.CadastrarTurma
{
    public class IndexModel : PageModel
    {
        private readonly IProfessorService _professorService;
        private readonly ITurmaService _turmaService;

        [BindProperty]
        public TurmaInputModel TurmaInput { get; set; }
        public IList<Professor> Professores { get; set; }


        public IndexModel(IProfessorService professorService, ITurmaService turmaService)
        {
            _professorService = professorService;
            _turmaService = turmaService;
        }

        public void OnGetAsync()
        {
            TurmaInput = new TurmaInputModel();
            Professores = _professorService.GetAllProfessoresAsync().GetAwaiter().GetResult();
            ViewData["Professores"] = new SelectList(Professores, "Id", "Nome");
        }

        public IActionResult OnGetEdit(int id)
        {
            var turma = _turmaService.GetAllTurmas().FirstOrDefault(t => t.TurmaID == id);

            if (turma == null)
            {
                return NotFound();
            }

            TurmaInput = new TurmaInputModel
            {
                CodigoOuNome = turma.CodigoOuNome,
                ProfessorID = turma.ProfessorID
            };

            RefreshData();

            return Page();
        }

        public IActionResult OnPostEdit(int id)
        {
            if (!ModelState.IsValid)
            {
                RefreshData();
                return Page();
            }

            var turmaToUpdate = _turmaService.GetAllTurmas().FirstOrDefault(t => t.TurmaID == id);

            if (turmaToUpdate == null)
            {
                return NotFound();
            }

            turmaToUpdate.CodigoOuNome = TurmaInput.CodigoOuNome;
            turmaToUpdate.ProfessorID = TurmaInput.ProfessorID;

            if (_turmaService.AtualizarTurma(turmaToUpdate, out var erro))
            {
                return RedirectToPage("/Turmas/Index");
            }

            ModelState.AddModelError(string.Empty, erro);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RefreshData();
                return Page();
            }

            var novaTurma = new Turma
            {
                CodigoOuNome = TurmaInput.CodigoOuNome,
                ProfessorID = TurmaInput.ProfessorID
            };

            if(!_turmaService.CadastrarTurma(novaTurma, out string erro))
            {
                ModelState.AddModelError(string.Empty, erro);
                RefreshData();
                return Page();
            }

            return RedirectToPage("/Turmas/Index");
        }

       

        public void RefreshData()
        {
            Professores = _professorService.GetAllProfessoresAsync().GetAwaiter().GetResult();
            ViewData["Professores"] = new SelectList(Professores, "Id", "Nome");
        }
    }
}
