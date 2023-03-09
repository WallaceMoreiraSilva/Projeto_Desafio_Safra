using ContasBancarias.Application.Commands.Responses;
using MediatR;

namespace ContasBancarias.Api.Application.Commands.Requests
{
    public class AtivarContaBancariaRequest : IRequest<Response<StatusResponse>>
    {
        public string Conta { get; set; }      
    }
}
