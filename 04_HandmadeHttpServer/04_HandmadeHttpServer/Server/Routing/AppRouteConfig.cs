using HandmadeHttpServer.Server.Enums;
using HandmadeHttpServer.Server.Handlers;
using HandmadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HandmadeHttpServer.Server.Routing
{
    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes => this.routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();

            var availableMethods = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();
            }
        }

        public void AddRoute(string route, RequestHandler handler)
        {
            string handlerName = handler.GetType().Name.ToLower();

            if (handlerName.Contains("get"))
            {
                this.routes[HttpRequestMethod.Get].Add(route, handler);
            }
            else if (handlerName.Contains("post"))
            {
                this.routes[HttpRequestMethod.Post].Add(route, handler);
            }
            else
            {
                throw new InvalidOperationException("Invalid handler.");
            }
        }
    }
}
