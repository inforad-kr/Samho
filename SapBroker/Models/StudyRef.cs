namespace SapBroker.Models;

record StudyRef(string Ship, string Report, string FilmId, int Ser)
{
    public bool IsValid => !string.IsNullOrWhiteSpace(Ship) && !string.IsNullOrWhiteSpace(Report) && !string.IsNullOrWhiteSpace(FilmId) && Ser > 0;

    public static ValueTask<StudyRef> BindAsync(HttpContext context)
    {
        var query = context.Request.Query;
        var obj = new StudyRef(query[nameof(Ship)], query[nameof(Report)], query[nameof(FilmId)], int.TryParse(query[nameof(Ser)], out var result) ? result : 0);
        return ValueTask.FromResult(obj);
    }
}
