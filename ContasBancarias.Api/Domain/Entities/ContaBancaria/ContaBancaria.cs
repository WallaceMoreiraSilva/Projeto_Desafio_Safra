using ContasBancarias.Api.Application.Enum;
using ContasBancarias.Api.Domain.Entities.Chave;
using ContasBancarias.Application.Commands.Requests;

namespace ContasBancarias.Api.Domain.Entities.ContaBancaria
{
    public class ContaBancaria : ChaveId
    {        
        public int Conta { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public int Ativo { get; set; }

        public ContaBancaria() { }


        public ContaBancaria(int conta, string nome, decimal saldo, int ativo)
        {
            Id = Id;
            Conta = conta;
            Nome = nome;
            Saldo = saldo;
            Ativo = ativo;
        }

        public ContaBancaria(InserirContaBancariaRequest command)
        {
            Id = Id;
            Conta = int.Parse(command.Conta);
            Nome = command.Nome;
            Saldo = command.Valor;
            Ativo = (int)StatusContaBancaria.Inativar;
        }

        public ContaBancaria(AtualizarContaBancariaRequest command)
        {
            Nome = command.Nome;
        }      
    }
}
