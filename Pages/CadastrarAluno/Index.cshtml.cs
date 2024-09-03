using ELLPScore.Domain.DTO;
using ELLPScore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.CadastrarAluno
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoService _alunoService;
        private readonly ITurmaService _turmaService;

        [BindProperty]
        public AlunoInputModel AlunoInput { get; set; }
        public IList<Turma> Turmas { get; set; }


        public IndexModel(IAlunoService alunoService, ITurmaService turmaService)
        {
            _alunoService = alunoService;
            _turmaService = turmaService;
        }

        public void OnGetAsync()
        {
            AlunoInput = new AlunoInputModel();
            Turmas = _turmaService.GetAllTurmas();
            ViewData["Dados"] = new SelectList(Turmas, "TurmaID", "CodigoOuNome");
        }

        public IActionResult OnGetEdit(int id)
        {
            var aluno = _alunoService.GetAllAlunos().FirstOrDefault(t => t.AlunoID == id);

            if (aluno == null)
            {
                RefreshData();
                return Page();
            }

            AlunoInput = new AlunoInputModel
            {
                Nome = aluno.Nome,
                CPF = aluno.CPF,
                Email = aluno.Email,
                Matricula = aluno.Matricula,
                Serie = aluno.Serie,
                Idade = aluno.Idade,
                Anotacoes = aluno.Anotacoes,
                TurmaId = aluno.TurmaID
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

            var alunoToUpdate = _alunoService.GetAllAlunos().FirstOrDefault(t => t.AlunoID == id);

            if (alunoToUpdate == null)
            {
                return NotFound();
            }

            alunoToUpdate.Nome = AlunoInput.Nome;
            alunoToUpdate.CPF = AlunoInput.CPF;
            alunoToUpdate.Email = AlunoInput.Email;
            alunoToUpdate.Matricula = AlunoInput.Matricula;
            alunoToUpdate.Serie = AlunoInput.Serie;
            alunoToUpdate.Idade = AlunoInput.Idade;
            alunoToUpdate.Anotacoes = AlunoInput.Anotacoes;
            alunoToUpdate.TurmaID = AlunoInput.TurmaId;


            if (_alunoService.AtualizarAluno(alunoToUpdate, out var erro))
            {
                return RedirectToPage("/Alunos/Index");
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

            var novoAluno = new Aluno
            {
                Nome = AlunoInput.Nome,
                CPF = AlunoInput.CPF,
                Email = AlunoInput.Email,
                Matricula = AlunoInput.Matricula,
                Serie = AlunoInput.Serie,
                Idade = AlunoInput.Idade,
                Anotacoes = AlunoInput.Anotacoes,
                TurmaID = AlunoInput.TurmaId
            };

            if (!_alunoService.CadastrarAluno(novoAluno, out string erro))
            {
                ModelState.AddModelError(string.Empty, erro);
                RefreshData();
                return Page();
            }



            return RedirectToPage("/Alunos/Index");
        }



        public void RefreshData()
        {
            Turmas = _turmaService.GetAllTurmas();
            ViewData["Dados"] = new SelectList(Turmas, "TurmaID", "CodigoOuNome");
        }
    }
}
