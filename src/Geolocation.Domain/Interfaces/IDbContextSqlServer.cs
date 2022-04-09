using System.Threading.Tasks;

namespace Geolocation.Domain.Interfaces
{
    public interface IDbContextSqlServer
    {
        public Task<int> NewAddress(IAddress address);
    }
}