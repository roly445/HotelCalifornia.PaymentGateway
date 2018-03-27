using System;

namespace HotelCalifornia.PaymentGateway.Domain.PaymentSessionAggregate
{
    public class PaymentSession
    {
        public int Id { get; set; }
        public int IntegrationId { get; set; }
        public Guid UniqueReference { get; set; }
        public DateTime WhenStarted { get; set; }
    }
}
