using RoadStatus.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadStatus.REST
{
    public class Print : IPrint
    {
        public StringBuilder Message { get; set; } = new StringBuilder();
        public Dictionary<string, string> ReportConstants { get; } = new Dictionary<string, string>()
        {
            { "statusSeverityDescription", "Road Status Description" },
            { "statusSeverity", "Road Status" }
        };

        public IPrint AddHeader(string text)
        {
            Message.AppendLine($"The status of the {text} is as follows");

            return this;
        }

        public IPrint AddRoadStatusText(string key, string text)
        {
            Message.AppendLine($"\t{ReportConstants[key]} is {text}");

            return this;
        }

        public IPrint AddError(string test)
        {
            Message.AppendLine($"{test} is not a valid road");

            return this;
        }

        public IPrint AddHelp()
        {
            Message.Clear();

            Message.AppendLine("Please enter:");
            Message.AppendLine("\tRoadStatus <RoadName>");

            return this;
        }

        public void PringStatus()
        {
            Console.WriteLine(Message.ToString());
        }
    }
}
