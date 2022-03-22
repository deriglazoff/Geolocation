using System;
using System.ComponentModel.DataAnnotations;
using Geolocation.Domain.Declare;
using Geolocation.Domain.Interfaces;

namespace Geolocation.Infrastructure.Entities
{
    public class AddressTypeEntity 
    {
        [Key]
        public AddressType Id  { get; set; }

        public string Name { get; set; }

    }
}
