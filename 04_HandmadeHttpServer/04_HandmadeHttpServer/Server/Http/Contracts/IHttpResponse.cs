namespace HandmadeHttpServer.Server.Http.Contracts
{
    using HandmadeHttpServer.Server.Enums;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        HttpHeaderCollection Headers { get; }
    }
}
