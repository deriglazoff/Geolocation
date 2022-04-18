using Geolocation.Client;

namespace Geolocation.UI.Data
{
    public class WeatherForecastService
    {
        private readonly IGeolocation_Client _client;


        public WeatherForecastService(IGeolocation_Client client)
        {
            _client = client;
        }

        public async Task<List<AddressViewModel>>GetForecastAsync()
        {
            var result = await _client.AddressesGETAsync();
            return result.Select(c=>(AddressViewModel)c).ToList();
        }
    }
}