using FluentValidation;
using FluentValidation.Results;
using ContasBancarias.Application.Commands.Responses;
using ContasBancarias.Application.Validations.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ContasBancarias.Application.Validations.Abstractions
{
    public abstract class AValidation<T> : AbstractValidator<T>, IValidation<T>
           where T : class
    {
        public Response<T> IsValid(T obj)
        {
            var result = Validate(obj);
            return CriarResultadoValidacao(result, obj);
        }

        public async Task<Response<T>> IsValidAsync(T obj)
        {
            var result = await ValidateAsync(obj);
            return CriarResultadoValidacao(result, obj);
        }

        private Response<T> CriarResultadoValidacao(ValidationResult validationResult, T obj)
        {
            if (validationResult.IsValid)
                return new Response<T>(obj);

            var mensagensErro = validationResult.Errors.Select(x => x.ErrorMessage);

            return new Response<T>(mensagensErro);
        }
    }
}
