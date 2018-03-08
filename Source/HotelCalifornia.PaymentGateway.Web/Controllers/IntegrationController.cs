using HotelCalifornia.PaymentGateway.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Controllers
{
    public class IntegrationController : Controller
    {
        [HttpPost]
        [Route("integration")]
        public Integration Get(string identifyingToken)
        {
            return new Integration(true, true, true, "GBP", new[] { "visa", "mastercard", "amex" },
                new[] {""}, new GooglePayConfiguration("Test", 1, "example", "123"));
        }
    }
}