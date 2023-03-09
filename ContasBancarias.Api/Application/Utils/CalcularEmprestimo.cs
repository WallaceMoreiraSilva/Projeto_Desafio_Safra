using ContasBancarias.Api.Domain.Entities.ContaBancaria;
using ContasBancarias.Infrastructure.Configurations;
using MediatR;

namespace ContasBancarias.Api.Application.Utils
{
    public static class CalcularEmprestimo
    {
        public static decimal GetValorPercentual(decimal valor, int tipoCredito)
        {
            var soma = 0.0M;
            var tipoCreditoNome = GetTipoCredito.GetTipoCreditoSigla(tipoCredito);
            var tipoCreditoTaxa = GetTipoCredito.GetTipoCreditoTaxas(tipoCreditoNome);
            var result = decimal.Round(valor / 100.0M * tipoCreditoTaxa, 2);           
            return result;
        }
    }
}
