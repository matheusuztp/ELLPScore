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

        public TurmaInputModel Turma { get; set; }
        public IList<Professor> Professores { get; set; }

        public IndexModel(IProfessorService professorService)
        {
            _professorService = professorService;
        } 

        public void OnGetAsync()
        {
            Turma = new TurmaInputModel();
            Professores = _professorService.GetAllProfessorAsync().GetAwaiter().GetResult();
            ViewData["Professores"] = new SelectList(Professores, "Id", "Nome");
        }
    }
}
