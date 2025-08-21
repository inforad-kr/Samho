using FluentFTP;
using ImageBroker.Models;
using Serilog;

namespace ImageBroker.Services;

class UploadService(ISapService sapService, IHttpClientFactory httpClientFactory, Settings settings)
{
    readonly HttpClient m_PacsClient = httpClientFactory.CreateClient("Pacs");

    public async Task UploadImages(Study study)
    {
        var images = await GetImages(study);
        foreach (var image in images)
        {
            var files = await GetFiles(image);
            foreach (var file in files)
            {
                var data = await GetFileData(file, false);
                await UploadFile(study, image, false, data);
                data = await GetFileData(file, true);
                await UploadFile(study, image, true, data);
            }
        }
    }

    private async Task<Image[]> GetImages(Study study) =>
        await m_PacsClient.GetFromJsonAsync<Image[]>($"image?ComponentId={study.ComponentId}&StudyId={study.StudyId}&AccessionNumber={study.AccessionNumber}");

    private async Task<ImageFile[]> GetFiles(Image image) =>
        await m_PacsClient.GetFromJsonAsync<ImageFile[]>($"image/{image.Id}/file");

    private async Task<byte[]> GetFileData(ImageFile file, bool jpeg) =>
        await m_PacsClient.GetByteArrayAsync($"file/{file.Id}/data?jpeg={jpeg}");

    private async Task UploadFile(Study study, Image image, bool jpeg, byte[] data)
    {
        using var ftpClient = settings.FtpUser != "" && settings.FtpPassword != "" ?
            new AsyncFtpClient(settings.FtpHost, settings.FtpUser, settings.FtpPassword) :
            new AsyncFtpClient(settings.FtpHost);
        await ftpClient.Connect();

        var folderPath = $"/ndtfiles/images/{study.ComponentId}";
        var fileExtension = jpeg ? "JPG" : "DCM";
        var fileName = $"{study.ComponentId}_{study.AccessionNumber}_{image.SeriesDescription}.{fileExtension}";
        var filePath = $"{folderPath}/{fileName}";

        if (!await ftpClient.DirectoryExists(folderPath))
        {
            await ftpClient.CreateDirectory(folderPath);
            Log.Information("Directory {FolderPath} created by FTP", folderPath);
        }
        using var dataStream = new MemoryStream(data);
        var status = await ftpClient.UploadStream(dataStream, filePath);
        Log.Information("File {FilePath} uploaded by FTP with status {Status}", filePath, status);

        var fileParameters = new FileParameters
        {
            ComponentId = study.ComponentId,
            StudyId = study.StudyId,
            AccessionNumber = study.AccessionNumber[4..],
            SeriesDescription = image.SeriesDescription,
            ProtocolName = image.ProtocolName,
            FileName = fileName
        };
        sapService.NotifyFile(fileParameters);
    }
}
