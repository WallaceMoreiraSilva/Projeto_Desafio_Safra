using ContasBancarias.Api.Domain.Entities.ContaBancaria;
using MediatR;

namespace ContasBancarias.Domain.Interfaces.Repository
{
    public interface IContaBancariaRepository
    {
        Task<ContaBancaria> Inserir(ContaBancaria request);
        Task<bool> Atualizar(int conta, ContaBancaria request);
        Task<bool> AtualizarStatus(int conta, ContaBancaria request);
        Task<ContaBancaria> Buscar(int conta);
        Task<IEnumerable<ContaBancaria>> BuscarTodos();
        Task<bool> Creditar(int conta, ContaBancaria request);       
    }
}
