using Geolocation.Client;
using Geolocation.UI.ModelsView;

namespace Geolocation.UI.Data
{
    public class AddressService
    {
        private readonly IGeolocation_Client _client;


        public AddressService(IGeolocation_Client client)
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