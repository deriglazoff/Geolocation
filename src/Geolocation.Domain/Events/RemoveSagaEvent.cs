using System;

namespace Geolocation.Domain.Events
{
    public class RemoveSagaEvent
    {
        public Guid CorrelationId { get; set; }
    }
}