using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Application.Enum;
using ContasBancarias.Api.Application.Notification;
using ContasBancarias.Api.Application.Utils;
using ContasBancarias.Api.Application.Validations.Rules;
using ContasBancarias.Api.Domain.Entities.Emprestimo;
using ContasBancarias.Api.Domain.Interfaces.Repository;
using ContasBancarias.Api.Infrastructure.Repository;
using ContasBancarias.Application.Commands.Responses;
using ContasBancarias.Domain.Enums;
using ContasBancarias.Domain.Extensions;
using ContasBancarias.Domain.Interfaces.Repository;
using ContasBancarias.Infrastructure.Configurations;
using MediatR;

namespace ContasBancarias.Api.Application.Commands.Handler
{
    public class CreditarContaBancariaHandler : IRequestHandler<CreditarContaBancariaRequest, Response<StatusResponse>>
    {
        private readonly ICreditarRepository _emprestimoContaBancariaRepository;
        private readonly IContaBancariaRepository _contaBancariaRepository;
        private readonly ILogger<CreditarRepository> _logger;
        private readonly IMediator _mediator;

        public CreditarContaBancariaHandler(IMediator mediator, 
            ICreditarRepository emprestimoContaBancariaRepository, 
            IContaBancariaRepository contaBancariaRepository, 
            ILogger<CreditarRepository> logger)
        {
            _emprestimoContaBancariaRepository = emprestimoContaBancariaRepository;
            _contaBancariaRepository = contaBancariaRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Response<StatusResponse>> Handle(CreditarContaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {    
                var validation = new CreditarRequestValidation();

                var validationResult = validation.IsValid(request);

                if (validationResult.PossuiErro)
                    return new Response<StatusResponse>(validationResult.Erro.Detalhes);              

                var contaBancaria = await _contaBancariaRepository.Buscar(int.Parse(request.Conta));                

                if (contaBancaria != null)
                {
                    var contaBancariaAtiva = contaBancaria.Ativo == (int)StatusContaBancaria.Ativar ? true : false;

                    if (contaBancariaAtiva)
                    {
                        var totalSaldo = contaBancaria.Saldo + request.Valor;

                        contaBancaria.Saldo = totalSaldo;

                        await _contaBancariaRepository.Creditar(contaBancaria.Conta, contaBancaria);

                        var emprestimoRequest = new Emprestimo(request);

                        emprestimoRequest.TipoCredito = GetTipoCredito.GetTipoCreditoSigla(request.TipoCredito);

                        emprestimoRequest.IdContaBancaria = contaBancaria.Id.ToString();

                        var valorEmprestimo = CalcularEmprestimo.GetValorPercentual(request.Valor, request.TipoCredito);

                        var totalEmprestimo = request.Valor + valorEmprestimo;

                        emprestimoRequest.Valor = totalEmprestimo;

                        await _emprestimoContaBancariaRepository.Inserir(emprestimoRequest);

                        await _mediator.Publish(new CreditarContaBancariaNotification
                        {
                            Conta = int.Parse(request.Conta),
                            Valor = request.Valor,
                            DataPrimeiroVencimento = request.DataPrimeiroVencimento,
                            TipoCredito = GetTipoCredito.GetTipoCreditoId(emprestimoRequest.TipoCredito)
                        }, cancellationToken);

                        return FactoryResponse.Criar(new StatusResponse
                        {
                            Status = (int)ActionContaBancaria.Creditar,
                            Mensagem = ActionContaBancaria.Creditar.GetDisplayValueOrDefault()
                        });
                    }
                    else
                    {
                        return FactoryResponse.Criar(new StatusResponse
                        {
                            Status = (int)ActionContaBancaria.CreditarEmContaInativa,
                            Mensagem = ActionContaBancaria.CreditarEmContaInativa.GetDisplayValueOrDefault()
                        });
                    }
                }
                else
                {
                    return FactoryResponse.Criar(new StatusResponse
                    {
                        Status = (int)ActionContaBancaria.NaoEncontrado,
                        Mensagem = ActionContaBancaria.NaoEncontrado.GetDisplayValueOrDefault()
                    }); 
                }               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao creditar {request.Valor} na conta bancaria {request.Conta}");
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                throw;
            }
        }
    }
}
