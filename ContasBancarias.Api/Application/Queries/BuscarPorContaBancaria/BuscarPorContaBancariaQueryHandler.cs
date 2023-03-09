using ContasBancarias.Api.Application.Commands.Responses;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Api.Domain.Entities.ContaBancaria;
using ContasBancarias.Application.Commands.Responses;
using ContasBancarias.Domain.Interfaces.Repository;
using ContasBancarias.Infrastructure.Repository;
using MediatR;

namespace ContasBancarias.Api.Application.Queries.BuscarPorId
{
    public class BuscarPorContaBancariaQueryHandler : IRequestHandler<BuscarPorContaBancariaQuery, ContaBancariaResponse>
    {
        private readonly IContaBancariaRepository _contaBancariaRepository;
        private readonly ILogger<ContaBancariaRepository> _logger;
        private readonly IMediator _mediator;

        public BuscarPorContaBancariaQueryHandler(IMediator mediator, IContaBancariaRepository contaBancariaRepository, ILogger<ContaBancariaRepository> logger)
        {
            _contaBancariaRepository = contaBancariaRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ContaBancariaResponse> Handle(BuscarPorContaBancariaQuery query, CancellationToken cancellationToken)
        {
            try
            {            
                ContaBancariaResponse contaBancariaResponse = new ContaBancariaResponse();

                var contaBancaria = await _contaBancariaRepository.Buscar(query.Conta);

                if (contaBancaria != null)
                {
                    MontarResponse montar = new MontarResponse();
                    contaBancariaResponse = await montar.MontarContaBancariaResponse(contaBancaria);
                }

                return contaBancariaResponse; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao consultar conta bancária: {query.Conta}");
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                throw;
            }
        }
    }
}
