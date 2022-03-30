using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Geolocation.Infrastructure
{
    public class LogCommandInterceptor : DbCommandInterceptor
    {
        private readonly ILogger _logger;

        public LogCommandInterceptor(ILogger<LogCommandInterceptor> logger)
        {
            _logger = logger;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            try
            {
                _logger.LogInformation(command.CommandText);
                return base.ReaderExecuting(command, eventData, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                _logger.LogInformation(command.CommandText);
                return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// TODO debug list params
        /// </summary>
        public static string GetGeneratedQuery(DbCommand dbCommand)
        {
            
            var query = dbCommand.CommandText;
            foreach (DbParameter parameter in dbCommand.Parameters)
            {
                query = query.Replace(parameter.ParameterName, parameter.Value.ToString());
            }
            return query;
        }
    }
}