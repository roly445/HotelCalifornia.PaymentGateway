namespace HotelCalifornia.PaymentGateway.Web.Features.Payment.Card
{
    public class Details
    {
        public Billingaddress BillingAddress { get; set; }
        public string CardNumber { get; set; }
        public string CardSecurityCode { get; set; }
        public string CardholderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}