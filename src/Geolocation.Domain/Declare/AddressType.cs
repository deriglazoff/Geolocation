namespace Geolocation.Domain.Declare
{
    /// <summary>
    /// Тип адреса
    /// </summary>
    public enum AddressType : byte
    {
        /// <summary>
        /// Адрес регистрации
        /// </summary>
        Registration = 1,
        /// <summary>
        /// фактический адрес проживания 
        /// </summary>
        Home = 2,
        /// <summary>
        /// Адрес места работы
        /// </summary>
        Work = 3
    }

}
