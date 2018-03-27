using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Features.Payment.Card
{
    public class CardPaymentController : Controller
    {
        [HttpPost]
        [Route("payment/card")]
        public IActionResult CardPayment([FromBody]CardRequest cardRequest)
        {

            return this.Ok(new GatewayResponse
            {
                Success = true
            });
        }
    }
}