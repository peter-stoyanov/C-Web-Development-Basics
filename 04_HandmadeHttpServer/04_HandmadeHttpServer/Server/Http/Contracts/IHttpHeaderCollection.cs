namespace HandmadeHttpServer.Server.Http.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader header);

        HttpHeader Get(string key);

        bool ContainsKey(string key);
    }
}
