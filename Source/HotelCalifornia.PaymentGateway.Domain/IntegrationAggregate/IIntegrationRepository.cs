using System.Collections.Generic;

namespace HotelCalifornia.PaymentGateway.Domain.IntegrationAggregate
{
    public interface IIntegrationRepository
    {
        IReadOnlyList<Integration> GetAll();
        Integration GetById(int id);
        Integration GetByKey(string key);
    }
}