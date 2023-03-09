using System.ComponentModel.DataAnnotations;

namespace ContasBancarias.Domain.Enums
{
    public enum ActionContaBancaria
    {
        [Display(Name = "Inserida com sucesso")]
        Inserido = 1,
        [Display(Name = "Atualizada com sucesso")]
        Atualizado,
        [Display(Name = "Ativada com sucesso")]
        Ativar,
        [Display(Name = "Inativada com sucesso")]
        Inativar,
        [Display(Name = "Já Existe uma conta bancária com essa numeração")]
        JaCadastrado,
        [Display(Name = "Não foi possível localizar a conta bancária informada")]
        NaoEncontrado,
        [Display(Name = "Credito efetuado com sucesso")]
        Creditar,
        [Display(Name = "Não é possível creditar em conta bancária inativa")]
        CreditarEmContaInativa
    }
}
