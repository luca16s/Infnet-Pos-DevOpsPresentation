using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DeadFishStudio.InfnetDevOps.ApiConnectionFactory.Interfaces;

namespace DeadFishStudio.InfnetDevOps.ApiConnectionFactory
{
    public class ApiActions : IApiActions
    {
        public HttpClient Connector { get; }

        public ApiActions(ApiConnector apiConnector)
        {
            Connector = apiConnector.ApiClient();
        }

        public async Task<Uri> Create(string path, string entity)
        {
            var response = await Connector.PostAsJsonAsync(path, entity);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task<string> Get(string path)
        {
            var result = string.Empty;
            var response = await Connector.GetAsync(path);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> Update(string path, string id, string entity)
        {
            var response = await Connector.PutAsJsonAsync($"{path}{id}", entity);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpStatusCode> Delete(string path, string id)
        {
            var response = await Connector.DeleteAsync($"{path}{id}");

            return response.StatusCode;
        }
    }
}
