using System.ComponentModel.DataAnnotations;
using Geolocation.Client;

namespace Geolocation.UI.ModelsView
{
    public class AddressViewModel
    {
        [Key]
        public Guid? CorrelationId { get; set; }

        public DateTime Date { get; set; }

        public string Value { get; set; }


        public  static implicit operator AddressViewModel(IAddress address)
        {
            return new AddressViewModel()
            {
                CorrelationId = address.CorrelationId,
                Date = DateTime.UtcNow,
                Value = address.Value
            };
        }
    }
}