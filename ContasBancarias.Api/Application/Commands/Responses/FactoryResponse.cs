namespace ContasBancarias.Application.Commands.Responses
{
    public static class FactoryResponse
    {
        public static Response<T> Criar<T>(T obj) where T : class
            => new Response<T>(obj);
        public static Response Criar(string mensagemErro)
            => new Response(mensagemErro);
        public static Response<T> Criar<T>(IEnumerable<string> erros) where T : class
            => new Response<T>(erros);
    }
}
