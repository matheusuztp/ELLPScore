using ELLPScore.Context.DB;
using ELLPScore.Domain;
using ELLPScore.Domain.DTO;
using Microsoft.EntityFrameworkCore;

public interface IAlunoService
{
    IList<Aluno> GetAllAlunos();
    Aluno GetAlunoById(int id);
    bool CadastrarAluno(Aluno aluno, out string erro);
    bool AtualizarAluno(Aluno aluno, out string erro);
    bool ExcluirAluno(int id, out string erro);
    IList<Turma> GetAllTurmas();
}

public class AlunoService : IAlunoService
{
    private readonly ELLPScoreDBContext _context;

    public AlunoService(ELLPScoreDBContext context)
    {
        _context = context;
    }

    // Obter todos os alunos
    public IList<Aluno> GetAllAlunos()
    {
        return _context.Alunos.ToList();
    }

    // Obter aluno por ID
    public Aluno GetAlunoById(int id)
    {
        return _context.Alunos.Include(a => a.Turma)
                               .FirstOrDefault(a => a.AlunoID == id);
    }

    // Cadastrar um novo aluno
    public bool CadastrarAluno(Aluno aluno, out string erro)
    {
        erro = string.Empty;
        try
        {
            var alunoJaExiste = _context.Alunos.Any(a => a.CPF == aluno.CPF
                                                      || a.Email == aluno.Email
                                                      || a.Matricula == aluno.Matricula);
            if (alunoJaExiste)
            {
                erro = "Aluno já cadastrado.";
                return false;
            }

            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    // Atualizar um aluno existente
    public bool AtualizarAluno(Aluno aluno, out string erro)
    {
        erro = string.Empty;
        try
        {
            var alunoJaExiste = _context.Alunos.Any(a => a.CPF == aluno.CPF
                                                      || a.Email == aluno.Email
                                                      || a.Matricula == aluno.Matricula);
            if (alunoJaExiste)
            {
                erro = "Aluno já cadastrado.";
                return false;
            }

            _context.Entry(alunoJaExiste).CurrentValues.SetValues(aluno);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }

    // Excluir um aluno
    public bool ExcluirAluno(int id, out string erro)
    {
        erro = string.Empty;
        try
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
            {
                erro = "Aluno não encontrado.";
                return false;
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return false;
        }
    }


    // Obter todas as turmas disponíveis
    public IList<Turma> GetAllTurmas()
    {
        return _context.Turmas.ToList();
    }
}
