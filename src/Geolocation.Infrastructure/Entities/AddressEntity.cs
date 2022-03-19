using System;
using System.ComponentModel.DataAnnotations;
using Geolocation.Domain.Declare;
using Geolocation.Domain.Interfaces;

namespace Geolocation.DataAccess.Entities
{
    public class AddressEntity : IAddress
    {
        [Key]
        public Guid CorrelationId  { get; set; }
        public string Value { get; set; }

        public string UnrestrictedValue { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string KladrId { get; set; }

        public AddressType Type { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime DateUpdate { get; set; }

    }
}
