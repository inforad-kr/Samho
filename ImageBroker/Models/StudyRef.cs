namespace ImageBroker.Models;

record StudyRef(string ShipNumber, string ReportNumber, string FilmId_Series)
{
    public bool IsValid => !string.IsNullOrWhiteSpace(ShipNumber) && !string.IsNullOrWhiteSpace(ReportNumber) && !string.IsNullOrWhiteSpace(FilmId_Series);
}
