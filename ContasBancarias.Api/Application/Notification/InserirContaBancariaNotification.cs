using ContasBancarias.Api.Application.Enum;
using MediatR;

namespace ContasBancarias.Api.Application.Notification
{
    public class InserirContaBancariaNotification : INotification
    {
        public int Conta { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public ActionNotification Action { get; set; }       
    }
}
