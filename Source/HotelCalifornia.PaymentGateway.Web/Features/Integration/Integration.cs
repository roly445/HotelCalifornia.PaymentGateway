using System.Collections.Generic;

namespace HotelCalifornia.PaymentGateway.Web.Features.Integration
{
    public class IntegrationDto
    {
        public IntegrationDto(string currency,
            IEnumerable<string> schemes, IEnumerable<string> methods, CardPaymentIntegration cardPaymentIntegration = null)
        {
            this.Currency = currency;
            this.Schemes = schemes;
            this.Methods = methods;
            this.CardPaymentIntegration = cardPaymentIntegration;
        }

        public bool CardPaymentEnabled => this.CardPaymentIntegration != null;
        public string Currency { get; }
        public IEnumerable<string> Schemes { get; }
        public IEnumerable<string> Methods { get; }

        public CardPaymentIntegration CardPaymentIntegration { get; }
    }

    public class CardPaymentIntegration
    {
        public CardPaymentIntegration(string hostedUrl)
        {
            this.HostedUrl = hostedUrl;
        }

        public string HostedUrl { get; }
    }
}