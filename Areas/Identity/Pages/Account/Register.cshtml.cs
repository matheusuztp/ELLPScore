using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;

namespace ELLPScore.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Professor> _userManager;
        private readonly SignInManager<Professor> _signInManager;

        public RegisterModel(UserManager<Professor> userManager, SignInManager<Professor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório.")]
            [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 3)]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório.")]
            [EmailAddress(ErrorMessage = "O campo {0} deve ser um e-mail válido.")]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório.")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Senha")]
            [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var professor = new Professor { UserName = Input.Email, Email = Input.Email, Nome = Input.Nome, EmailConfirmed = true, NormalizedEmail = Input.Email };
             
                if(_userManager.Users.ToListAsync().GetAwaiter().GetResult().Count == 0)
                    professor.IsAdmin = true;
                
                var result = await _userManager.CreateAsync(professor, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(professor, isPersistent: false);
                    return RedirectToPage("RegisterConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
