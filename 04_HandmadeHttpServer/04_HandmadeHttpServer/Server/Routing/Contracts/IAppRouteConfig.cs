using HandmadeHttpServer.Server.Enums;
using HandmadeHttpServer.Server.Handlers;
using System.Collections.Generic;

namespace HandmadeHttpServer.Server.Routing.Contracts
{
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes
        {
            get;
        }

        void AddRoute(string route, RequestHandler handler);
    }
}
