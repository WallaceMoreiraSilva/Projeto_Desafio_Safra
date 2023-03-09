using FluentValidation;
using ContasBancarias.Application.Commands.Requests;
using ContasBancarias.Application.Validations.Abstractions;

namespace ContasBancarias.Application.Validations.Rules
{
    public class InserirRequestValidation : AValidation<InserirContaBancariaRequest>
    {
        public InserirRequestValidation()
        {
            RuleFor(a => a.Conta).NotNull().Must(x => (int.TryParse(x, out var val) && val > 0) && (x.ToString().Length <= 10)).WithMessage("Conta bancária inválida");
            RuleFor(x => x.Nome).NotNull().Length(4, 250).WithMessage("Nome do titular deve ter entre 4 e 250 caracteres");
            RuleFor(x => x.Valor).GreaterThanOrEqualTo(0).WithMessage("Verifique se o valor de depósito está correto");
        }
    }
}
    