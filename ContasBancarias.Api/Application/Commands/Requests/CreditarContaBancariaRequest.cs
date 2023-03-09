using ContasBancarias.Application.Commands.Responses;
using MediatR;

namespace ContasBancarias.Api.Application.Commands.Requests
{
    public class CreditarContaBancariaRequest : IRequest<Response<StatusResponse>>
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
        public int TipoCredito { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
    }
}
