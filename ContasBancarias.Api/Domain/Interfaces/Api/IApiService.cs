using ContasBancarias.Domain.Entities.Api;

namespace ContasBancarias.Domain.Interfaces.Api
{
    public interface IApiService
    {
        Task<ApiResponse> GetAsync(string baseUrl, string endpoint, Dictionary<string, string> queryParameters = default,
            Dictionary<string, string> customHeaders = default);

        Task<ApiResponse> PostAsync<TBody>(string baseUrl, string endpoint, TBody body = null,
            Dictionary<string, string> customHeaders = default)
            where TBody : class;

        Task<ApiResponse> PutAsync<TBody>(string baseUrl, string endpoint, TBody body = null,
            Dictionary<string, string> customHeaders = default)
            where TBody : class;

        Task<ApiResponse> DeleteAsync(string baseUrl, string endpoint, Dictionary<string, string> customHeaders = default);

        Task<ApiResponse> PatchAsync<TBody>(string baseUrl, string endpoint, TBody body = null,
            Dictionary<string, string> customHeaders = default)
            where TBody : class;

        Task<ApiResponse> PostCallbackAsync<TBody>(string url, TBody body = null, Dictionary<string, string> customHeaders = null)
           where TBody : class;
    }
}
