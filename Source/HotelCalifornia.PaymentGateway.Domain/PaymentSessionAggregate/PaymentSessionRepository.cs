using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HotelCalifornia.PaymentGateway.Domain.PaymentSessionAggregate
{
    public class PaymentSessionRepository : IPaymentSessionRepository
    {
        private static List<PaymentSession> _paymentSessions;

        public PaymentSessionRepository()
        {
            if (_paymentSessions == null)
                _paymentSessions = new List<PaymentSession>();
        }
        public PaymentSession GetByUniqueReference(Guid uniqueReference)
        {
            return _paymentSessions.SingleOrDefault(x => x.UniqueReference == uniqueReference);
        }

        public void Create(PaymentSession paymentSession)
        {
            _paymentSessions.Add(paymentSession);
        }
    }

    public interface IPaymentSessionRepository
    {
        PaymentSession GetByUniqueReference(Guid uniqueReference);
        void Create(PaymentSession paymentSession);
    }
}
