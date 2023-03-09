using ContasBancarias.Domain.Entities.Base;

namespace ContasBancarias.Api.Domain.Entities.Chave
{
    public class ChaveId : IEntity
    {
        public string Id { get; set; }

        private string CreateId()
            => $"{Guid.NewGuid()}".ToUpper();

        public ChaveId()
        {
            Id = CreateId();
        }

        public ChaveId(string origem)
        {
            Id = CreateId();
        }
    }
}
