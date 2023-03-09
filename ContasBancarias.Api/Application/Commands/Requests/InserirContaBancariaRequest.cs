using ContasBancarias.Application.Commands.Responses;
using MediatR;

namespace ContasBancarias.Application.Commands.Requests
{
    public class InserirContaBancariaRequest : IRequest<Response<StatusResponse>>
    {
        public string Conta { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
