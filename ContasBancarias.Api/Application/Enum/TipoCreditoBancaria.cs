using System.ComponentModel.DataAnnotations;

namespace ContasBancarias.Api.Application.Enum
{
    public enum TipoCreditoBancaria
    {
        [Display(Name = "CD")]
        CreditoDireto = 1,
        [Display(Name = "CC")]
        CreditoConsignado = 2,
        [Display(Name = "CPJ")]
        CreditoPessoaJuridica = 3,
        [Display(Name = "CPF")]
        CreditoPessoaFisica = 4,
        [Display(Name = "CI")]
        CreditoImobiliario = 5
    }
}
