using Geolocation.Client;

namespace Geolocation.UI.Data
{
    public partial class AddressViewModel
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public  static implicit operator AddressViewModel(IAddress address)
        {
            return new AddressViewModel(){Date = DateTime.UtcNow};
        }
    }
}