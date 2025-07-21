namespace SapBroker;

class Settings
{
    public SapSettings Sap { get; set; }

    public XPacsSettings XPacs { get; set; }

    public class SapSettings
    {
        public string ConnectionString { get; set; }

        public int Days { get; set; }
    }

    public class XPacsSettings
    {
        public string Url { get; set; }

        public string ApiToken { get; set; }
    }
}
