using Geolocation.Client;

namespace Geolocation.UI.Data;

public class GeolocationClientTest: IGeolocation_Client
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public Task<ICollection<IAddress>> AddressesGETAsync()
    {
        ICollection<IAddress> result = Summaries
            .Select(c => new IAddress {Value = c, CorrelationId = Guid.NewGuid()})
            .ToList();
        return Task.FromResult(result);
    }

    public Task<ICollection<IAddress>> AddressesGETAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddressesPOSTAsync(AddressDto body)
    {
        throw new NotImplementedException();
    }

    public Task AddressesPOSTAsync(AddressDto body, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AddressDto> GeolocationGETAsync(double? lat, double? lon)
    {
        throw new NotImplementedException();
    }

    public Task<AddressDto> GeolocationGETAsync(double? lat, double? lon, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}