using HandmadeHttpServer.Server.Common;
using HandmadeHttpServer.Server.Handlers;
using HandmadeHttpServer.Server.Routing.Contracts;
using System.Collections.Generic;

namespace HandmadeHttpServer.Server.Routing
{
    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler handler, IEnumerable<string> parameters)
        {
            CoreValidator.ThrowIfNull(handler, nameof(handler));
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));

            this.Parameters = parameters;
            this.Handler = handler;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler Handler { get; private set; }
    }
}
