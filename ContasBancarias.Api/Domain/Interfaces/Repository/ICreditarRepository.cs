using ContasBancarias.Api.Domain.Entities.Emprestimo;

namespace ContasBancarias.Api.Domain.Interfaces.Repository
{
    public interface ICreditarRepository
    {
        Task<Emprestimo> Inserir(Emprestimo request);
    }
}
