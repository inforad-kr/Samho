using SapBroker.Models;
using SapNwRfc;

namespace SapBroker.Services;

class SapService : ISapService, IDisposable
{
    readonly SapConnection m_Connection;
    readonly int m_Days;

    public SapService(Settings settings)
    {
        m_Connection = new(settings.Sap.ConnectionString);
        m_Connection.Connect();
        m_Days = settings.Sap.Days;
    }

    public void Dispose() => m_Connection?.Dispose();

    public IEnumerable<Order> RetrieveOrders()
    {
        using var func = m_Connection.CreateFunction("Z_QMSA_12_0015_GET");
        var today = DateTime.Today;
        var parameters = new OrderParameters
        {
            StartDate = today.AddDays(-m_Days).ToString("yyyyMMdd"),
            EndDate = today.ToString("yyyyMMdd")
        };
        var result = func.Invoke<ResultContainer<Order[]>>(parameters);
        return result.Result;
    }
}
