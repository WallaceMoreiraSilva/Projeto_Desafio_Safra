using ContasBancarias.Application.Commands.Requests;
using ContasBancarias.Application.Validations.Abstractions;
using FluentValidation;

namespace ContasBancarias.Application.Validations.Rules
{
    public class AtualizarRequestValidation : AValidation<AtualizarContaBancariaRequest>
    {
        public AtualizarRequestValidation()
        {
            RuleFor(a => a.Conta).NotNull().Must(x => (int.TryParse(x, out var val) && val > 0) && (x.ToString().Length <= 10)).WithMessage("Número de conta bancária inválido");
            RuleFor(x => x.Nome).NotNull().Length(4, 250).WithMessage("Nome do titular deve ter entre 4 e 250 caracteres");
        }
    }
}
