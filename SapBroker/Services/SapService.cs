using SapBroker.Models;
using SapNwRfc;

namespace SapBroker.Services;

class SapService : ISapService, IDisposable
{
    readonly SapConnection m_Connection;

    public SapService(Settings settings)
    {
        m_Connection = new(settings.SapConnectionString);
        m_Connection.Connect();
    }

    public void Dispose() => m_Connection?.Dispose();

    public IEnumerable<Order> RetrieveOrders(int days)
    {
        using var func = m_Connection.CreateFunction("Z_QMSA_12_0015_GET");
        var today = DateTime.Today;
        var parameters = new OrderParameters
        {
            StartDate = today.AddDays(-days).ToString("yyyyMMdd"),
            EndDate = today.ToString("yyyyMMdd")
        };
        var result = func.Invoke<ArrayResultContainer<Order>>(parameters);
        return result.Result;
    }

    public void NotifyFile(FileParameters parameters)
    {
        using var func = m_Connection.CreateFunction("Z_QMSA_12_0016_SET");
        var result = func.Invoke<ResultContainer<string>>(parameters);
        if (result.Result != "S")
        {
            throw new InvalidOperationException($"{func.Metadata.GetName} returns {result.Result}");
        }
    }
}
