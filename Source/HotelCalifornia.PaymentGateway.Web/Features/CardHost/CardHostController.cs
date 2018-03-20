using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelCalifornia.PaymentGateway.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Features.CardHost
{
    public class CardHostController : Controller
    {
        private readonly IIntegrationRepository _integrationRepository;

        public CardHostController(IIntegrationRepository integrationRepository)
        {
            this._integrationRepository = integrationRepository;
        }

        [HttpGet]
        [Route("~/card-host")]
        public IActionResult CardHost(string identifyingToken)
        {
            var integration = this._integrationRepository.GetByKey(identifyingToken);

            var model = new CodeHostViewModel
            {
                CardPaymentEnabled = integration.CardEnabled,
                Currency = integration.Currency,
                Schemes = integration.Schemes,
                Methods = integration.Methods

            };

            return this.View(model);
        }


    }

    public class CodeHostViewModel
    {
        public bool CardPaymentEnabled { get; set; }
        public string Currency { get; set; }
        public IEnumerable<string> Schemes { get; set; }
        public IEnumerable<string> Methods { get; set; }
    }
}
