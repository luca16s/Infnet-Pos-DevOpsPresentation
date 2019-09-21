using System.Net.Http;

namespace DeadFishStudio.InfnetDevOps.ApiConnectionFactory.Interfaces
{
    public interface IApiConnector
    {
        HttpClient ApiClient();
    }
}
