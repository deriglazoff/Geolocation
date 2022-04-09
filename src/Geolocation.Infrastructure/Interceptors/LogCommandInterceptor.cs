using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Geolocation.Infrastructure.Interceptors
{
    public class LogCommandInterceptor : DbCommandInterceptor
    {
        private readonly ILogger _logger;

        public LogCommandInterceptor(ILogger<LogCommandInterceptor> logger)
        {
            _logger = logger;
        }
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            _logger.LogDebug(command.CommandText);
            return base.ReaderExecuting(command, eventData, result);
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command,
            CommandEventData eventData, InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogDebug(command.CommandText);
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override async Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData,
            CancellationToken cancellationToken = new CancellationToken())
        {
            CommandFailed(command, eventData);
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            throw new Exception("Ошибка БД", eventData.Exception) {Data = {{ "Query", GetGeneratedQuery(command)}}};
        }

        /// <summary>
        /// TODO debug list params
        /// </summary>
        public static string GetGeneratedQuery([NotNull] DbCommand dbCommand)
        {
            return dbCommand.Parameters.Cast<DbParameter>().Aggregate(dbCommand.CommandText,
                (current, parameter) => current.Replace(parameter.ParameterName, parameter.Value?.ToString()));
        }
    }


}