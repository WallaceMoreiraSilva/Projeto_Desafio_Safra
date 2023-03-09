using ContasBancarias.Api.Domain.Entities.Movimento;

namespace ContasBancarias.Api.Domain.Interfaces.Repository
{
    public interface IMovimentoRepository
    {
        Task<Movimento> Inserir(Movimento request);
    }
}
