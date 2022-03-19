using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation.DataAccess.Entities;
using Geolocation.Domain.Interfaces;
using Geolocation.Infrastructure.Saga;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Geolocation.Infrastructure
{
    public class GeolocationContext : SagaDbContext, IGeolocationContext
    {
        public GeolocationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new AddressSagaStateMap(); }
        }

        public DbSet<AddressEntity> Addresses { get; set; }

        public async Task<IList<IAddress>> Get()
        {
            var result = await Addresses.ToListAsync();
            return result.ToList<IAddress>();
        }

        public async Task Insert(IAddress address)
        {
            var entity = new AddressEntity()
            {
                CorrelationId = address.CorrelationId
            };
            await Addresses.AddAsync(entity);
        }
    }
}