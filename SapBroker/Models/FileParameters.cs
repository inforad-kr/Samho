using SapNwRfc;

namespace SapBroker.Models;

class FileParameters
{
    [SapName("I_PSPID")]
    public string ShipNumber { get; set; }

    [SapName("I_REP_NO")]
    public string ReportNumber { get; set; }

    [SapIgnore]
    public string FilmId_Series { get; set; }

    [SapName("I_FILMID")]
    public string FilmId => FilmId_Series[..^4];

    [SapName("I_SER")]
    public int FilmSeries => int.Parse(FilmId_Series[^3..]);

    [SapName("I_LOC")]
    public string Location { get; set; }

    [SapName("I_VCODE")]
    public string VerificationCode { get; set; }

    [SapName("I_FNAME")]
    public string FileName { get; set; }
}
