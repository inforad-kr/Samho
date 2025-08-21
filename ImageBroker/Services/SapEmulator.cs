using ImageBroker.Models;
using Serilog;

namespace ImageBroker.Services;

class SapEmulator : ISapService
{
    public void NotifyFile(FileParameters parameters)
    {
        Log.Information("Notify SAP about file {Parameters}", new
        {
            parameters.ShipNumber,
            parameters.ReportNumber,
            parameters.FilmId,
            parameters.FilmSeries,
            parameters.Location,
            parameters.VerificationCode,
            parameters.FileName
        });
    }
}
