using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Application.Enum;
using ContasBancarias.Application.Validations.Abstractions;
using ContasBancarias.Infrastructure.Configurations;
using FluentValidation;

namespace ContasBancarias.Api.Application.Validations.Rules
{
    public class CreditarRequestValidation : AValidation<CreditarContaBancariaRequest>
    {
        public CreditarRequestValidation()
        {
            RuleFor(a => a.Conta).NotEmpty().Must(x => (int.TryParse(x, out var val) && val > 0) && (x.ToString().Length <= 10)).WithMessage("Conta bancária inválida");
            RuleFor(x => x.Valor).NotEmpty().GreaterThan(0).LessThanOrEqualTo(1000000).WithMessage("Verifique se o valor do crédito esta correto");
            RuleFor(x => x.QtdParcelas).NotEmpty().GreaterThanOrEqualTo(5).LessThanOrEqualTo(72).WithMessage("Verifique a quantidade de parcelas informada");
            
            RuleFor(x => x.DataPrimeiroVencimento)
                .NotEmpty().WithMessage("Informe a data do primeiro vencimento")
                .Must(IsValidDataPrimeiroVencimento)
                .WithMessage("A data do primeiro vencimento sempre deverá ser no minimo 15 dias e no máximo 40 dias a partir da data atual");

            RuleFor(x => x.Valor)
                .GreaterThanOrEqualTo(decimal.Parse(ConstantsConfiguration.ValorMinimoCreditoPessoaJuridica))
                .NotNull()
                .When(x => x.TipoCredito == (int)TipoCreditoBancaria.CreditoPessoaJuridica)
                .WithMessage("Para o crédito de pessoa jurídica , o valor mínimo a ser liberado é de R$ 15.000,00");
        }

        private static bool IsValidDataPrimeiroVencimento(DateTime dataPrimeiroVencimento) =>
                dataPrimeiroVencimento >= DateTime.Now.AddDays(15) && dataPrimeiroVencimento <= DateTime.Now.AddDays(40);
    }
}