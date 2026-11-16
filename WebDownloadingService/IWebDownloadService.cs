using System.ServiceModel;

namespace WebDownloadingService
{
    [ServiceContract]
    public interface IWebDownloadService
    {
        [OperationContract]
        string WebDownload(string url);
    }
}