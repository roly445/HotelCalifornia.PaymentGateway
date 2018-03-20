using HotelCalifornia.PaymentGateway.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotelCalifornia.PaymentGateway.Web.Features.Integration
{
    public class IntegrationController : Controller
    {
        private readonly IIntegrationRepository _integrationRepository;

        public IntegrationController(IIntegrationRepository integrationRepository)
        {
            this._integrationRepository = integrationRepository;
        }

        [HttpPost]
        [Route("integration")]
        public IntegrationDto Integration([FromBody]IntegrationRequestModel requestModel)
        {
            var integration = this._integrationRepository.GetByKey(requestModel.IdentifyingToken);

            return new IntegrationDto(integration.Currency, integration.Schemes,
                integration.Methods, new CardPaymentIntegration(this.Url.Action("CardHost", "CardHost", new { identifyingToken = requestModel.IdentifyingToken}, Request.Scheme)));
        }
    }

    public class IntegrationRequestModel
    {
        public  string IdentifyingToken { get; set; }
    }
}