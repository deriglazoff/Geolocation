using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocation.Domain.Interfaces
{
    public interface IGeolocationContext
    {
        public Task<IList<IAddress>> Get();

        public Task Insert(IAddress address);
    }
}