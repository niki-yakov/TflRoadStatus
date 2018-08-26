using Newtonsoft.Json;
using RoadStatus.REST.Interfaces;
using RoadStatus.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus.REST
{
    public class RoadStatusValidator : IRoadStatusValidator
    {
        private readonly IRestClient restClient;
        public IPrint Print { get; }

        public CorridorStatus CorridorStatus { get; set; }
        public NotFoundError HttpNotFoundError { get; set; }

        public RoadStatusValidator(IRestClient restClient, IPrint print)
        {
            this.restClient = restClient;
            this.Print = print;
        }

        public int GetRoadCurrentStatus(string road)
        {
            try
            {
                var roadStatus = restClient.Get(road);

                if (!string.IsNullOrEmpty(roadStatus?.Trim()))
                {
                    if (restClient.StatusCode == HttpStatusCode.OK)
                    {
                        CorridorStatus = JsonConvert.DeserializeObject<CorridorStatus[]>(roadStatus)?.First();
                        PrintStatus();

                        return 0;
                    }
                    else if (restClient.StatusCode == HttpStatusCode.NotFound)
                    {
                        HttpNotFoundError = JsonConvert.DeserializeObject<NotFoundError>(roadStatus);
                        PrintError(road);

                        return 1;
                    }
                }
            }
            catch (Exception) { /* Implement Logging if required */ }

            return -1;
        }

        private void PrintError(string road)
        {
            Print.AddError(road);
            Print.PringStatus();
        }

        private void PrintStatus()
        {
            Print.AddHeader(CorridorStatus.displayName);
            Print.AddRoadStatusText("statusSeverity", CorridorStatus.statusSeverity);
            Print.AddRoadStatusText("statusSeverityDescription", CorridorStatus.statusSeverityDescription);
            Print.PringStatus();
        }
    }
}
