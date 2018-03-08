using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Controllers
{
    public class SessionDataController : Controller
    {
        [HttpPost]
        [Route("session/apple-pay")]
        public async Task<IActionResult> CreateAppleSession([FromBody]AppleSessionRequest sessionRequest)
        {
            using (var client = new HttpClient())
            {
                using (var res = await client.PostAsync("", new StringContent("", Encoding.UTF8, "application/json")))
                {
                    var returnedVal = await res.Content.ReadAsStringAsync();
                    return this.Ok(returnedVal);
                }
            }
        }
    }

    public class AppleSessionRequest
    {
        public string ValidationURL { get; set; }
    }
}
