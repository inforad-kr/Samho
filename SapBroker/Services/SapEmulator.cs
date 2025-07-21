using SapBroker.Models;

namespace SapBroker.Services;

class SapEmulator : ISapService
{
    public Order[] RetrieveOrders() =>
    [
        new()
        {
            ComponentName = "Test",
            ComponentId = "111",
            RequestedJobId = "J423",
            RequestedJobDescription = "D352867",
            StudyId = "P6256",
            InternalModality = "RT",
            FilmId = "12345",
            FilmSeries = 1,
            ComponentOwnerName = "Samho",
            ScheduledDate = DateTime.Today,
            ScheduledTime = TimeSpan.FromHours(9)
        }
    ];
}
