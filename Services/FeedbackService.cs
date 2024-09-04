using ELLPScore.Context.DB;
using ELLPScore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IFeedbackService
{
    IList<Feedback> GetAllFeedbacks();
    Feedback GetFeedbackById(int id);
    bool CadastrarFeedback(Feedback feedback, out string erro);
    bool AtualizarFeedback(Feedback feedback, out string erro);
    bool ExcluirFeedback(int id, out string erro);
    Task<int> GetTotalFeedbacksAsync();
}

public class FeedbackService : IFeedbackService
{
    private readonly ELLPScoreDBContext _context;

    public FeedbackService(ELLPScoreDBContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalFeedbacksAsync()
    {
        return await _context.Feedbacks.CountAsync();
    }

    // Obter todos os feedbacks
    public IList<Feedback> GetAllFeedbacks()
    {
        return _context.Feedbacks.Include(f => f.Aluno).ToList();
    }

    // Obter feedback por ID
    public Feedback GetFeedbackById(int id)
    {
        return _context.Feedbacks.Include(f => f.Aluno)
                                 .FirstOrDefault(f => f.ID == id);
    }

    // Cadastrar um novo feedback
    public bool CadastrarFeedback(Feedback feedback, out string erro)
    {
        erro = string.Empty;
        try
        {
            // Validação adicional pode ser feita aqui, se necessário

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    // Atualizar um feedback existente
    public bool AtualizarFeedback(Feedback feedback, out string erro)
    {
        erro = string.Empty;
        try
        {
            var feedbackExistente = _context.Feedbacks.Find(feedback.ID);
            if (feedbackExistente == null)
            {
                erro = "Feedback não encontrado.";
                return false;
            }

            _context.Entry(feedbackExistente).CurrentValues.SetValues(feedback);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    // Excluir um feedback
    public bool ExcluirFeedback(int id, out string erro)
    {
        erro = string.Empty;
        try
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback == null)
            {
                erro = "Feedback não encontrado.";
                return false;
            }

            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }
}
