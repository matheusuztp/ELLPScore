using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Pages.Alunos
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoService _alunoService;

        public IndexModel(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [BindProperty]
        public AlunoInputModel Aluno { get; set; }

        public IList<Aluno> Alunos { get; set; }
        public IList<Turma> Turmas { get; set; }

        public async Task OnGetAsync()
        {
            Alunos = await _alunoService.GetAllAlunosAsync();
            Turmas = _alunoService.GetAllTurmas();
        }

        public async Task<IActionResult> OnPostCadastrarAlunoAsync()
        {
            if (!ModelState.IsValid)
            {
                Alunos = await _alunoService.GetAllAlunosAsync(); // Recarrega os alunos para exibir novamente na página
                Turmas = _alunoService.GetAllTurmas();            // Recarrega as turmas para exibir novamente na página
                return Page();
            }

            var novoAluno = new Aluno
            {
                Nome = Aluno.Nome,
                Email = Aluno.Email,
                Serie = Aluno.Serie,
                CPF = Aluno.CPF,
                Idade = Aluno.Idade,
                Matricula = Aluno.Matricula,
                Anotacoes = Aluno.Anotacoes,
                TurmaAlunos = new List<TurmaAluno>()
            };

            foreach (var turmaId in Aluno.TurmaIds)
            {
                novoAluno.TurmaAlunos.Add(new TurmaAluno { Aluno = novoAluno, TurmaID = turmaId });
            }

            await _alunoService.CadastrarAlunoAsync(novoAluno);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetCadastrarAlunoPartial()
        {
            return RedirectToPage("_CadastrarAlunoPartial", new AlunoInputModel() { Turmas = this.Turmas });
        }
    }
}
