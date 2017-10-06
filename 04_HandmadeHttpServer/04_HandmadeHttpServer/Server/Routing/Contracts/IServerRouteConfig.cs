using HandmadeHttpServer.Server.Enums;
using System.Collections.Generic;

namespace HandmadeHttpServer.Server.Routing.Contracts
{
    public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes
        {
            get;
        }
    }
}
