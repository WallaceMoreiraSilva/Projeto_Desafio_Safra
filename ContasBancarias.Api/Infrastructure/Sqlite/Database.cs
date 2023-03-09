using ContasBancarias.Api.Infrastructure.Sqlite.Configurations;
using ContasBancarias.Api.Infrastructure.Sqlite.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ContasBancarias.Infrastructure.Sqlite
{
    public class Database : IDatabase
    {
        private readonly DatabaseConfig databaseConfig;

        public Database(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND (name = 'ContaBancaria' or name = 'Movimento');");

            var tableName = table.FirstOrDefault();

            if (!string.IsNullOrEmpty(tableName) && (tableName == "ContaBancaria" || tableName == "Movimento"))
                return;

            connection.Execute("CREATE TABLE ContaBancaria ( " +
                               "id TEXT(250) PRIMARY KEY," +
                               "conta INTEGER(10) NOT NULL UNIQUE," +
                               "nome TEXT(250) NOT NULL," +
                               "saldo REAL NOT NULL," +
                               "ativo INTEGER(1) NOT NULL default 0," +
                               "CHECK(ativo in (0, 1)) " +
                               ");");

            connection.Execute("CREATE TABLE Movimento ( " +
                "id TEXT(250) PRIMARY KEY," +
                "idContaBancaria TEXT(250) NOT NULL," +                
                "tipoCredito TEXT(3) NOT NULL," +
                "qtdParcelas INTEGER(2) NOT NULL," +                
                "valor REAL NOT NULL," +
                "dataPrimeiroVencimento TEXT(25) NOT NULL," +
                "CHECK(tipoCredito in ('cd', 'cc', 'cpj', 'cpf', 'ci')), " +
                "FOREIGN KEY(idContaBancaria) REFERENCES ContaBancaria(id) " +
                ");");

            connection.Execute("INSERT INTO ContaBancaria(id, conta, nome, saldo, ativo) VALUES('B6BAFC09-6967-ED11-A567-055DFA4A16C9', 123, 'Khale Surtada', 1.111, 1);");
            connection.Execute("INSERT INTO ContaBancaria(id, conta, nome, saldo, ativo) VALUES('FA99D033-7067-ED11-96C6-7C5DFA4A16C9', 456, 'Shuri Trator', 1.222, 1);");
            connection.Execute("INSERT INTO ContaBancaria(id, conta, nome, saldo, ativo) VALUES('382D323D-7067-ED11-8866-7D5DFA4A16C9', 789, 'Safira Morde Tudo', 1.333, 1);");
            connection.Execute("INSERT INTO ContaBancaria(id, conta, nome, saldo, ativo) VALUES('F475F943-7067-ED11-A06B-7E5DFA4A16C9', 741, 'Brutus Bolinha', 1.444 ,0);");
            connection.Execute("INSERT INTO ContaBancaria(id, conta, nome, saldo, ativo) VALUES('BCDACA4A-7067-ED11-AF81-825DFA4A16C9', 852, 'Mamba Dormi Facil', 1.555 ,0);");
            connection.Execute("INSERT INTO ContaBancaria(id, conta, nome, saldo, ativo) VALUES('D2E02051-7067-ED11-94C0-835DFA4A16C9', 963, 'Ragnar Rei das Mantas', 1.666 ,0);");
        }
    }
}
