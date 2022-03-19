using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocation.Infrastructure.Saga
{
    public class AddressSagaStateMap : SagaClassMap<AddressSaga>
    {
        protected override void Configure(EntityTypeBuilder<AddressSaga> entity, ModelBuilder model)
        {
            entity.ToTable(GetType().Name);
        }
    }
}