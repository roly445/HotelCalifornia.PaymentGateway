using System;
using System.Collections.Generic;

namespace HotelCalifornia.PaymentGateway.Web.Features.CardHost
{
    public class CodeHostViewModel
    {
        public Guid IdentifyingToken { get; set; }
        public string SriHash { get; set; }
    }
}