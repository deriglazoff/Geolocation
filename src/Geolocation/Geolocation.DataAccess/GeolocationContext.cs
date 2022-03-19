using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Geolocation.DataAccess
{
    public class GeolocationContext : DbContext, IGeolocationContext
    {
        public GeolocationContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }

        public async Task<IList<IAddress> > Get()
        {
            var a =  await Addresses.ToListAsync();
            return a.ToList<IAddress>();
        }

        public async Task Insert(IAddress address)
        {
            //await Addresses.AddAsync(address);
        }
    }
}