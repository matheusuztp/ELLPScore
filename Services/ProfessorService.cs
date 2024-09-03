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
    Task<IdentityResult> ExcluirProfessorAsync(Professor professor);
    Task<IdentityResult> AlterarSenhaAsync(Professor professor, string novaSenha);
}


public class ProfessorService : IProfessorService
{
    private readonly UserManager<Professor> _userManager;

    public ProfessorService(UserManager<Professor> userManager)
    {
        _userManager = userManager;
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

    public async Task<IdentityResult> ExcluirProfessorAsync(Professor professor)
    {
        var existingProfessor = await _userManager.FindByIdAsync(professor.Id.ToString());

        if (existingProfessor == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Professor não encontrado." });
        }

        return await _userManager.DeleteAsync(existingProfessor);
    }

    public async Task<IdentityResult> AlterarSenhaAsync(Professor professor, string novaSenha)
    {
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(professor);
        return await _userManager.ResetPasswordAsync(professor, resetToken, novaSenha);
    }
}