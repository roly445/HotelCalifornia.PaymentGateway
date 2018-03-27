using System;
using HotelCalifornia.PaymentGateway.Domain;
using HotelCalifornia.PaymentGateway.Domain.IntegrationAggregate;
using HotelCalifornia.PaymentGateway.Domain.PaymentSessionAggregate;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Features.Integration
{
    public class IntegrationController : Controller
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IPaymentSessionRepository _paymentSessionRepository;

        public IntegrationController(IIntegrationRepository integrationRepository, IPaymentSessionRepository paymentSessionRepository)
        {
            this._integrationRepository = integrationRepository;
            this._paymentSessionRepository = paymentSessionRepository;
        }

        [HttpPost]
        [Route("integration")]
        public IntegrationDto Integration([FromBody]IntegrationRequestModel requestModel)
        {
            var integration = this._integrationRepository.GetByKey(requestModel.IdentifyingToken);

            var paymentSession = new PaymentSession
            {
                IntegrationId = integration.Id,
                UniqueReference = Guid.NewGuid(),
                WhenStarted = DateTime.UtcNow
            };

            this._paymentSessionRepository.Create(paymentSession);

            return new IntegrationDto(paymentSession.UniqueReference,integration.Currency, integration.Schemes,
                integration.Methods, new CardPaymentIntegration(this.Url.Action("CardHost", "CardHost", new { identifyingToken = requestModel.IdentifyingToken}, Request.Scheme)));
        }
    }

    public class IntegrationRequestModel
    {
        public  string IdentifyingToken { get; set; }
    }
}