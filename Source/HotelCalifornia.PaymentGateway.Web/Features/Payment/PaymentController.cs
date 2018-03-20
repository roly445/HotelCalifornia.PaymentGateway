using HotelCalifornia.PaymentGateway.Web.Models.Card;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        [Route("payment/card")]
        public IActionResult Card([FromBody]CardRequest cardRequest)
        {
            return this.Ok();
        }

        public IActionResult ApplePay()
        {
            return this.Ok();
        }

        public IActionResult GooglePay()
        {
            return this.Ok();
        }
    }
}