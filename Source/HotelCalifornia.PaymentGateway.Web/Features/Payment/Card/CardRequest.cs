namespace HotelCalifornia.PaymentGateway.Web.Features.Payment.Card
{
    public class CardRequest
    {
        public string IdentifingToken { get; set; }
        public Details Details { get; set; }
    }
}