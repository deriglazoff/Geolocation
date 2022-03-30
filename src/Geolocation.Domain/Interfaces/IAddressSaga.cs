using System;

namespace Geolocation.Domain.Interfaces
{
    public interface IAddressSaga : IAddress
    {
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