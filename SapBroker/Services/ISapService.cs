using SapBroker.Models;

namespace SapBroker.Services;

interface ISapService
{
    Order[] RetrieveOrders();
}
