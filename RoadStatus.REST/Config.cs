
using RoadStatus.REST.Interfaces;
using System.Collections.Specialized;
using System.Configuration;

namespace RoadStatus.REST
{
    public class Config : IConfig
    {
        public Config()
        {
            var settings = (NameValueCollection)ConfigurationManager.GetSection("rsAppSettings");

            this.Url = settings["url"]?.ToString() ?? "";
            this.AppID = settings["app_id"]?.ToString() ?? "";
            this.AppKey = settings["app_key"]?.ToString() ?? "";
        }
        public string Url { get; set; }
        public string AppID { get; set; }
        public string AppKey { get; set; }
    }
}