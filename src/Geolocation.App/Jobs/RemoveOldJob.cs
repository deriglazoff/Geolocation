using System;
using System.Linq;
using System.Threading.Tasks;
using Geolocation.Domain.Events;
using Geolocation.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Geolocation.App.Jobs
{
    [DisallowConcurrentExecution]
    public class RemoveOldJob : IJob, IRunner
    {
        private readonly ILogger _logger;

        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IRepository<IAddress> _repository;

        public RemoveOldJob(IPublishEndpoint publishEndpoint, ILogger<RemoveOldJob> logger, IRepository<IAddress> repository)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _repository = repository;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                await Run();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Ошибка при обработке пакета операций");
            }

        }

        public async Task Run()
        {
            var list = await _repository.GetOld();

            var tasks = list.Select(message =>
                    Task.Run(() => _publishEndpoint.Publish<RemoveSagaEvent>(message)))
                .ToList();

            await Task.WhenAll(tasks);
        }
    }
}