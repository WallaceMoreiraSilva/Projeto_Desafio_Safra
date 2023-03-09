using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Domain.Entities.Chave;

namespace ContasBancarias.Api.Domain.Entities.Movimento
{
    public class Movimento : ChaveId
    {        
        public string IdContaBancaria { get; set; }       
        public string TipoCredito { get; set; }
        public int QtdParcelas { get; set; }
        public decimal Valor { get; set; }
        public string DataPrimeiroVencimento { get; set; }

        public Movimento() { }

        public Movimento(CreditarContaBancariaRequest request)
        {
            Id = Id;
            IdContaBancaria = IdContaBancaria;
            TipoCredito = TipoCredito;
            QtdParcelas = request.QtdParcelas;
            Valor = request.Valor;
            DataPrimeiroVencimento = request.DataPrimeiroVencimento.ToString(); 
        }
    }
}
