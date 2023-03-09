using ContasBancarias.Application.Commands.Responses;

namespace ContasBancarias.Application.Validations.Interfaces
{
    public interface IValidation<T> where T : class
    {
        Response<T> IsValid(T obj);
        Task<Response<T>> IsValidAsync(T obj);
    }
}
