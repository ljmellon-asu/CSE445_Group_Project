using System.ServiceModel;
using System.ServiceModel.Web;

namespace Top10ContentWordsService
{
    [ServiceContract]
    public interface ITop10Service
    {
        [OperationContract]
        [WebGet(UriTemplate = "Top10ContentWords?url={url}", ResponseFormat = WebMessageFormat.Json)]
        string[] Top10ContentWords(string url);
    }
}