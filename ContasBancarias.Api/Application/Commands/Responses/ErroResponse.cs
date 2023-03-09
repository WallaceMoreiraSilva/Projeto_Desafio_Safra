namespace ContasBancarias.Application.Commands.Responses
{
    public class ErroResponse
    {
        public ErroResponse()
        {
            Detalhes = new List<string>();
        }

        public ErroResponse(string msg) : this()
        {
            Detalhes.Add(msg);
        }

        public ICollection<string> Detalhes { get; set; }
    }
}
