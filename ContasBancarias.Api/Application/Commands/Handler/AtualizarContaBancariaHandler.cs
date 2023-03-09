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
    public class AtualizarContaBancariaHandler : IRequestHandler<AtualizarContaBancariaRequest, Response<StatusResponse>>
    {
        private readonly IContaBancariaRepository _contaBancariaRepository;
        private readonly ILogger<ContaBancariaRepository> _logger;
        private readonly IMediator _mediator;

        public AtualizarContaBancariaHandler(IMediator mediator, IContaBancariaRepository contaBancariaRepository, ILogger<ContaBancariaRepository> logger)
        {
            _contaBancariaRepository = contaBancariaRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Response<StatusResponse>> Handle(AtualizarContaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validation = new AtualizarRequestValidation();

                var validationResult = validation.IsValid(request);

                if (validationResult.PossuiErro)
                    throw new Exception(validationResult.Erro.Detalhes.ToString());

                var contaBancaria = await _contaBancariaRepository.Buscar(int.Parse(request.Conta));

                if (contaBancaria == null)
                { 
                    return FactoryResponse.Criar(new StatusResponse
                    {
                        Status = (int)ActionContaBancaria.NaoEncontrado,
                        Mensagem = ActionContaBancaria.NaoEncontrado.GetDisplayValueOrDefault()
                    });
                }

                var result = new ContaBancaria(request);         

                await _contaBancariaRepository.Atualizar(contaBancaria.Conta, result);

                await _mediator.Publish(new AtualizarContaBancariaNotification
                {
                    Nome = request.Nome,
                    Data = DateTime.Now,
                    Action = ActionNotification.Atualizada
                }, cancellationToken);

                return FactoryResponse.Criar(new StatusResponse
                {
                    Status = (int)ActionContaBancaria.Atualizado,
                    Mensagem = ActionContaBancaria.Atualizado.GetDisplayValueOrDefault()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao atualizar conta corrente: {request.Conta}");
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                throw;
            }
        }
    }
}
