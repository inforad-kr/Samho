using SapBroker.Models;
using Serilog;

namespace SapBroker.Services;

class XPacsService : IDisposable, IXPacsService
{
    readonly HttpClient m_Client;

    public XPacsService(Settings settings, HttpClient client)
    {
        m_Client = client;
        m_Client.BaseAddress = new(settings.XPacs.Url);
        m_Client.BaseAddress = new(m_Client.BaseAddress, "api/v1/");
        m_Client.DefaultRequestHeaders.Authorization = new("Bearer", settings.XPacs.ApiToken);
    }

    public void Dispose() => m_Client?.Dispose();

    public async Task SendOrders(Order[] orders)
    {
        foreach (var order in orders)
        {
            var existingOrders = await m_Client.GetFromJsonAsync<Order[]>($"order?accessionNumber={order.AccessionNumber}");
            if (existingOrders.Length == 0)
            {
                await m_Client.PostAsJsonAsync("order", order);
                Log.Information("Order {AccessionNumber} sent to XPACS", order.AccessionNumber);
            }
            else
            {
                Log.Warning("Order {AccessionNumber} already exists in XPACS", order.AccessionNumber);
            }
        }
    }
}
