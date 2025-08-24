using SapBroker.Models;

namespace SapBroker.Services;

interface ISapService
{
    IEnumerable<Order> RetrieveOrders(int days);

    void NotifyFile(FileParameters parameters);
}
