using SapNwRfc;
using System.Text.Json.Serialization;

namespace SapBroker.Models;

class Order
{
    [SapName("MANDT")]
    public string ComponentName { get; set; }

    [SapName("PSPID")]
    public string ComponentId { get; set; }

    [SapName("JOINT")]
    public string RequestedJobId { get; set; }

    [SapName("DWGNO")]
    public string RequestedJobDescription { get; set; }

    [SapName("PCSNO")]
    public string StudyId { get; set; }

    [SapName("INSPITM")]
    [JsonIgnore]
    public string InternalModality { get; set; }

    public string Modality => InternalModality switch
    {
        "RT" => "SC",
        "DR" => "DX",
        _ => "OT"
    };

    [SapName("FILMID")]
    [JsonIgnore]
    public string FilmId { get; set; }

    [SapName("SER")]
    [JsonIgnore]
    public int FilmSeries { get; set; }

    public string AccessionNumber => FilmSeries > 1 ? $"{FilmId}/{FilmSeries:d3}" : FilmId;

    [SapName("INSP_SHOP")]
    public string ComponentOwnerName { get; set; }

    [SapName("FIX_DAY")]
    [JsonIgnore]
    public DateTime ScheduledDate { get; set; }

    [SapName("FIX_TIME")]
    [JsonIgnore]
    public TimeSpan ScheduledTime { get; set; }

    public DateTime ScheduledDateTime => ScheduledDate + ScheduledTime;
}
