namespace HandmadeHttpServer.Server.Handlers
{
    using HandmadeHttpServer.Server.Http.Contracts;
    using System;

    public class PostHandler : RequestHandler
    {
        public PostHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
            : base(handlingFunc)
        {
        }
    }
}
