using SapBroker.Models;

namespace SapBroker.Services;

class SapEmulator : ISapService
{
    public IEnumerable<Order> RetrieveOrders()
    {
        for (var i = 0; i < 200; ++i)
        {
            var filmId = $"{i + 1:d3}";
            var order = new Order
            {
                ComponentName = "Test",
                ComponentId = "111",
                RequestedJobId = $"J{filmId}",
                RequestedJobDescription = $"D{filmId}",
                StudyId = $"P{filmId}",
                InternalModality = "RT",
                FilmId = filmId,
                FilmSeries = 1,
                ComponentOwnerName = "Samho",
                ScheduledDate = DateTime.Today,
                ScheduledTime = TimeSpan.FromMinutes(i + 1)
            };
            yield return order;
        }
    }
}
