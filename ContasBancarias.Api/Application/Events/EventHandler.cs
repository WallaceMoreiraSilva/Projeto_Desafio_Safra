using ContasBancarias.Api.Application.Notification;
using MediatR;

namespace ContasBancarias.Api.Application.Events
{
    public class EventHandler :
                            INotificationHandler<AtivarContaBancariaNotification>,
                            INotificationHandler<InativarContaBancariaNotification>,
                            INotificationHandler<InserirContaBancariaNotification>,
                            INotificationHandler<AtualizarContaBancariaNotification>,
                            INotificationHandler<CreditarContaBancariaNotification>, 
                            INotificationHandler<ErroNotification>
    {
        public Task Handle(InserirContaBancariaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Criada: '{notification.Conta} " +
                    $"- {notification.Nome} - {notification.Data}'");
            });
        }

        public Task Handle(AtualizarContaBancariaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Alterada: '{notification.Nome} - {notification.Data} '");
            });
        }      

        public Task Handle(CreditarContaBancariaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Credito: '{notification.Valor} " +
                    $" - {notification.Conta} - {notification.DataPrimeiroVencimento}'");
            });
        }
        public Task Handle(AtivarContaBancariaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Ativada: '{notification.Conta} " +
                    $"- {notification.Data}'");
            });
        }

        public Task Handle(InativarContaBancariaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Inativada: '{notification.Conta} " +
                    $"- {notification.Data}'");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Erro: '{notification.Erro} \n {notification.PilhaErro}'");
            });
        }
    }
}
