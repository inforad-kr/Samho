using SapBroker.Models;
using Serilog;

namespace SapBroker.Services;

class SapEmulator : ISapService
{
    public IEnumerable<Order> RetrieveOrders(int days)
    {
        for (var i = 0; i < 20; ++i)
        {
            var filmId = $"{i + 1:d3}";
            var order = new Order
            {
                ComponentId = "111",
                RequestedJobId = $"J{filmId}",
                RequestedJobDescription = $"D{filmId}",
                StudyId = $"P{filmId}",
                StudyDescription = $"R{filmId}",
                InternalModality = "RT",
                FilmId = filmId,
                FilmSeries = 1,
                ComponentManufacturingDate = DateTime.Today.AddYears(-1),
                ComponentOwnerName = "Samho",
                StudyDate = DateTime.Today,
                StudyTime = TimeSpan.FromMinutes(i + 1)
            };
            yield return order;
        }
    }

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
