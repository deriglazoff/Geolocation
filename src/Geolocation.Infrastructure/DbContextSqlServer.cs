using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Geolocation.Domain.Interfaces;
using Geolocation.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Geolocation.Infrastructure
{
    public sealed class DbContextSqlServer : IDbContextSqlServer
    {
        private readonly string _connectionString;
        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
        public DbContextSqlServer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(GetType().Name);

        }

        public async Task<int> NewAddress(IAddress address)
        {
            var parameters = new AddressEntity
            {
                Value = address.Value
            };
            using var connection = CreateConnection();

            var paymentParams = await QueryAsync(connection, "Geolocation_Address_add", parameters,
                commandType: CommandType.StoredProcedure);
            return paymentParams.First();
        }


        public static async Task<IEnumerable<dynamic>> QueryAsync(IDbConnection connection,
            string commandText,
            object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
            => await QueryAsync<dynamic>(connection, commandText, parameters, transaction, commandTimeout,
                commandType, flags, cancellationToken);


        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="commandText">The SQL to execute for the query.</param>
        /// <param name="parameters">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string commandText,
            object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            var command = new CommandDefinition(commandText, parameters, transaction, commandTimeout, commandType,
                flags, cancellationToken);
            try
            {
                return await connection.QueryAsync<T>(command);
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка БД ({e.Message})", e) {Data = {{"Query", command}}};
            }

        }

    }


}