using Autofac;
using RoadStatus.REST.Interfaces;
using System.Linq;

namespace RoadStatus
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = RoadStatusContainer.Container.Resolve<IRoadStatusValidator>();

            string parameter;

            if ((parameter = ParseArgs(args)) != null)
            {
                return app.GetRoadCurrentStatus(parameter);
            }

            return -1;
        }

        private static string ParseArgs(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                return args.First();
            }

            PrintHelp();

            return null;
        }

        private static void PrintHelp()
        {
            var print = RoadStatusContainer.Container.Resolve<IPrint>();

            print.AddHelp()
                   .PringStatus();
        }
    }
}
