using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Application.Enum;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Api.Application.Validations.Rules;
using ContasBancarias.Application.Commands.Responses;
using ContasBancarias.Domain.Enums;
using ContasBancarias.Domain.Extensions;
using ContasBancarias.Domain.Interfaces.Repository;
using ContasBancarias.Infrastructure.Repository;
using MediatR;

namespace ContasBancarias.Api.Application.Commands.Handler
{
    public class AtivarContaBancariaHandler : IRequestHandler<AtivarContaBancariaRequest, Response<StatusResponse>>
    {
        private readonly IContaBancariaRepository _contaBancariaRepository;
        private readonly ILogger<ContaBancariaRepository> _logger;
        private readonly IMediator _mediator;

        public AtivarContaBancariaHandler(IMediator mediator, IContaBancariaRepository contaBancariaRepository, ILogger<ContaBancariaRepository> logger)
        {
            _contaBancariaRepository = contaBancariaRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Response<StatusResponse>> Handle(AtivarContaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validation = new AtivarRequestValidation();

                var validationResult = validation.IsValid(request);

                if (validationResult.PossuiErro)
                    return new Response<StatusResponse>(validationResult.Erro.Detalhes);

                var contaBancaria = await _contaBancariaRepository.Buscar(int.Parse(request.Conta));

                if (contaBancaria == null)
                {
                    return FactoryResponse.Criar(new StatusResponse
                    {
                        Status = (int)ActionContaBancaria.NaoEncontrado,
                        Mensagem = ActionContaBancaria.NaoEncontrado.GetDisplayValueOrDefault()
                    });
                }

                contaBancaria.Ativo = (int)StatusContaBancaria.Ativar;
                               
                await _contaBancariaRepository.AtualizarStatus(contaBancaria.Conta, contaBancaria);

                await _mediator.Publish(new AtivarContaBancariaNotification
                {
                    Conta = int.Parse(request.Conta),
                    Data = DateTime.Now,
                    Action = ActionNotification.Ativada
                }, cancellationToken);

                return FactoryResponse.Criar(new StatusResponse
                {
                    Status = (int)ActionContaBancaria.Ativar,                    
                    Mensagem = ActionContaBancaria.Ativar.GetDisplayValueOrDefault()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao ativar conta bancária: {request.Conta}");
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                throw;
            }
        }
    }
}
