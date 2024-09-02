using ELLPScore.Domain;
using ELLPScore.Domain.DTO;

public interface IAlunoService
{
    IList<AlunoViewModel> GetAllAlunos();
    Task CadastrarAlunoAsync(Aluno aluno);
}

public class AlunoService : IAlunoService
{
    private readonly List<Aluno> _alunos = new List<Aluno>();

    public IList<AlunoViewModel> GetAllAlunos()
    {
        var viewModel = new List<AlunoViewModel>();
        foreach (var aluno in _alunos)
        {
            viewModel.Add(new AlunoViewModel
            {
                AlunoID = aluno.AlunoID,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Serie = aluno.Serie,
                CPF = aluno.CPF,
                Idade = aluno.Idade,
                Turma = aluno.Turma,
                Matricula = aluno.Matricula
            });
        }
        return viewModel;
    }

    public Task CadastrarAlunoAsync(Aluno aluno)
    {
        aluno.AlunoID = _alunos.Count + 1; // Simulação de incremento de ID
        _alunos.Add(aluno);
        return Task.CompletedTask;
    }
}
