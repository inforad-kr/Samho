using ImageBroker.Models;

namespace ImageBroker.Services;

interface ISapService
{
    void NotifyFile(FileParameters parameters);
}
