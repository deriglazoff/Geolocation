using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;

namespace Geolocation.App.Example
{
    /// <summary>
    /// TODO Remove test object
    /// </summary>
    public class SuggestClientTest : ISuggestClientAsync
    {
        public Task<SuggestResponse<Address>> SuggestAddress(string query, int count = 5, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Address>> SuggestAddress(SuggestAddressRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Address>> FindAddress(string query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Address>> FindAddress(FindAddressRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Address>> Geolocate(double lat, double lon, int radius_meters = 100, int count = 5,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = new SuggestResponse<Address>
            {
                suggestions = new List<Suggestion<Address>>
                {
                    new()
                    {
                        data = new Address
                        {
                            postal_code = "309502",
                            country = "Россия",
                        },
                        unrestricted_value = "Белгородская обл, г Старый Оскол, мкр Дубрава квартал 3, д 3",
                        value = "309502, Белгородская обл, г Старый Оскол, мкр Дубрава квартал 3, д 3"
                    },
                    new()
                    {
                        data = new Address
                        {
                            postal_code = "309502",
                            country = "Россия",
                        },
                        unrestricted_value = "Белгородская обл, г Старый Оскол, мкр Дубрава квартал 3, д 6 к б",
                        value = "309502, Белгородская обл, г Старый Оскол, мкр Дубрава квартал 3, д 6 к б"
                    }

                }
            };
            return Task.FromResult(result);
        }

        public Task<SuggestResponse<Address>> Geolocate(GeolocateRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IplocateResponse> Iplocate(string ip, string language = "ru", CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Bank>> SuggestBank(string query, int count = 5, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Bank>> SuggestBank(SuggestBankRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Bank>> FindBank(string query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Bank>> FindBank(FindBankRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Email>> SuggestEmail(string query, int count = 5, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Email>> SuggestEmail(SuggestRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<FiasAddress>> SuggestFias(string query, int count = 5, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<FiasAddress>> SuggestFias(SuggestAddressRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<FiasAddress>> FindFias(string query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<FiasAddress>> FindFias(SuggestRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Fullname>> SuggestName(string query, int count = 5, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Fullname>> SuggestName(SuggestNameRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Party>> SuggestParty(string query, int count = 5, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Party>> SuggestParty(SuggestPartyRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Party>> FindParty(string query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Party>> FindParty(FindPartyRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Party>> FindAffiliated(string query, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<SuggestResponse<Party>> FindAffiliated(FindAffiliatedRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}
