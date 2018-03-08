using System.Collections.Generic;

namespace HotelCalifornia.PaymentGateway.Web.Models
{
    public class Integration
    {
        public Integration(bool applePayEnabled, bool googlePayEnabled, bool cardPaymentEnabled, string currency,
            IEnumerable<string> schemes, IEnumerable<string> methods, GooglePayConfiguration googlePayConfiguration)
        {
            this.ApplePayEnabled = applePayEnabled;
            this.GooglePayEnabled = googlePayEnabled;
            this.Currency = currency;
            this.Schemes = schemes;
            this.Methods = methods;
            this.GooglePayConfiguration = googlePayConfiguration;
            this.CardPaymentEnabled = cardPaymentEnabled;
        }

        public bool ApplePayEnabled { get; }
        public bool GooglePayEnabled { get; }
        public bool CardPaymentEnabled { get; }
        public string Currency { get; }
        public IEnumerable<string> Schemes { get; }
        public IEnumerable<string> Methods { get; }
        public GooglePayConfiguration GooglePayConfiguration { get; }
    }

    public class GooglePayConfiguration
    {
        public GooglePayConfiguration(string environment, int apiVersion, string gateway, string gatewayMerchantId)
        {
            this.Environment = environment;
            this.ApiVersion = apiVersion;
            this.Gateway = gateway;
            this.GatewayMerchantId = gatewayMerchantId;
        }

        public string Environment { get; }
        public int ApiVersion { get; }
        public string Gateway { get; }
        public string GatewayMerchantId { get; }
    }
}