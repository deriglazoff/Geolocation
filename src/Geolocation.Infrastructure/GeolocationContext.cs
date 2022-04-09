using System;
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

namespace Geolocation.Infrastructure
{
    public class GeolocationContext : SagaDbContext, IRepository<IAddress>
    {
        public GeolocationContext(DbContextOptions<GeolocationContext> options)
            : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new AddressSagaStateMap(); }
        }

        public DbSet<AddressSaga> AddressSagas { get; set; }

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
            await SaveChangesAsync();
        }

        public async Task<IList<IAddress>> GetOld()
        {
            var result = await AddressSagas.Where(e => e.DateUpdate < DateTime.UtcNow.AddDays(-1)).ToListAsync();
            return result.ToList<IAddress>();
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AddressTypeEntity>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Entity<AddressTypeEntity>().HasData(
                Enum.GetValues(typeof(AddressType)).Cast<AddressType>().ToList().Select(addressType =>
                    new AddressTypeEntity
                    {
                        Id = addressType,
                        Name = addressType.ToString()
                    }).ToList()
            );
        }

    }
}