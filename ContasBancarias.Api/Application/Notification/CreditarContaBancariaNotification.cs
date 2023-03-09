using ContasBancarias.Api.Application.Enum;
using MediatR;

namespace ContasBancarias.Api.Application.Notification
{
    public class CreditarContaBancariaNotification : INotification
    {
        public int Conta { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
        public int TipoCredito{ get; set; }
    }
}
