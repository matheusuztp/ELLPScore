using ELLPScore.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ELLPScore.Pages.Sair
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<Professor> _signInManager;
        public IndexModel(SignInManager<Professor> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
