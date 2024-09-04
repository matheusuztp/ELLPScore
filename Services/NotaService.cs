using ELLPScore.Context.DB;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore
{
    public interface INotaService
    {
        IList<Nota> GetAllNotas(int id = 0);
        Nota GetNotaById(int id);
        bool CadastrarNota(Nota nota, out string erro);
        bool AtualizarNota(Nota nota, out string erro);
        bool ExcluirNota(int id, out string erro);
        IEnumerable<Nota> GetNotasByAlunoId(int alunoId);
        IEnumerable<Nota> GetNotasByTurma(int turmaId);
        Task<int> GetTotalNotasAsync();
    }

    public class NotaService : INotaService
    {
        private readonly ELLPScoreDBContext _context;

        public NotaService(ELLPScoreDBContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalNotasAsync()
        {
            return await _context.Notas.CountAsync();
        }

        public IEnumerable<Nota> GetNotasByAlunoId(int alunoId)
        {
            return _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .Where(n => n.AlunoID == alunoId)
                .ToList();
        }

        // Retorna as notas de uma turma específica
        public IEnumerable<Nota> GetNotasByTurma(int turmaId)
        {
            return _context.Notas
                .Include(n => n.Aluno) 
                .Include(n => n.Turma)
                .Where(n => n.TurmaID == turmaId)
                .ToList();
        }

        public IList<Nota> GetAllNotas(int id = 0)
        {
            if (id > 0)
            {
                return _context.Notas
                    .Include(n => n.Aluno)
                    .Include(n => n.Disciplina)
                    .Include(n => n.Turma)
                    .Where(n => n.AlunoID == id)
                    .ToList();
            }

            return _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .Include(n => n.Turma)
                .ToList();
        }

        public Nota GetNotaById(int id)
        {
            return _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .Include(n => n.Turma)
                .FirstOrDefault(n => n.NotaID == id);
        }

        public bool CadastrarNota(Nota nota, out string erro)
        {
            erro = string.Empty;
            try
            {
                var jaExiste = _context.Notas.Any(n => n.AlunoID == nota.AlunoID
                                                    && n.DisciplinaID == nota.DisciplinaID
                                                    && n.Periodo == nota.Periodo);

                if(jaExiste)
                {
                    erro = "Nota já cadastrada nesta disciplina e periodo.";
                    return false;
                }

                _context.Notas.Add(nota);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public bool AtualizarNota(Nota nota, out string erro)
        {
            erro = string.Empty;
            try
            {
                _context.Notas.Update(nota);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public bool ExcluirNota(int id, out string erro)
        {
            erro = string.Empty;
            try
            {
                var nota = _context.Notas.Find(id);
                if (nota == null)
                {
                    erro = "Nota não encontrada.";
                    return false;
                }

                _context.Notas.Remove(nota);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }
    }
}
