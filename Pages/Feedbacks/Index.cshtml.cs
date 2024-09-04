using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.UI.Services;
using ELLPScore.Domain;
using System.Text.Encodings.Web;

namespace ELLPScore.Pages.Feedbacks
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoService _alunoService;
        private readonly IFeedbackService _feedbackService;
        private readonly IEmailSender _emailSender;

        public IndexModel(IAlunoService alunoService, IFeedbackService feedbackService, IEmailSender emailSender)
        {
            _alunoService = alunoService;
            _feedbackService = feedbackService;
            _emailSender = emailSender;
        }

        [BindProperty]
        public int AlunoID { get; set; }

        [BindProperty]
        public string Matricula { get; set; }

        [BindProperty]
        public string Feedback { get; set; }

        public SelectList Alunos { get; set; }

        public void OnGet()
        {
            var alunos = _alunoService.GetAllAlunos();
            Alunos = new SelectList(alunos, "AlunoID", "Nome");
            Matricula = alunos.First().Matricula;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var feedback = new Feedback
            {
                AlunoID = AlunoID,
                FeedbackTexto = Feedback
            };

            if (!_feedbackService.CadastrarFeedback(feedback, out string erro))
            {
                ModelState.AddModelError(string.Empty, erro);
                return Page();
            }

            // Envia o feedback por e-mail
            var aluno = _alunoService.GetAlunoById(AlunoID);
            if (aluno != null)
            {
                var emailBody = $"Olá {aluno.Nome},<br/><br/>Você recebeu o seguinte feedback do professor:<br/><br/>{Feedback}<br/><br/>Atenciosamente,<br/>ELLP Score.";
                await _emailSender.SendEmailAsync(
                    aluno.Email, 
                    "Feedback do Professor",
                    emailBody);
            }

            return RedirectToPage("FeedbackSuccess");
        }

        public IActionResult OnGetMatriculaPorAluno(int alunoId)
        {
            var aluno = _alunoService.GetAllAlunos().FirstOrDefault(a => a.AlunoID == alunoId);
            if (aluno != null)
            {
                return new JsonResult(aluno.Matricula); // Retorna a matrícula diretamente
            }
            return new JsonResult(string.Empty);
        }
    }
}
