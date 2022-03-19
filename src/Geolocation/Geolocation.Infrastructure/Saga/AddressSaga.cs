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
        /// <summary>
        /// ID корреляции
        /// </summary>
        public Guid CorrelationId { get; set; }

        public string Value { get; set; }
        public string UnrestrictedValue { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string KladrId { get; set; }
        public AddressType Type { get; set; }

        /// <summary>
        /// Текущее состояние
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public string Description { get; set; }

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