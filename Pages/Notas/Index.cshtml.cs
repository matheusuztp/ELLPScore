using ELLPScore.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ELLPScore.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ELLPScore.Pages.Notas
{
    public class IndexModel : PageModel
    {
        private readonly INotaService _notaService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly ITurmaService _turmaService;
        private readonly IAlunoService _alunoService;

        public IndexModel(INotaService notaService, IDisciplinaService disciplinaService, ITurmaService turmaService, IAlunoService alunoService)
        {
            _notaService = notaService;
            _disciplinaService = disciplinaService;
            _turmaService = turmaService;
            _alunoService = alunoService;
        }

        public IList<Nota> Notas { get; set; }
        public SelectList Disciplinas { get; set; }
        public SelectList Turmas { get; set; }
        public SelectList Alunos { get; set; }
        public string Serie { get; set; }

        [BindProperty]
        public int AlunoSelecionadoId { get; set; }

        public void OnGet(int? alunoId = null)
        {
            Notas = _notaService.GetAllNotas((alunoId.HasValue ? alunoId.Value : 0)) ?? new List<Nota>();
            Disciplinas = new SelectList(_disciplinaService.GetAllDisciplinas(), "DisciplinaID", "Nome");
            Turmas = new SelectList(_turmaService.GetAllTurmas(), "TurmaID", "CodigoOuNome");
            Alunos = new SelectList(_alunoService.GetAllAlunos(), "AlunoID", "Nome");

            if (alunoId.HasValue)
            {
                AlunoSelecionadoId = alunoId.Value;
                var aluno = _alunoService.GetAlunoById(AlunoSelecionadoId);
                Serie = aluno?.Serie ?? string.Empty;
            }
            else if (Alunos.Any())
            {
                AlunoSelecionadoId = int.Parse(Alunos.First().Value);
                var aluno = _alunoService.GetAlunoById(AlunoSelecionadoId);
                Serie = aluno?.Serie ?? string.Empty;
            }
        }

        public IActionResult OnPost([FromForm] NotaInputModel input)
        {
            // Suponha que NotaInputModel é um modelo que captura os campos do formulário
            var nota = new Nota
            {
                AlunoID = input.AlunoID,
                DisciplinaID = input.DisciplinaID,
                TurmaID = input.TurmaID,
                Periodo = input.Periodo,
                NotaValor = input.ValorNota,
                Serie = input.Serie

            };

            if (_notaService.CadastrarNota(nota, out var erro))
            {
                return new JsonResult(new { success = true });
            }

            return null; // Ou algum tipo de erro
        }

        public IActionResult OnGetGetSeriePorAluno(int alunoId)
        {
            var aluno = _alunoService.GetAllAlunos().FirstOrDefault(a => a.AlunoID == alunoId);
            if (aluno != null)
            {
                return new JsonResult(aluno.Serie);
            }

            return new JsonResult(string.Empty);
        }
    }

    public class NotaInputModel
    {
        public int AlunoID { get; set; }
        public int DisciplinaID { get; set; }
        public int TurmaID { get; set; }
        public string Periodo { get; set; }
        public decimal ValorNota { get; set; }
        public string Serie { get; set; }
    }
}
