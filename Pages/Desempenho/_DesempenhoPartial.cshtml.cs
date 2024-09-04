using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Pages.Desempenho
{
    public class _DesempenhoPartialModel : PageModel
    {
        public void OnGet()
        {
        }
        public class DesempenhoDataModel
        {
            public List<DesempenhoPorMateria> DesempenhoPorMateria { get; set; }
            public List<AlunosVsNotas> AlunosVsNotas { get; set; }
            public List<Aprovacoes> Aprovacoes { get; set; }
        }

        public class DesempenhoPorMateria
        {
            public string Materia { get; set; }
            public int Nota { get; set; }
        }

        public class AlunosVsNotas
        {
            public string Aluno { get; set; }
            public int Nota { get; set; }
        }

        public class Aprovacoes
        {
            public string Periodo { get; set; }
            public int Aprovado { get; set; }
        }

    }
}
