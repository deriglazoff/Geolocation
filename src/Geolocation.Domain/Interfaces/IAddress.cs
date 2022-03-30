using System;
using System.Threading.Tasks;
using Geolocation.Domain.Declare;

namespace Geolocation.Domain.Interfaces
{
    public interface IAddress
    {
        /// <summary>
        /// ID корреляции
        /// </summary>
        public Guid CorrelationId  { get; set; }
        public string Value { get; set; }

        public string UnrestrictedValue { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string KladrId { get; set; }

        public AddressType Type { get; set; }

    }

    public interface IRunner
    {
        public Task Run();

    }
}
