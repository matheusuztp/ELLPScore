using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.EntityFrameworkCore;

public interface IAlunoService
{
    Task<IList<Aluno>> GetAllAlunosAsync();
    Task CadastrarAlunoAsync(Aluno aluno);
    IList<Turma> GetAllTurmas(); 
}

public class AlunoService : IAlunoService
{
    private readonly ELLPScoreDBContext _context;

    public AlunoService(ELLPScoreDBContext context)
    {
        _context = context;
    }

    public async Task<IList<Aluno>> GetAllAlunosAsync()
    {
        return await _context.Alunos.ToListAsync();
    }

    public async Task CadastrarAlunoAsync(Aluno aluno)
    {
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
    }

    public IList<Turma> GetAllTurmas()
    {
        return _context.Turmas.ToList();
    }
}
