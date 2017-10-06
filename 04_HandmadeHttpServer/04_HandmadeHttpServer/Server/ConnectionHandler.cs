namespace HandmadeHttpServer.Server
{
    using HandmadeHttpServer.Server.Common;
    using HandmadeHttpServer.Server.Handlers;
    using HandmadeHttpServer.Server.Http;
    using HandmadeHttpServer.Server.Http.Contracts;
    using HandmadeHttpServer.Server.Routing.Contracts;
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.serverRouteConfig = serverRouteConfig;
            this.client = client;
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            var httpContext = new HttpContext(httpRequest);

            var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

            var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

            var byteSegments = new ArraySegment<byte>(responseBytes);

            await this.client.SendAsync(byteSegments, SocketFlags.None);

            Console.WriteLine("-----REQUEST-----");
            Console.WriteLine(httpRequest);
            Console.WriteLine("-----RESPONSE-----");
            Console.WriteLine(httpResponse);
            Console.WriteLine();

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();

            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None);

                if (numBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numBytesRead);

                result.Append(bytesAsString);

                if (numBytesRead < 1024)
                {
                    break;
                }
            }

            return new HttpRequest(result.ToString());
        }
    }
}
