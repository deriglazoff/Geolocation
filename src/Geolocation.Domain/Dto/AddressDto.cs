using System;
using System.ComponentModel.DataAnnotations;
using Geolocation.Domain.Declare;
using Geolocation.Domain.Interfaces;

namespace Geolocation.Domain.Dto
{
    public class AddressDto : IAddress
    {
        public Guid CorrelationId { get; set; }
        [Required]
        public string Value { get; set; }

        public string UnrestrictedValue { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string KladrId { get; set; }

        public AddressType Type { get; set; }

    }
}
