using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Application.Commands.Requests;
using ContasBancarias.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContasBancarias.Api.Application.Queries.BuscarPorId;
using ContasBancarias.Api.Application.Queries.BuscarTodos;

namespace ContasBancarias.Api.Controller.ContaBancaria
{
    [ApiController]
    [ApiVersion(ConstantsConfiguration.ApiVersion)]
    [Route("api/v{version:apiVersion}/conta-bancaria")]
    public class ContaBancariaController : ControllerBase
    {
        private IMediator _mediator;

        public ContaBancariaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] InserirContaBancariaRequest request)
        {
            var result = await _mediator.Send(request, CancellationToken.None);
            return Ok(result);
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarContaBancariaRequest request)
        {            
            var result = await _mediator.Send(request, CancellationToken.None);
            return Ok(result);
        }

        [HttpPost]
        [Route("creditar")]
        public async Task<IActionResult> Creditar([FromBody] CreditarContaBancariaRequest request)
        {            
            var result = await _mediator.Send(request, CancellationToken.None);          
            return Ok(result);
        }

        [HttpPost]
        [Route("ativar")]
        public async Task<IActionResult> Ativar([FromBody] AtivarContaBancariaRequest request)
        {           
            var result = await _mediator.Send(request, CancellationToken.None);
            return Ok(result);           
        }

        [HttpPost]
        [Route("inativar")]
        public async Task<IActionResult> Inativar([FromBody] InativarContaBancariaRequest request)
        {
            var result = await _mediator.Send(request, CancellationToken.None);
            return Ok(result);
        }

        [HttpGet]
        [Route("consultar")]
        public async Task<IActionResult> BuscarPorContaBancaria(int conta)
        {
            var result = await _mediator.Send(new BuscarPorContaBancariaQuery { Conta = conta });            
            return Ok(result);
        }

        [HttpGet]
        [Route("consultar-todas")]
        public async Task<IActionResult> BuscarTodasContasBancarias()
        {
            var result = await _mediator.Send(new BuscarTodosQuery());
            return Ok(result);
        }
    }
}
