using ELLPScore.Domain.DTO;
using ELLPScore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ELLPScore.Pages.CadastrarProfessor
{
    public class IndexModel : PageModel
    {
        private readonly IProfessorService _professorService;

        [BindProperty]
        public ProfessorInputModel ProfessorInput { get; set; }

        public IndexModel(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        public void OnGet()
        {
            ProfessorInput = new ProfessorInputModel();
        }

        public IActionResult OnGetEdit(int id)
        {
            var professor = _professorService.GetProfessorByIdAsync(id).Result;

            if (professor == null)
            {
                return NotFound();
            }

            ProfessorInput = new ProfessorInputModel
            {
                Nome = professor.Nome,
                Email = professor.Email,
                Id = professor.Id,
                IsAdmin = professor.IsAdmin,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostEdit(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var professorToUpdate = await _professorService.GetProfessorByIdAsync(id);

            if (professorToUpdate == null)
            {
                return NotFound();
            }

            professorToUpdate.Nome = ProfessorInput.Nome;
            professorToUpdate.Email = ProfessorInput.Email;

            if (!string.IsNullOrEmpty(ProfessorInput.Password))
            {
                var result = await _professorService.AlterarSenhaAsync(professorToUpdate, ProfessorInput.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            var updateResult = await _professorService.AtualizarProfessorAsync(professorToUpdate);
            if (updateResult.Succeeded)
                return RedirectToPage("/Professores/Index");

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var novoProfessor = new Professor
            {
                Nome = ProfessorInput.Nome,
                Email = ProfessorInput.Email,
                UserName = ProfessorInput.Email,
            };

            var result = await _professorService.CadastrarProfessorAsync(novoProfessor, ProfessorInput.Password);

            if (result.Succeeded)
                return RedirectToPage("/Professores/Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
