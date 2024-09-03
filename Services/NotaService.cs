using ELLPScore.Context.DB;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Services
{
    public interface INotaService
    {
        IList<Nota> GetAllNotas(int id = 0);
        Nota GetNotaById(int id);
        bool CadastrarNota(Nota nota, out string erro);
        bool AtualizarNota(Nota nota, out string erro);
        bool ExcluirNota(int id, out string erro);
    }

    public class NotaService : INotaService
    {
        private readonly ELLPScoreDBContext _context;

        public NotaService(ELLPScoreDBContext context)
        {
            _context = context;
        }

        public IList<Nota> GetAllNotas(int id = 0)
        {
            if (id > 0)
            {
                return _context.Notas
                    .Include(n => n.Aluno)
                    .Include(n => n.Disciplina)
                    .Include(n => n.Turma)
                    .Where(n => n.NotaID == id)
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
