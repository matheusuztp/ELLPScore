using ELLPScore.Context.DB;
using ELLPScore.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore
{
    public interface IRelatorioService
    {
        Task<List<RelatoriosViewModel>> FiltrarRelatoriosAsync(string tipoRelatorio, string search);
    }


    public class RelatorioService : IRelatorioService
    {
        private readonly ELLPScoreDBContext _context;

        public RelatorioService(ELLPScoreDBContext context)
        {
            _context = context;
        }

        public async Task<List<RelatoriosViewModel>> FiltrarRelatoriosAsync(string tipoRelatorio, string search)
        {
            var query = _context.Alunos.AsQueryable();
            var notas = _context.Notas.AsQueryable();

            // Adiciona filtros conforme o tipo de relatório selecionado
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Nome.Contains(search));
                
            }

            var result = await query.Select(a => new RelatoriosViewModel
            {
                Turma = a.Turma.CodigoOuNome,
                Serie = a.Serie,
                NomeAluno = a.Nome,
                Disciplina = a.Nome,
                Periodo = a.Nome,
                Nota = 0
            }).ToListAsync();

            return result;
        }
    }
}
