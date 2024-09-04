using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.EntityFrameworkCore;

public interface ITurmaService
{
    IList<Turma> GetAllTurmas();
    bool CadastrarTurma(Turma turma, out string erro);
    bool AtualizarTurma(Turma turma, out string erro);
    bool ExcluirTurma(int id, out string erro);
    Task<IList<Professor>> GetAllProfessoresAsync();
    Task<int> GetTotalTurmasAsync();
}

public class TurmaService : ITurmaService
{
    private readonly ELLPScoreDBContext _context;

    public TurmaService(ELLPScoreDBContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalTurmasAsync()
    {
        return await _context.Turmas.CountAsync();
    }

    public IList<Turma> GetAllTurmas()
    {
        var turmas = _context.Turmas.ToList();

        foreach (var turma in turmas)
        {
            _context.Entry(turma)
                    .Reference(t => t.Professor)
                    .Load();
        }

        return turmas;
    }

    public bool CadastrarTurma(Turma turma, out string erro)
    {
        erro = string.Empty;
        try
        {
            var turmaJaExiste = _context.Turmas.Any(t => t.CodigoOuNome == turma.CodigoOuNome
                                                             && t.ProfessorID == turma.ProfessorID);

            if (turmaJaExiste)
            {
                erro = "Turma já cadastrada.";
                return false;
            }

            _context.Turmas.Add(turma);
            _context.SaveChanges();
            return true;

        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    public bool AtualizarTurma(Turma turma, out string erro)
    {
        erro = string.Empty;
        try
        {
            var turmaJaExiste = _context.Turmas.Any(t => t.CodigoOuNome == turma.CodigoOuNome
                                                             && t.ProfessorID == turma.ProfessorID);

            if (turmaJaExiste)
            {
                erro = "Turma já cadastrada.";
                return false;
            }

            _context.Turmas.Update(turma);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    public bool ExcluirTurma(int id, out string erro)
    {
        erro = string.Empty;
        try
        {
            var turma = _context.Turmas.Find(id);
            if (turma == null)
            {
                erro = "Turma nao encontrada.";
                return false;
            }

            if(_context.Alunos.Any(a => a.TurmaID == id))
            {
                erro = "Nao e possivel excluir uma turma com alunos matriculados.";
                return false;
            }

            _context.Turmas.Remove(turma);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    public async Task<IList<Professor>> GetAllProfessoresAsync()
    {
        return await _context.Professores.ToListAsync();
    }
}
