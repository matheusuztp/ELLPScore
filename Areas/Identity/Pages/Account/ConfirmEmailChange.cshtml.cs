using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ELLPScore.Domain;

namespace ELLPScore.Areas.Identity.Pages.Account
{
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<Professor> _userManager;
        private readonly SignInManager<Professor> _signInManager;

        public ConfirmEmailChangeModel(UserManager<Professor> userManager, SignInManager<Professor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{userId}'.");
            }

            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                StatusMessage = "Erro ao alterar e-mail.";
                return Page();
            }

            // Atualiza o nome de usuário
            var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Erro ao alterar nome de usuário.";
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Obrigado por confirmar a alteração de seu e-mail.";
            return Page();
        }
    }
}
