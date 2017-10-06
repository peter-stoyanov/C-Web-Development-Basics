namespace HandmadeHttpServer.Server.Handlers
{
    using HandmadeHttpServer.Server.Common;
    using HandmadeHttpServer.Server.Handlers.Contracts;
    using HandmadeHttpServer.Server.Http.Contracts;
    using System;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlerFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));

            this.handlerFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            var response = this.handlerFunc(context.Request);

            response.Headers.Add(new Http.HttpHeader("Content-Type", "text-plain"));

            return response;
        }
    }
}
