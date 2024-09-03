using ELLPScore.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ELLPScore.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

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
            RefreshData(alunoId);
        }

        public void RefreshData(int? alunoId = null)
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
            string erro = string.Empty;

            if (input.AlunoID == 0)
                erro += "Aluno deve estar selecionado." + Environment.NewLine;

            if(input.DisciplinaID == 0)
                erro += "Disciplina deve estar selecionada." + Environment.NewLine;

            if(input.TurmaID == 0)
                erro += "Turma deve estar selecionada." + Environment.NewLine;

            if(!string.IsNullOrEmpty(erro))
            {
                ModelState.AddModelError(string.Empty, erro);
                RefreshData();
                return Page();
            }

            var nota = new Nota
            {
                AlunoID = input.AlunoID,
                DisciplinaID = input.DisciplinaID,
                TurmaID = input.TurmaID,
                Periodo = input.Periodo,
                NotaValor = input.ValorNota,
                Serie = input.Serie

            };

            if (!_notaService.CadastrarNota(nota, out string erroCad))
                erro += Environment.NewLine + erroCad;

            ModelState.AddModelError(string.Empty, erro);
            return Page();
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

        [StringLength(50, ErrorMessage = "O per�odo n�o pode exceder 50 caracteres.")]
        public string? Periodo { get; set; }

        [Range(1.0, 120.0, ErrorMessage = "A nota deve ser um n�mero entre 0 e 100.")]
        [Required(ErrorMessage = "A nota � obrigat�ria.")]
        public decimal ValorNota { get; set; }

        [Required(ErrorMessage = "A serie � obrigat�ria.")]
        public string Serie { get; set; }
    }
}
