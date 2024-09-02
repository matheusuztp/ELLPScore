using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ELLPScore.Domain;
using System.ComponentModel.DataAnnotations;

namespace ELLPScore.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Professor> _signInManager;

        public LoginModel(SignInManager<Professor> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório.")]
            [EmailAddress(ErrorMessage = "O campo {0} deve ser um e-mail válido.")]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Conta bloqueada temporariamente.");
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario e/ou senha incorretos.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
