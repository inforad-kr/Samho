namespace ImageBroker.Models;

record StudyRef(string Ship, string Report, string FilmId, int Ser)
{
    public bool IsValid => !string.IsNullOrWhiteSpace(Ship) && !string.IsNullOrWhiteSpace(Report) && !string.IsNullOrWhiteSpace(FilmId) && Ser > 0;
}
