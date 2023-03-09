using ContasBancarias.Domain.Interfaces.Repository;
using Microsoft.Data.Sqlite;
using Dapper;
using ContasBancarias.Api.Infrastructure.Sqlite.Configurations;
using ContasBancarias.Api.Domain.Entities.ContaBancaria;

namespace ContasBancarias.Infrastructure.Repository
{
    public class ContaBancariaRepository : IContaBancariaRepository
    {
        private readonly DatabaseConfig databaseConfig;
        private readonly ILogger<ContaBancariaRepository> _logger;        

        public ContaBancariaRepository(DatabaseConfig databaseConfig, ILogger<ContaBancariaRepository> logger)
        {
            this.databaseConfig = databaseConfig;
            _logger = logger;
        }

        public async Task<ContaBancaria> Inserir(ContaBancaria request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = $"insert into ContaBancaria (id, conta, nome, saldo, ativo) values (@Id, @Conta, @Nome, @Saldo, @Ativo)";

                var result = connection.Execute(query, request);

                return await Task.FromResult(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao inserir conta bancária: {request.Conta}");
                throw;
            }
        }

        public async Task<bool> Atualizar(int conta, ContaBancaria request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = $"update ContaBancaria set nome = @Nome where conta = " + conta;

                var result = connection.Execute(query, request);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao atualizar conta bancária: {request.Conta}");
                throw;
            }
        }

        public async Task<bool> AtualizarStatus(int conta, ContaBancaria request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = $"update ContaBancaria set ativo = @Ativo where conta = " + conta;

                var result = connection.Execute(query, request);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao ativar conta bancária: {request.Conta}");
                throw;
            }
        }

        public async Task<IEnumerable<ContaBancaria>> BuscarTodos()
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var result = connection.Query<ContaBancaria>("select * from ContaBancaria");

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao buscar contas bancárias");
                throw;
            }           
        }

        public async Task<ContaBancaria> Buscar(int conta)
        {
            try
            {               
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = "select * from ContaBancaria where conta = " + conta;

                var result = connection.Query<ContaBancaria>(query).FirstOrDefault();

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao buscar conta bancária: {conta}");
                throw;
            }           
        }

        public async Task<bool> Creditar(int conta, ContaBancaria request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                var query = $"update ContaBancaria set saldo = @Saldo WHERE conta = " + conta;

                var result = connection.Execute(query, request);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao creditar na conta bancária: {request.Conta}");
                throw;
            }
        }
    }
}
