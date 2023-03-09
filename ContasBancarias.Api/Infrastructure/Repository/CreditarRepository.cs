using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Domain.Entities.Emprestimo;
using ContasBancarias.Api.Domain.Interfaces.Repository;
using ContasBancarias.Api.Infrastructure.Sqlite.Configurations;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ContasBancarias.Api.Infrastructure.Repository
{
    public class CreditarRepository : ICreditarRepository
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly ILogger<CreditarRepository> _logger;

        public CreditarRepository(DatabaseConfig databaseConfig, ILogger<CreditarRepository> logger)
        {
            this.databaseConfig = databaseConfig;
            _logger = logger;
        }

        public async Task<Emprestimo> Inserir(Emprestimo request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = $"insert into Emprestimo (id, idContaBancaria, tipoCredito, qtdParcelas, valor, dataPrimeiroVencimento) values (@Id, @IdContaBancaria, @TipoCredito, @QtdParcelas, @Valor, @DataPrimeiroVencimento)";

                var result = connection.Execute(query, request);

                return await Task.FromResult(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao inserir emprestimo para conta bancaria: {request.IdContaBancaria}");
                throw;
            }
        }
    }
}
