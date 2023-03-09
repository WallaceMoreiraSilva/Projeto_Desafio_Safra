using ContasBancarias.Api.Application.Enum;
using MediatR;

namespace ContasBancarias.Api.Application.Notification
{
    public class InativarContaBancariaNotification : INotification
    {
        public int Conta { get; set; }
        public DateTime Data { get; set; }
        public ActionNotification Action { get; set; }
    }
}
