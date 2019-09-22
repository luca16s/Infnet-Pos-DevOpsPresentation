using System;
using System.Net;
using System.Threading.Tasks;

namespace DeadFishStudio.InfnetDevOps.ApiConnectionFactory.Interfaces
{
    public interface IApiActions
    {
        Task<Uri> Create(string path, string entity);
        Task<string> Get(string path);
        Task<string> Update(string path, string id, string entity);
        Task<HttpStatusCode> Delete(string path, string id);
    }
}
