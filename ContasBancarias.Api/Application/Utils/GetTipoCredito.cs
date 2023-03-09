using ContasBancarias.Api.Application.Enum;
using ContasBancarias.Infrastructure.Configurations;

namespace ContasBancarias.Api.Application.Utils
{
    public static class GetTipoCredito
    {
        public static string GetTipoCreditoSigla(int tipoCredito)
        {
            switch (tipoCredito)
            {
                case 1:
                    return "cd";
                case 2:
                    return "cc";
                case 3:
                    return "cpj";
                case 4:
                    return "cpf";
                case 5:
                    return "ci";
                default:
                    throw new ApplicationException("Tipo de credito não existe");
            }
        }

        public static int GetTipoCreditoId(string tipoCredito)
        {
            switch (tipoCredito.ToLower())
            {
                case "cd":
                    return (int)TipoCreditoBancaria.CreditoDireto;
                case "cc":
                    return (int)TipoCreditoBancaria.CreditoConsignado;
                case "cpj":
                    return (int)TipoCreditoBancaria.CreditoPessoaJuridica;
                case "cpf":
                    return (int)TipoCreditoBancaria.CreditoPessoaFisica;
                case "ci":
                    return (int)TipoCreditoBancaria.CreditoImobiliario;
                default:
                    throw new ApplicationException("Tipo de credito não existe");
            }            
        }

        public static decimal GetTipoCreditoTaxas(string tipoCredito)
        {
            switch (tipoCredito.ToLower())
            {
                case "cd":
                    return decimal.Parse(ConstantsConfiguration.TaxaCreditoDireto);
                case "cc":
                    return decimal.Parse(ConstantsConfiguration.TaxaCreditoConsignado);
                case "cpj":
                    return decimal.Parse(ConstantsConfiguration.TaxaCreditoPessoaJuridica);
                case "cpf":
                    return decimal.Parse(ConstantsConfiguration.TaxaCreditoPessoaFisica);
                case "ci":
                    return decimal.Parse(ConstantsConfiguration.TaxaCreditoImobiliario);
                default:
                    throw new ApplicationException("Tipo de credito não existe");
            }
        }
    }
}
