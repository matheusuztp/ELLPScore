using ELLPScore.Domain;
using ELLPScore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ELLPScore.Pages.Notas
{
    public class _NotasTablePartialModel : PageModel
    {
        private readonly INotaService _notaService;

        public _NotasTablePartialModel(INotaService notaService)
        {
            _notaService = notaService;
        }

        public List<Nota> Notas { get; set; }

        public void OnGet(int alunoId)
        {
            // Carrega todas as notas do aluno
            Notas = _notaService.GetAllNotas().Where(n => n.AlunoID == alunoId).ToList();
        }
    }
}
