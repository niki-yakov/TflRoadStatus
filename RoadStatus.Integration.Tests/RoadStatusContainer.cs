using Autofac;
using RoadStatus.REST;

namespace RoadStatus
{
    public static class RoadStatusContainer
    {
        public static Autofac.IContainer Container { get; set; }

        static RoadStatusContainer()
        {
            Container = Build();
        }

        private static Autofac.IContainer Build()
        {
            var container = new ContainerBuilder();

            container.RegisterModule(new RoadStatusRestContainer());

            return container.Build();
        }
    }
}
