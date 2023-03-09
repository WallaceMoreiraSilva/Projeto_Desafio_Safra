namespace ContasBancarias.Application.Commands.Responses
{
    [Serializable]
    public class Response
    {
        public ErroResponse Erro { get; private set; }
        
        public bool PossuiErro { get; private set; }

        public Response()
        {
            PossuiErro = false;
            Erro = new ErroResponse();
        }

        public Response(string erro) : this()
        {
            Erro.Detalhes.Add(erro);
            PossuiErro = true;
        }

        public Response(IEnumerable<string> erros) : this()
        {
            foreach (var item in erros)
                Erro.Detalhes.Add(item);

            PossuiErro = true;
        }
    }

    public class Response<T> : Response where T : class
    {
        public T Conteudo { get; set; }

        public Response()
        {
            Conteudo = default(T);
        }

        public Response(T conteudo)
        {
            Conteudo = conteudo;
        }

        public Response(string erro) : base(erro) { }

        public Response(IEnumerable<string> erros) : base(erros) { }
    }
}
