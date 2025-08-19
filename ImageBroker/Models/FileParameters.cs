using SapNwRfc;

namespace ImageBroker.Models;

class FileParameters
{
    [SapName("I_PSPID")]
    public string ComponentId { get; set; }

    [SapName("I_REP_NO")]
    public string StudyId { get; set; }

    [SapIgnore]
    public string AccessionNumber { get; set; }

    [SapName("I_FILMID")]
    public string FilmId => AccessionNumber[..^4];

    [SapName("I_SER")]
    public int FilmSeries => int.Parse(AccessionNumber[^3..]);

    [SapName("I_LOC")]
    public string SeriesDescription { get; set; }

    [SapName("I_VCODE")]
    public string ProtocolName { get; set; }

    [SapName("I_FNAME")]
    public string FileName { get; set; }
}
