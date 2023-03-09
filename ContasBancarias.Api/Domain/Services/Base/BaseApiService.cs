using ContasBancarias.Domain.Entities.Api;
using ContasBancarias.Domain.Interfaces.Api;
using ContasBancarias.Domain.Interfaces.Serializer;

namespace ContasBancarias.Domain.Services.Base
{
    public abstract class BaseApiService
    {
        protected readonly string ServiceName;
        protected readonly IApiService ApiService;
        protected readonly ISerializerService Serializer;
        protected const string UnauthorizedMessage = "Serviço não autorizado a realizar consulta.";
        private const string BadRequestMessageTemplate = "Url {0} retornou StatusCode InternalServerError.";
        private const string NotFoundMessageTemplate = "Url {0} não encontrada.";

        protected BaseApiService(string serviceName, IApiService apiService, ISerializerService serializer)
        {
            ServiceName = serviceName;
            Serializer = serializer;
            ApiService = apiService;
        }

        public T Deserialize<T>(ApiResponse apiResponse) => Serializer.DeserializeObject<T>(apiResponse.Content);

        public string NotFoundMessage(ApiResponse apiResponse) => string.Format(NotFoundMessageTemplate,
            apiResponse.ResponseUri?.AbsoluteUri ?? string.Empty);

        public string BadRequestMessage(ApiResponse apiResponse) => string.Format(BadRequestMessageTemplate,
            apiResponse.ResponseUri?.AbsoluteUri ?? string.Empty);
    }
}

