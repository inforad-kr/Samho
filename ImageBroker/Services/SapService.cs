using ImageBroker.Models;
using SapNwRfc;

namespace ImageBroker.Services;

class SapService : ISapService, IDisposable
{
    readonly SapConnection m_Connection;

    public SapService(Settings settings)
    {
        m_Connection = new(settings.SapConnectionString);
        m_Connection.Connect();
    }

    public void Dispose() => m_Connection?.Dispose();

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
