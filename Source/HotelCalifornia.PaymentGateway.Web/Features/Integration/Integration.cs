using System;
using System.Collections.Generic;

namespace HotelCalifornia.PaymentGateway.Web.Features.Integration
{
    public class IntegrationDto
    {
        public IntegrationDto(Guid uniqueReference, string currency,
            IEnumerable<string> schemes, IEnumerable<string> methods, CardPaymentIntegration cardPaymentIntegration = null)
        {
            this.UniqueReference = uniqueReference;
            this.Currency = currency;
            this.Schemes = schemes;
            this.Methods = methods;
            this.CardPaymentIntegration = cardPaymentIntegration;
        }

        public Guid UniqueReference { get; }
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