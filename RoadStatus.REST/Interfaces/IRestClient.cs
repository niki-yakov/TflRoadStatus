using System.Net;

namespace RoadStatus.REST.Interfaces
{
    public interface IRestClient
    {
        HttpStatusCode StatusCode { get; }
        string Get(string road);
    }
}