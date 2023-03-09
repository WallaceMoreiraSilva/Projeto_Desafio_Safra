using ContasBancarias.Application.Commands.Responses;
using MediatR;

namespace ContasBancarias.Api.Application.Queries.BuscarPorId
{
    public class BuscarPorContaBancariaQuery : IRequest<ContaBancariaResponse>
    {
        public int Conta { get; set; }
    }
}
