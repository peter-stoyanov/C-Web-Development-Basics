namespace HandmadeHttpServer.Server.Http.Contracts
{
    public interface IHttpContext
    {
        IHttpRequest Reuest { get; }
    }
}
