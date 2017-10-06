using HandmadeHttpServer.Server.Handlers;
using System.Collections.Generic;

namespace HandmadeHttpServer.Server.Routing.Contracts
{
    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }
        RequestHandler Handler { get; }
    }
}
