using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ELLPScore
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoService _alunoService;
        private readonly INotaService _notasService;
        private readonly IFeedbackService _feedbackService;
        private readonly ITurmaService _turmaService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly IProfessorService _professorService;

        public int TotalAlunos { get; set; }
        public int TotalNotas { get; set; }
        public int TotalFeedbacks { get; set; }
        public int TotalTurmas { get; set; }
        public int TotalDisciplinas { get; set; }
        public int TotalProfessores { get; set; }

        public IndexModel(
            IAlunoService alunoService,
            INotaService notasService,
            IFeedbackService feedbackService,
            ITurmaService turmaService,
            IDisciplinaService disciplinaService,
            IProfessorService professorService)
        {
            _alunoService = alunoService;
            _notasService = notasService;
            _feedbackService = feedbackService;
            _turmaService = turmaService;
            _disciplinaService = disciplinaService;
            _professorService = professorService;
        }

        public async Task OnGetAsync()
        {
            TotalAlunos = await _alunoService.GetTotalAlunosAsync();
            TotalNotas = await _notasService.GetTotalNotasAsync();
            TotalFeedbacks = await _feedbackService.GetTotalFeedbacksAsync();
            TotalTurmas = await _turmaService.GetTotalTurmasAsync();
            TotalDisciplinas = await _disciplinaService.GetTotalDisciplinasAsync();
            TotalProfessores = await _professorService.GetTotalProfessoresAsync();
        }
    }
}
