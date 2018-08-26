using Autofac;
using RoadStatus.REST.HttpRestClient;
using RoadStatus.REST.Interfaces;
using System.Net.Http;

namespace RoadStatus.REST
{
    public class RoadStatusRestContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new Config())
					.As<IConfig>();

            builder.Register(c => new RestClient(c.Resolve<IConfig>(), new HttpClient()))
					.As<IRestClient>();
            builder.Register(c => new Print()).As<IPrint>();

            builder.Register(c => new RoadStatusValidator(c.Resolve<IRestClient>(), c.Resolve<IPrint>()))
					.As<IRoadStatusValidator>();
        }
    }
}
