using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCalifornia.PaymentGateway.Web.Models.Card
{
    public class CardRequest
    {
        public string IdentifingToken { get; set; }
        public Details Details { get; set; }
    }
    public class Details
    {
        public Billingaddress BillingAddress { get; set; }
        public string CardNumber { get; set; }
        public string CardSecurityCode { get; set; }
        public string CardholderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
    public class Billingaddress
    {
        public string[] AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string DependentLocality { get; set; }
        public string LanguageCode { get; set; }
        public string Organization { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Recipient { get; set; }
        public string Region { get; set; }
        public string SortingCode { get; set; }
    }
}
