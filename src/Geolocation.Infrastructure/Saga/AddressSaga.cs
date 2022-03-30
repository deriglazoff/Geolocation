using System;
using Automatonymous;
using Geolocation.Domain.Declare;
using Geolocation.Domain.Interfaces;
using MassTransit;

namespace Geolocation.Infrastructure.Saga
{
    /// <summary>
    /// Сага
    /// </summary>
    public class AddressSaga : SagaStateMachineInstance, IAddress, CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }

        public string Value { get; set; }
        public string UnrestrictedValue { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string KladrId { get; set; }
        public AddressType Type { get; set; }

        public string CurrentState { get; set; }

        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }
    }

}