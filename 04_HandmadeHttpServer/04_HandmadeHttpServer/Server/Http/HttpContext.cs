namespace HandmadeHttpServer.Server.Http
{
    using Contracts;
    using HandmadeHttpServer.Server.Common;

    public class HttpContext : IHttpContext
    {
        private IHttpRequest request;

        public HttpContext(IHttpRequest request)
        {
            CoreValidator.ThrowIfNull(request, nameof(request));

            this.request = request;
        }

        public IHttpRequest Reuest => this.request;
    }
}
