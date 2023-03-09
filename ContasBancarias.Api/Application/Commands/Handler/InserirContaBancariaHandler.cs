using ContasBancarias.Api.Application.Enum;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Api.Domain.Entities.ContaBancaria;
using ContasBancarias.Application.Commands.Requests;
using ContasBancarias.Application.Commands.Responses;
using ContasBancarias.Application.Validations.Rules;
using ContasBancarias.Domain.Enums;
using ContasBancarias.Domain.Extensions;
using ContasBancarias.Domain.Interfaces.Repository;
using ContasBancarias.Infrastructure.Repository;
using MediatR;

namespace ContasBancarias.Api.Application.Commands.Handler
{
    public class InserirContaBancariaHandler : IRequestHandler<InserirContaBancariaRequest, Response<StatusResponse>>
    {
        private readonly IContaBancariaRepository _contaBancariaRepository;
        private readonly ILogger<ContaBancariaRepository> _logger;
        private readonly IMediator _mediator;

        public InserirContaBancariaHandler(IMediator mediator, IContaBancariaRepository contaBancariaRepository, ILogger<ContaBancariaRepository> logger)
        {
            _contaBancariaRepository = contaBancariaRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Response<StatusResponse>> Handle(InserirContaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validation = new InserirRequestValidation();

                var validationResult = validation.IsValid(request);

                if (validationResult.PossuiErro)
                    return new Response<StatusResponse>(validationResult.Erro.Detalhes);

                var contaBancariaRequest = new ContaBancaria(request);

                var contaBancaria = await _contaBancariaRepository.Buscar(contaBancariaRequest.Conta);

                if (contaBancaria != null)
                {
                    return FactoryResponse.Criar(new StatusResponse
                    {
                        Status = (int)ActionContaBancaria.JaCadastrado,
                        Mensagem = ActionContaBancaria.JaCadastrado.GetDisplayValueOrDefault()
                    });
                }               

                var result = await _contaBancariaRepository.Inserir(contaBancariaRequest);

                await _mediator.Publish(new InserirContaBancariaNotification
                {
                    Conta = int.Parse(request.Conta),
                    Nome = request.Nome,
                    Data = DateTime.Now,
                    Action = ActionNotification.Criada                   
                }, cancellationToken);             

                return FactoryResponse.Criar(new StatusResponse
                {
                    Status = (int)ActionContaBancaria.Inserido,
                    Mensagem = ActionContaBancaria.Inserido.GetDisplayValueOrDefault()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao inserir conta bancária: {request.Conta}");
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                throw;
            }
        }
    }
}
