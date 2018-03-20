using System;
using System.Collections.Generic;
using System.Text;

namespace HotelCalifornia.PaymentGateway.Domain
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
