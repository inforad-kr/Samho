using ImageBroker.Models;
using Serilog;

namespace ImageBroker.Services;

class SapEmulator : ISapService
{
    public void NotifyFile(FileParameters parameters)
    {
        Log.Information("Notify SAP about file {Parameters}", new
        {
            parameters.ComponentId,
            parameters.StudyId,
            parameters.FilmId,
            parameters.FilmSeries,
            parameters.SeriesDescription,
            parameters.ProtocolName,
            parameters.FileName
        });
    }
}
