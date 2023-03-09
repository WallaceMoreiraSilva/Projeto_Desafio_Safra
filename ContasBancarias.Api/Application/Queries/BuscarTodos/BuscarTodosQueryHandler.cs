using ContasBancarias.Api.Application.Commands.Responses;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Application.Commands.Responses;
using ContasBancarias.Domain.Interfaces.Repository;
using ContasBancarias.Infrastructure.Repository;
using MediatR;

namespace ContasBancarias.Api.Application.Queries.BuscarTodos
{
    public class BuscarTodosQueryHandler : IRequestHandler<BuscarTodosQuery, List<ContaBancariaResponse>>
    {
        private readonly IContaBancariaRepository _contaBancariaRepository;
        private readonly ILogger<ContaBancariaRepository> _logger;
        private readonly IMediator _mediator;

        public BuscarTodosQueryHandler(IMediator mediator, IContaBancariaRepository contaBancariaRepository, ILogger<ContaBancariaRepository> logger)
        {
            _contaBancariaRepository = contaBancariaRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<List<ContaBancariaResponse>> Handle(BuscarTodosQuery query, CancellationToken cancellationToken)
        {
            try
            {
                List<ContaBancariaResponse> contasBancariasResponse = new List<ContaBancariaResponse>();

                var contasBancarias = await _contaBancariaRepository.BuscarTodos();

                if(contasBancarias != null)
                {
                    MontarResponse montar = new MontarResponse();
                    contasBancariasResponse = await montar.MontarContasBancariasResponse(contasBancarias);                    
                }

                return contasBancariasResponse;                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao consultar contas bancárias");
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                throw;
            }
        }
    }
}
