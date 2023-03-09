using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Application.Validations.Abstractions;
using FluentValidation;

namespace ContasBancarias.Api.Application.Validations.Rules
{
    public class AtivarRequestValidation : AValidation<AtivarContaBancariaRequest>
    {
        public AtivarRequestValidation()
        {
            RuleFor(a => a.Conta).NotNull().Must(x => (int.TryParse(x, out var val) && val > 0) && (x.ToString().Length <= 10)).WithMessage("Número de conta bancária inválido");
        }
    }
}
