using ContasBancarias.Application.Commands.Responses;
using MediatR;

namespace ContasBancarias.Api.Application.Commands.Requests
{
    public class InativarContaBancariaRequest : IRequest<Response<StatusResponse>>
    {
        public string Conta { get; set; }
    }
}
