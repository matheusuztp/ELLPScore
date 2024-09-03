using ELLPScore.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ELLPScore.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

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
        public string Turma { get; set; }
        public SelectList Periodos { get; set; }

        [BindProperty]
        public int AlunoSelecionadoId { get; set; }

        public void OnGet(int? alunoId = null)
        {
            RefreshData(alunoId);
            Periodos = new SelectList(Enum.GetValues(typeof(Periodos)).Cast<Periodos>().Select(p =>
                                        new SelectListItem(
                                            p.GetType().GetField(p.ToString())
                                            .GetCustomAttribute<DisplayAttribute>()?.Name ?? p.ToString(),
                                            p.ToString())),
                                        "Value", "Text");
        }

        public void RefreshData(int? alunoId = null)
        {
            Periodos = new SelectList(Enum.GetValues(typeof(Periodos)).Cast<Periodos>().Select(p =>
                                        new SelectListItem(
                                            p.GetType().GetField(p.ToString())
                                            .GetCustomAttribute<DisplayAttribute>()?.Name ?? p.ToString(),
                                            p.ToString())),
                                        "Value", "Text");

            Disciplinas = new SelectList(_disciplinaService.GetAllDisciplinas(), "DisciplinaID", "Nome");
            Turmas = new SelectList(_turmaService.GetAllTurmas(), "TurmaID", "CodigoOuNome");
            Alunos = new SelectList(_alunoService.GetAllAlunos(), "AlunoID", "Nome");

            if (alunoId.HasValue)
            {
                AlunoSelecionadoId = alunoId.Value;
                var aluno = _alunoService.GetAlunoById(AlunoSelecionadoId);
                Serie = aluno?.Serie ?? string.Empty;
                Turma = aluno?.Turma.CodigoOuNome ?? string.Empty;
                Notas = _notaService.GetAllNotas(AlunoSelecionadoId) ?? new List<Nota>();
            }
            else if (Alunos.Any())
            {
                AlunoSelecionadoId = int.Parse(Alunos.First().Value);
                var aluno = _alunoService.GetAlunoById(AlunoSelecionadoId);
                Serie = aluno?.Serie ?? string.Empty;
                Turma = aluno?.Turma.CodigoOuNome ?? string.Empty;
                Notas = _notaService.GetAllNotas(AlunoSelecionadoId) ?? new List<Nota>();
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

            decimal valorNota = 0;
            if (input.ValorNota != null && input.ValorNota.Contains('.'))
                valorNota = decimal.Parse(input.ValorNota.Replace('.', ','));

            if(valorNota > 100 || valorNota < 0)
            {
                erro += "Nota deve estar entre 0 e 100." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(erro))
            {
                ModelState.AddModelError(string.Empty, erro);
                RefreshData(input.AlunoID);
                return Page();
            }

            var nota = new Nota
            {
                AlunoID = input.AlunoID,
                DisciplinaID = input.DisciplinaID,
                TurmaID = input.TurmaID,
                Periodo = input.Periodo,
                NotaValor = valorNota,
                Serie = input.Serie
            };

            if (!_notaService.CadastrarNota(nota, out string erroCad))
            {
                erro += Environment.NewLine + erroCad;
                ModelState.AddModelError(string.Empty, erro);
            }

            RefreshData(input.AlunoID);
            return Page();
        }

        public IActionResult OnGetGetSeriePorAluno(int alunoId)
        {
            var aluno = _alunoService.GetAllAlunos().FirstOrDefault(a => a.AlunoID == alunoId);
            if (aluno != null)
            {
                RefreshData(alunoId);
                var data = new string[] { aluno.Serie, aluno.Turma?.CodigoOuNome ?? string.Empty };
                return new JsonResult(data);
            }
            return new JsonResult(string.Empty);
        }

        public IActionResult OnPostDeleteNotaAsync(int id)
        {
            var alunoId = _notaService.GetAllNotas().FirstOrDefault(a => a.NotaID == id).AlunoID;
            if (!_notaService.ExcluirNota(id, out string erro))
            {
                ModelState.AddModelError(string.Empty, erro);
            }

            RefreshData(alunoId);
            return Page();
        }

        public PartialViewResult OnGetNotaPartial(int alunoId)
        {
            Notas = _notaService.GetAllNotas(alunoId);
            return Partial("_NotaPartial", Notas);
        }
    }

    public class NotaInputModel
    {
        public int AlunoID { get; set; }
        public int DisciplinaID { get; set; }
        public int TurmaID { get; set; }
        public string? Periodo { get; set; }

        [Required(ErrorMessage = "A nota é obrigatória.")]
        public string ValorNota { get; set; }

        [Required(ErrorMessage = "A serie é obrigatória.")]
        public string Serie { get; set; }
    }
}

