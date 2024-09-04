using ELLPScore.Domain.DTO;
using ELLPScore.Services;
using System.Collections.Generic;
using System.Linq;

namespace ELLPScore
{
    
    public interface IDesempenhoService
    {
        DesempenhoDataModel GetDesempenhoPorAluno(int alunoId);
    }
    public class DesempenhoService : IDesempenhoService
    {
        private readonly IAlunoService _alunoRepository;
        private readonly INotaService _notaRepository;

        public DesempenhoService(IAlunoService alunoRepository, INotaService notaRepository)
        {
            _alunoRepository = alunoRepository;
            _notaRepository = notaRepository;
        }

        public DesempenhoDataModel GetDesempenhoPorAluno(int alunoId)
        {
            var aluno = _alunoRepository.GetAlunoById(alunoId);
            if (aluno == null)
            {
                return null; // Ou você pode lançar uma exceção personalizada
            }

            // 1. Desempenho por Matéria
            var desempenhoPorMateria = _notaRepository.GetNotasByAlunoId(alunoId)
                .GroupBy(n => n.Disciplina)
                .Select(g => new DesempenhoPorDisciplina
                {
                    Disciplina = g.Key.Nome,
                    Nota = (int)g.Average(n => n.NotaValor)
                }).ToList();

            var alunosVsPeriodo = _notaRepository.GetNotasByPeriodo(aluno.TurmaID)
                .GroupBy(n => n.Periodo)
                .Select(g => new AlunosVsPeriodo
                {
                    Periodo = g.Key,
                    Nota = (int)g.Average(n => n.NotaValor)
                }).ToList();

            foreach(var periodo in alunosVsPeriodo)
            {
                if (Enum.TryParse(typeof(Periodos), periodo.Periodo, out var periodoEnum))
                {
                    var periodoDisplayName = ((Periodos)periodoEnum).GetDisplayName();
                    periodo.Periodo = periodoDisplayName;
                }
            }

            var aprovacoes = _notaRepository.GetNotasByAlunoId(alunoId)
                .GroupBy(n => n.Periodo)
                .Select(g => new Aprovacoes
                {
                    Periodo = g.Key,
                    Aprovado = g.Count(n => n.NotaValor >= 60) 
                }).ToList();

            return new DesempenhoDataModel
            {
                DesempenhoPorDisciplina = desempenhoPorMateria,
                AlunosVsPeriodo = alunosVsPeriodo,
                Aprovacoes = aprovacoes
            };
        }
    }
}
