using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation.Domain.Declare;
using Geolocation.Domain.Interfaces;
using Geolocation.Infrastructure.Entities;
using Geolocation.Infrastructure.Saga;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public DbSet<AddressTypeEntity> AddressTypes { get; set; }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AddressTypeEntity>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Entity<AddressTypeEntity>().HasData(new List<AddressTypeEntity>
            {
                new()
                {
                    Id = AddressType.Home,
                    Name = AddressType.Home.ToString(),
                },
            });
        }
    }
}