using ELLPScore.Context.DB;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;

public interface IDisciplinaService
{
    IList<Disciplina> GetAllDisciplinas();
    bool CadastrarDisciplina(Disciplina disciplina, out string erro);
    bool AtualizarDisciplina(Disciplina disciplina, out string erro);
    bool ExcluirDisciplina(int id, out string erro);
}

public class DisciplinaService : IDisciplinaService
{
    private readonly ELLPScoreDBContext _context;

    public DisciplinaService(ELLPScoreDBContext context)
    {
        _context = context;
    }

    public IList<Disciplina> GetAllDisciplinas()
    {
        return _context.Disciplinas.ToList();
    }

    public bool CadastrarDisciplina(Disciplina disciplina, out string erro)
    {
        erro = string.Empty;
        try
        {
            var disciplinaJaExiste = _context.Disciplinas.Any(d => d.Nome == disciplina.Nome);

            if (disciplinaJaExiste)
            {
                erro = "Disciplina já cadastrada.";
                return false;
            }

            _context.Disciplinas.Add(disciplina);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    public bool AtualizarDisciplina(Disciplina disciplina, out string erro)
    {
        erro = string.Empty;
        try
        {
            var disciplinaJaExiste = _context.Disciplinas.Any(d => d.Nome == disciplina.Nome);

            if (disciplinaJaExiste)
            {
                erro = "Disciplina já cadastrada.";
                return false;
            }

            _context.Disciplinas.Update(disciplina);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    public bool ExcluirDisciplina(int id, out string erro)
    {
        erro = string.Empty;
        try
        {
            var disciplina = _context.Disciplinas.Find(id);
            if (disciplina == null)
            {
                erro = "Disciplina não encontrada.";
                return false;
            }

            _context.Disciplinas.Remove(disciplina);
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
