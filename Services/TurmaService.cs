using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.EntityFrameworkCore;

public interface ITurmaService
{
    IList<Turma> GetAllTurmas();
    Task CadastrarTurmaAsync(Turma turma);
    Task<IList<Professor>> GetAllProfessoresAsync();
}

public class TurmaService : ITurmaService
{
    private readonly ELLPScoreDBContext _context;

    public TurmaService(ELLPScoreDBContext context)
    {
        _context = context;
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

    public async Task CadastrarTurmaAsync(Turma turma)
    {
        _context.Turmas.Add(turma);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<Professor>> GetAllProfessoresAsync()
    {
        return await _context.Professores.ToListAsync();
    }
}
