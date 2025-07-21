using SapBroker.Models;

namespace SapBroker.Services;

interface IXPacsService
{
    Task SendOrders(Order[] orders);
}
