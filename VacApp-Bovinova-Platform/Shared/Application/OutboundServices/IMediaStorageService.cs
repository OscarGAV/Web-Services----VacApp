namespace VacApp_Bovinova_Platform.Shared.Application.OutboundServices;

public interface IMediaStorageService
{
    string UploadFileAsync(string fileName, Stream fileData);
    void UpdateFileAsync(string url, Stream fileData);
}