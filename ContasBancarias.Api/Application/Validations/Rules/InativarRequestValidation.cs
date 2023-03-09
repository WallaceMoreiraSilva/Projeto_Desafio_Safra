using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Application.Validations.Abstractions;
using FluentValidation;

namespace ContasBancarias.Api.Application.Validations.Rules
{
    public class InativarRequestValidation : AValidation<InativarContaBancariaRequest>
    {
        public InativarRequestValidation()
        {
            RuleFor(a => a.Conta).NotNull().Must(x => (int.TryParse(x, out var val) && val > 0) && (x.ToString().Length <= 10)).WithMessage("Conta bancária inválida");
        }
    }
}
