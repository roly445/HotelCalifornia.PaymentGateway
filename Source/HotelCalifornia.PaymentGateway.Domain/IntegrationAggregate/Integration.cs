using System.Collections.Generic;

namespace HotelCalifornia.PaymentGateway.Domain.IntegrationAggregate
{
    public class Integration
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int MerchantId { get; set; }

        public IList<string> Schemes { get; set; }
        public IList<string> Methods { get; set; }
        public string Currency { get; set; }
        public bool CardEnabled { get; set; }
    }
}
