using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public IList<AlunoViewModel> Alunos { get; set; }

        public void OnGet()
        {
            Alunos = _alunoService.GetAllAlunos();
        }

        public async Task<IActionResult> OnPostCadastrarAlunoAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var novoAluno = new Aluno
            {
                Nome = Aluno.Nome,
                Email = Aluno.Email,
                Serie = Aluno.Serie,
                CPF = Aluno.CPF,
                Idade = Aluno.Idade,
                Turma = Aluno.Turma,
                Matricula = Aluno.Matricula,
                Anotacoes = Aluno.Anotacoes
            };

            await _alunoService.CadastrarAlunoAsync(novoAluno);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetCadastrarAlunoPartial()
        {
            return RedirectToPage("_CadastrarAlunoPartial", new AlunoInputModel());
        }
    }
}
