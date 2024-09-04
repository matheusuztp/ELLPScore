using ELLPScore.Context.DB;
using ELLPScore.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore
{
    public interface IRelatorioService
    {
        Task<List<RelatoriosViewModel>> FiltrarRelatoriosAsync(Filtros filtros);
    }


    public class RelatorioService : IRelatorioService
    {
        private readonly ELLPScoreDBContext _context;

        public RelatorioService(ELLPScoreDBContext context)
        {
            _context = context;
        }

        public async Task<List<RelatoriosViewModel>> FiltrarRelatoriosAsync(Filtros filtros)
        {
            var result = from notas in _context.Notas
                         join alunos in _context.Alunos on notas.AlunoID equals alunos.AlunoID
                         join turmas in _context.Turmas on notas.TurmaID equals turmas.TurmaID
                         join disciplinas in _context.Disciplinas on notas.DisciplinaID equals disciplinas.DisciplinaID
                         join professores in _context.Professores on turmas.ProfessorID equals professores.Id
                         where (filtros.AlunoId == 0 || notas.AlunoID == filtros.AlunoId)
                         && (filtros.TurmaId == 0 || notas.TurmaID == filtros.TurmaId)
                         && (filtros.DisciplinaId == 0 || notas.DisciplinaID == filtros.DisciplinaId)
                         && (filtros.ProfessorId == 0 || turmas.ProfessorID == filtros.ProfessorId)
                         select new RelatoriosViewModel
                         {
                             NomeAluno = alunos.Nome,
                             Serie = alunos.Serie,
                             Turma = turmas.CodigoOuNome,
                             Disciplina = disciplinas.Nome,
                             Periodo = notas.Periodo,
                             Nota = notas.NotaValor
                         };

            return result.ToList();
        }
    }
}
