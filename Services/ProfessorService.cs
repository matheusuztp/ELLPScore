using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public interface IProfessorService
{
    Task<IList<Professor>> GetAllProfessorAsync();
    Task<IdentityResult> CadastrarProfessorAsync(Professor professor, string senha);
}

public class ProfessorService : IProfessorService
{
    private readonly ELLPScoreDBContext _context;
    private readonly UserManager<Professor> _userManager;

    public ProfessorService(ELLPScoreDBContext context, UserManager<Professor> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IList<Professor>> GetAllProfessorAsync()
    {
        return await _context.Professores.ToListAsync();
    }

    //TODO USAR USERMANAGER PARA CRIAR USUÁRIO
    public async Task<IdentityResult> CadastrarProfessorAsync(Professor professor, string senha)
    {
        return await _userManager.CreateAsync(professor, senha);
    }
}
