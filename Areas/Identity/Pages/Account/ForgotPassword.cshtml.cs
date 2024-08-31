using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELLPScore.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Implementar l�gica de recupera��o de senha
                // Enviar email com o link de recupera��o de senha
            }

            return Page();
        }
    }
}
