namespace HandmadeHttpServer.Server.Handlers
{
    using HandmadeHttpServer.Server.Http.Contracts;
    using System;

    public class GetHandler : RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
            : base(handlingFunc)
        {
        }
    }
}
