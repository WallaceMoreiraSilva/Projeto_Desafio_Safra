using ContasBancarias.Configurations;
using ContasBancarias.Domain.Entities.Api;
using ContasBancarias.Domain.Interfaces.Api;
using RestSharp;

namespace ContasBancarias.Domain.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly Dictionary<string, RestClient> _clients;

        public ApiService(ApiServiceConfiguration configuration)
        {
            _clients = configuration.ServiceUrlDictionary.ToDictionary(url => url.Key, url => CreateClient(url.Value));
        }

        public async Task<ApiResponse> DeleteAsync(string baseUrl, string endpoint, Dictionary<string, string> customHeaders = null)
        {
            var client = GetClientByServiceName(baseUrl);

            if (client == null) throw new System.Exception(ClientInvalido(baseUrl));

            var request = CreateRequest(endpoint, Method.Delete);

            if (customHeaders != default) AddHeaders(request, customHeaders);

            var response = await client.ExecuteAsync(request);

            return CriarResponse(response);
        }

        public async Task<ApiResponse> GetAsync(string baseUrl, string endpoint, Dictionary<string, string> queryParameters = default, Dictionary<string, string> customHeaders = null)
        {
            var client = GetClientByServiceName(baseUrl);

            if (client == null) throw new System.Exception(ClientInvalido(baseUrl));

            var request = CreateRequest(endpoint, Method.Get);

            if (customHeaders != default) AddHeaders(request, customHeaders);

            if (queryParameters != default) AddParameters(request, queryParameters);

            var response = await client.ExecuteAsync(request);

            return CriarResponse(response);
        }

        public async Task<ApiResponse> PostAsync<TBody>(string baseUrl, string endpoint, TBody body = null, Dictionary<string, string> customHeaders = null)
            where TBody : class
        {
            var client = GetClientByServiceName(baseUrl);

            if (client == null) throw new System.Exception(ClientInvalido(baseUrl));

            var request = CreateRequest(endpoint, Method.Post);

            if (customHeaders != default) AddHeaders(request, customHeaders);

            if (body != null) request.AddBody(body);

            var response = await client.ExecuteAsync(request);

            return CriarResponse(response);
        }

        public async Task<ApiResponse> PutAsync<TBody>(string baseUrl, string endpoint, TBody body = null, Dictionary<string, string> customHeaders = null)
            where TBody : class
        {
            var client = GetClientByServiceName(baseUrl);

            if (client == null) throw new System.Exception(ClientInvalido(baseUrl));

            var request = CreateRequest(endpoint, Method.Put);

            if (customHeaders != default) AddHeaders(request, customHeaders);

            if (body != null) request.AddBody(body);

            var response = await client.ExecuteAsync(request);

            return CriarResponse(response);
        }

        public async Task<ApiResponse> PatchAsync<TBody>(string baseUrl, string endpoint, TBody body = null, Dictionary<string, string> customHeaders = null)
            where TBody : class
        {
            var client = GetClientByServiceName(baseUrl);

            if (client == null) throw new System.Exception(ClientInvalido(baseUrl));

            var request = CreateRequest(endpoint, Method.Patch);

            if (customHeaders != default) AddHeaders(request, customHeaders);

            if (body != null) request.AddBody(body);

            var response = await client.ExecuteAsync(request);

            return CriarResponse(response);
        }

        public async Task<ApiResponse> PostCallbackAsync<TBody>(string url, TBody body = null, Dictionary<string, string> customHeaders = null)
            where TBody : class
        {
            var client = GetClientByServiceName(url);

            if (client == null)
            {
                client = CreateClient(url);

                AddToClientDictionary(url, client);
            }

            var request = CreateRequest(url, Method.Post);

            if (customHeaders != default) AddHeaders(request, customHeaders);

            if (body != null) request.AddBody(body);

            var response = await client.ExecuteAsync(request);

            return CriarResponse(response);
        }

        private RestClient CreateClient(string baseUrl)
        {
            //TODO: analisar options, definir timeout etc
            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false
            };

            return new RestClient(options);
        }

        private string ClientInvalido(string url)
            => $"Client nao configurado para url '{url}'";

        private RestClient GetClientByServiceName(string serviceName)
            => _clients.TryGetValue(serviceName, out RestClient client) ? client : null;

        private RestRequest CreateRequest(string endpoint, Method method)
            => new RestRequest(endpoint, method);

        private void AddParameters(RestRequest request, Dictionary<string, string> parameters)
        {
            foreach (var item in parameters)
                request.AddParameter(item.Key, item.Value);
        }

        private void AddHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value);
        }

        private ApiResponse CriarResponse(RestResponse response)
        {
            return new ApiResponse(response.Content,
                response.StatusCode,
                response.IsSuccessful,
                response.ErrorException,
                response.ErrorMessage,
                response.ResponseUri);
        }

        private void AddToClientDictionary(string url, RestClient client)
            => _clients.Add(url, client);
    }
}
