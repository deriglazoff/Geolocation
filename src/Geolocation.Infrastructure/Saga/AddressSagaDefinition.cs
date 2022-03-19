using System;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;

namespace Geolocation.Infrastructure.Saga
{
    public class AddressSagaDefinition : SagaDefinition<AddressSaga>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<AddressSaga> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)));
            //TODO: rm rate limit
            endpointConfigurator.UseRateLimit(5, TimeSpan.FromSeconds(1));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}