using ContasBancarias.Application.Commands.Responses;
using MediatR;

namespace ContasBancarias.Api.Application.Queries.BuscarTodos
{
    public class BuscarTodosQuery : IRequest<List<ContaBancariaResponse>>
    {
    }
}
