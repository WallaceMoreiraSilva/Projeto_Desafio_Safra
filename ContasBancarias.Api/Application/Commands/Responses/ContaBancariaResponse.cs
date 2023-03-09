namespace ContasBancarias.Application.Commands.Responses
{
    public class ContaBancariaResponse
    {
        public int Conta { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public int Ativo { get; set; }
    }
}
