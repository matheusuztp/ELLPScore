using ELLPScore.Context.DB;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;

public interface IDisciplinaService
{
    Task<IList<Disciplina>> GetAllDisciplinaAsync();
    Task CadastrardisciplinaAsync(Disciplina disciplina);
}

public class DisciplinaService : IDisciplinaService
{
    private readonly ELLPScoreDBContext _context;

    public DisciplinaService(ELLPScoreDBContext context)
    {
        _context = context;
    }

    public async Task<IList<Disciplina>> GetAllDisciplinaAsync()
    {
        var Disciplinas = await _context.Disciplinas.ToListAsync();
        return Disciplinas;
    }

    public async Task CadastrardisciplinaAsync(Disciplina disciplina)
    {
        _context.Disciplinas.Add(disciplina);
        await _context.SaveChangesAsync();
    }
}
