using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.Desempenho
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoService _alunoService;
        private readonly IDesempenhoService _desempenhoService;

        public IndexModel(IAlunoService alunoService, IDesempenhoService desempenhoService)
        {
            _alunoService = alunoService;
            _desempenhoService = desempenhoService;
        }

        [BindProperty(SupportsGet = true)]
        public int AlunoSelecionado { get; set; }

        public DesempenhoDataModel DesempenhoDataModel { get; set; }
        public SelectList Alunos { get; set; }

        public void OnGet()
        {
            var alunos = _alunoService.GetAllAlunos()
                .Select(a => new { a.AlunoID, a.Nome })
                .ToList();
            DesempenhoDataModel = _desempenhoService.GetDesempenhoPorAluno(alunos.First().AlunoID);
            Alunos = new SelectList(alunos, "AlunoID", "Nome");
        }

        public PartialViewResult OnGetDesempenhoPorAluno()
        {
            DesempenhoDataModel = _desempenhoService.GetDesempenhoPorAluno(AlunoSelecionado);
            return Partial("_DesempenhoPartial", DesempenhoDataModel);
        }
    }


}