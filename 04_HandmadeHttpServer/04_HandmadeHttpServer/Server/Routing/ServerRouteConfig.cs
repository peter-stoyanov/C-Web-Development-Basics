using HandmadeHttpServer.Server.Enums;
using HandmadeHttpServer.Server.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HandmadeHttpServer.Server.Routing
{
    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var availableMethods = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeRouteConfig(appConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes => this.routes;

        private void InitializeRouteConfig(IAppRouteConfig appConfig)
        {
            foreach (var registeredRoute in appConfig.Routes)
            {
                var routesWithHandlers = registeredRoute.Value;
                var requestMethod = registeredRoute.Key;

                foreach (var routeWithHandler in routesWithHandlers)
                {
                    var route = routeWithHandler.Key;
                    var handler = routeWithHandler.Value;

                    var parameters = new List<string>();
                    var parsedRouteRegex = this.ParseRoute(route, parameters);
                    var routingContext = new RoutingContext(handler, parameters);
                    this.routes[requestMethod].Add(parsedRouteRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var result = new StringBuilder();
            result.Append("^");

            if (route == "/")
            {
                result.Append("/$");
                return result.ToString();
            }

            var tokens = route.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            this.ParseTokens(tokens, parameters, result);

            return result.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder result)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = tokens.Length - 1 == i ? "?" : "/";
                var currentToken = tokens[i];
                if (!currentToken.StartsWith("{") && !currentToken.EndsWith("}"))
                {
                    result.Append($"{currentToken}{end}");
                    continue;
                }

                var parameterRegex = new Regex("<\\w+>");
                var parameterMatch = parameterRegex.Match(currentToken);
                if (!parameterMatch.Success)
                {
                    throw new InvalidOperationException($"Route parameter in '{currentToken}' was not valid.");
                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);

                parameters.Add(parameter);

                var currentTokenWithoutCurlyBraces = currentToken.Substring(1, currentToken.Length - 2);

                result.Append($"{currentTokenWithoutCurlyBraces}{end}");
            }
        }
    }
}
