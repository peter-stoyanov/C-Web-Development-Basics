namespace HandmadeHttpServer.Server.Http.Contracts
{
    public interface IHttpContext
    {
        IHttpRequest Request { get; }
    }
}
