using RoadStatus.REST.Interfaces;
using System;
using System.Net;
using System.Net.Http;

namespace RoadStatus.REST.HttpRestClient
{
    public class RestClient : IRestClient
    {
        private readonly IConfig config;
        private readonly HttpClient httpClient;
        
        public HttpStatusCode StatusCode { get; private set; }

        public RestClient(IConfig config, HttpClient httpClient)
        {
            this.config = config;
            this.httpClient = httpClient;
        }

        public string Get(string road)
        {
            string content = string.Empty;

            using (httpClient)
            {
                var requestUrl = CreateUrl(road);

                var httpContent = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpResponseMessage response = httpClient.SendAsync(httpContent).Result;

                this.StatusCode = response.StatusCode;

                if (response.StatusCode != HttpStatusCode.OK && 
                    response.StatusCode != HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException($"Error request http status code {response.StatusCode}");
                }

                content = response.Content.ReadAsStringAsync().Result;
            }

            return content;
        }

        private string CreateUrl(string road)
        {
            return string.Format(config.Url + "{0}?app_id={1}&app_key={2}", road, config.AppID, config.AppKey);
        }
    }
}
