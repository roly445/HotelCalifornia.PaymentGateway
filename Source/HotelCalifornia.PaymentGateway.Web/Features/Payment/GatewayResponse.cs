namespace HotelCalifornia.PaymentGateway.Web.Features.Payment
{
    public class GatewayResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}