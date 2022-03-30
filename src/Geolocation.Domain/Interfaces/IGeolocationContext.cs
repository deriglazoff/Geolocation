using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocation.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<IList<T>> Get();

        public Task Insert(T entity);

        public Task<IList<T>> GetOld();
    }
}