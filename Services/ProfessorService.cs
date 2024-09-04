using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public interface IProfessorService
{
    Task<IList<Professor>> GetAllProfessoresAsync();
    Task<IdentityResult> CadastrarProfessorAsync(Professor professor, string senha);
    Task<Professor> GetProfessorByIdAsync(int id);
    Task<IdentityResult> AtualizarProfessorAsync(Professor professor);
    bool ExcluirProfessor(Professor professor, out string erro);
    Task<IdentityResult> AlterarSenhaAsync(Professor professor, string novaSenha);
}


public class ProfessorService : IProfessorService
{
    private readonly UserManager<Professor> _userManager;
    private readonly ELLPScoreDBContext _context;

    public ProfessorService(UserManager<Professor> userManager, ELLPScoreDBContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IList<Professor>> GetAllProfessoresAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<Professor> GetProfessorByIdAsync(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IdentityResult> CadastrarProfessorAsync(Professor professor, string senha)
    {
        return await _userManager.CreateAsync(professor, senha);
    }

    public async Task<IdentityResult> AtualizarProfessorAsync(Professor professor)
    {
        var existingProfessor = await _userManager.FindByIdAsync(professor.Id.ToString());

        if (existingProfessor == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Professor não encontrado." });
        }

        existingProfessor.Email = professor.Email;
        existingProfessor.UserName = professor.Email; // Normalmente o UserName é o Email
        existingProfessor.IsAdmin = professor.IsAdmin;

        return await _userManager.UpdateAsync(existingProfessor);
    }

    public bool ExcluirProfessor(Professor professor, out string erro)
    {
        erro = string.Empty;
        var existingProfessor = _userManager.FindByIdAsync(professor.Id.ToString()).GetAwaiter().GetResult();

        if (existingProfessor == null)
        {
            erro = "Professor nao encontrado.";
            return false;
        }

        var professorTemTurma = _context.Turmas.Any(t => t.ProfessorID == professor.Id);
        if(professorTemTurma)
        {
            erro = "Nao e possivel excluir um professor com turmas cadastradas.";
            return false;
        }

        _userManager.DeleteAsync(existingProfessor).GetAwaiter().GetResult();
        return true; 
    }

    public async Task<IdentityResult> AlterarSenhaAsync(Professor professor, string novaSenha)
    {
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(professor);
        return await _userManager.ResetPasswordAsync(professor, resetToken, novaSenha);
    }
}