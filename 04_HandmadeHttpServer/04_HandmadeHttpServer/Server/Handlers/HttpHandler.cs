namespace HandmadeHttpServer.Server.Handlers
{
    using HandmadeHttpServer.Server.Common;
    using HandmadeHttpServer.Server.Handlers.Contracts;
    using HandmadeHttpServer.Server.Http.Contracts;
    using HandmadeHttpServer.Server.Http.Response;
    using HandmadeHttpServer.Server.Routing.Contracts;
    using System.Text.RegularExpressions;

    public class HttpHandler : IRequestHandler
    {
        private IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            foreach (var kvp in this.serverRouteConfig.Routes[context.Request.Method])
            {
                string pattern = kvp.Key;
                var regex = new Regex(pattern);
                var match = regex.Match(context.Request.Path);

                if (!match.Success)
                {
                    continue;
                }

                foreach (string parameter in kvp.Value.Parameters)
                {
                    context.Request.AddUrlParameter(parameter, match.Groups[parameter].Value);
                }

                return kvp.Value.Handler.Handle(context);
            }

            return new NotFoundResponse();
        }
    }
}
