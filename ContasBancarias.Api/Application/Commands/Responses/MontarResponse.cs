using ContasBancarias.Api.Domain.Entities.ContaBancaria;
using ContasBancarias.Application.Commands.Responses;

namespace ContasBancarias.Api.Application.Commands.Responses
{
    public class MontarResponse
    {
        public async Task<ContaBancariaResponse> MontarContaBancariaResponse(ContaBancaria request)
        {
            var contaBancaria = new ContaBancariaResponse 
            {
                Conta = request.Conta,
                Nome = request.Nome,
                Saldo = request.Saldo,
                Ativo = request.Ativo
            };    

            await Task.CompletedTask;

            return contaBancaria;
        }

        public async Task<List<ContaBancariaResponse>> MontarContasBancariasResponse(IEnumerable<ContaBancaria> request)
        {       
            List<ContaBancariaResponse> contasBancarias = new List<ContaBancariaResponse>();

            foreach (var item in request)
            {
                contasBancarias.Add(new ContaBancariaResponse
                {
                    Conta = item.Conta,
                    Nome = item.Nome,
                    Saldo = item.Saldo,
                    Ativo = item.Ativo
                });
            }           

            await Task.CompletedTask;

            return contasBancarias;
        }
    }
}
