using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELLPScore.Pages.Relatorios
{
    public class IndexModel : PageModel
    {
        private readonly IRelatorioService _relatorioService;
        private readonly IAlunoService _alunoService;
        private readonly ITurmaService _turmaService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly IProfessorService _professorService;

        public IndexModel(IRelatorioService relatorioService, IAlunoService alunoService, ITurmaService turmaService, IDisciplinaService disciplinaService, IProfessorService professorService)
        {
            _relatorioService = relatorioService;
            _alunoService = alunoService;
            _turmaService = turmaService;
            _disciplinaService = disciplinaService;
            _professorService = professorService;
        }

        public List<RelatoriosViewModel> Relatorios { get; set; }
        public List<SelectListItem> Alunos { get; set; }
        public List<SelectListItem> Turmas { get; set; }
        public List<SelectListItem> Disciplinas { get; set; }
        public List<SelectListItem> Professores { get; set; }

        [BindProperty(SupportsGet = true)]
        public Filtros Filtros { get; set; }

        

        public async Task OnGetAsync()
        {
            Relatorios = new List<RelatoriosViewModel>();

            Alunos = (_alunoService.GetAllAlunos())
                .Select(a => new SelectListItem { Value = a.AlunoID.ToString(), Text = a.Nome })
                .ToList();

            Turmas = (_turmaService.GetAllTurmas())
                .Select(t => new SelectListItem { Value = t.TurmaID.ToString(), Text = t.CodigoOuNome })
                .ToList();

            Disciplinas = (_disciplinaService.GetAllDisciplinas())
                .Select(d => new SelectListItem { Value = d.DisciplinaID.ToString(), Text = d.Nome })
                .ToList();

            Professores = (await _professorService.GetAllProfessoresAsync())
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome })
                .ToList();
        }

        public async Task<PartialViewResult> OnGetFiltrarRelatoriosAsync()
        {
            Relatorios = await _relatorioService.FiltrarRelatoriosAsync(Filtros);
            return Partial("_RelatoriosPartial", Relatorios);
        }
    }
}
