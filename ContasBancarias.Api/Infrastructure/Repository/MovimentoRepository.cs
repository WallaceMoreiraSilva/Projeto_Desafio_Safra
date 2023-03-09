using ContasBancarias.Api.Application.Commands.Requests;
using ContasBancarias.Api.Domain.Entities.Movimento;
using ContasBancarias.Api.Domain.Interfaces.Repository;
using ContasBancarias.Api.Infrastructure.Sqlite.Configurations;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ContasBancarias.Api.Infrastructure.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly ILogger<MovimentoRepository> _logger;

        public MovimentoRepository(DatabaseConfig databaseConfig, ILogger<MovimentoRepository> logger)
        {
            this.databaseConfig = databaseConfig;
            _logger = logger;
        }

        public async Task<Movimento> Inserir(Movimento request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = $"insert into Movimento (id, idContaBancaria, tipoCredito, qtdParcelas, valor, dataPrimeiroVencimento) values (@Id, @IdContaBancaria, @TipoCredito, @QtdParcelas, @Valor, @DataPrimeiroVencimento)";

                var result = connection.Execute(query, request);

                return await Task.FromResult(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao inserir movimentação da conta corrente: {request.IdContaBancaria}");
                throw;
            }
        }
    }
}
