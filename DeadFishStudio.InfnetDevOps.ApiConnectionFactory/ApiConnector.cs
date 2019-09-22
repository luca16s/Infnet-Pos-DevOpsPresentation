using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DeadFishStudio.InfnetDevOps.ApiConnectionFactory.Interfaces;

namespace DeadFishStudio.InfnetDevOps.ApiConnectionFactory
{
    public class ApiConnector : IApiConnector
    {
        public readonly Uri Url;

        public ApiConnector(string url)
        {
            Url = new Uri(url);
        }

        public HttpClient ApiClient()
        {
            var client = new HttpClient {BaseAddress = Url};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
