using SapNwRfc;

namespace SapBroker.Models;

class OrderParameters
{
    [SapName("I_FRDATE")]
    public string StartDate { get; set; }

    [SapName("I_TODATE")]
    public string EndDate { get; set; }
}
