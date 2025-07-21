using SapNwRfc;
using System.Text.Json.Serialization;

namespace SapBroker.Models;

class Order
{
    [SapIgnore]
    public string ComponentName => "?";

    [SapName("PSPID")]
    public string ComponentId { get; set; }

    [SapName("JOINT")]
    public string RequestedJobId { get; set; }

    [SapName("DWGNO")]
    public string RequestedJobDescription { get; set; }

    [SapName("PCSNO")]
    public string StudyId { get; set; }

    [SapName("REP_NO")]
    public string StudyDescription { get; set; }

    [SapName("INSPITM")]
    [JsonIgnore]
    public string InternalModality { get; set; }

    [SapIgnore]
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

    [SapIgnore]
    public string AccessionNumber => FilmSeries > 1 ? $"{FilmId}/{FilmSeries:d3}" : FilmId;

    [SapName("FIX_DAY")]
    public DateTime ComponentManufacturingDate { get; set; }

    [SapName("INSP_SHOP")]
    public string ComponentOwnerName { get; set; }

    [SapName("REAL_DAY")]
    [JsonIgnore]
    public DateTime ScheduledDate { get; set; }

    [SapName("REAL_TIME")]
    [JsonIgnore]
    public TimeSpan ScheduledTime { get; set; }

    [SapIgnore]
    public DateTime ScheduledDateTime => ScheduledDate + ScheduledTime;
}
