using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCalifornia.PaymentGateway.Domain
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private static readonly List<Integration> Integrations = new List<Integration>
        {
            new Integration
            {
                Id = 1,
                Key = "tk_5dH4fhd5ggd6",
                MerchantId = 1,
                Methods = new List<string>
                {
                    "credit",
                    "debit"
                },
                Schemes = new List<string>
                {
                    "visa",
                    "mastercard",
                    "amex"
                },
                Currency = "GBP",
                CardEnabled = true
            }
        };
        public IReadOnlyList<Integration> GetAll()
        {
            return Integrations.AsReadOnly();
        }

        public Integration GetById(int id)
        {
            return Integrations.SingleOrDefault(x => x.Id == id);
        }

        public Integration GetByKey(string key)
        {
            return Integrations.SingleOrDefault(x => x.Key == key);
        }

    }
}
