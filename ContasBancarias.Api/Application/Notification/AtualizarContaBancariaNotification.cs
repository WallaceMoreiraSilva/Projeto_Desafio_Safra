using ContasBancarias.Api.Application.Enum;
using MediatR;

namespace ContasBancarias.Api.Application.Notification
{
    public class AtualizarContaBancariaNotification : INotification
    {       
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public ActionNotification Action { get; set; }       
    }
}
