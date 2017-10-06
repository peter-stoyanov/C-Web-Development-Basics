namespace HandmadeHttpServer.Server
{
    using HandmadeHttpServer.Server.Routing;
    using HandmadeHttpServer.Server.Routing.Contracts;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class WebServer : IRunnable
    {
        private const string LOCALHOSTIPADDRESS = "127.0.0.1";

        private int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private TcpListener listener;
        private bool isRunning;

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.serverRouteConfig = new ServerRouteConfig(routeConfig);

            this.listener = new TcpListener(IPAddress.Parse(LOCALHOSTIPADDRESS), port);
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server running on {LOCALHOSTIPADDRESS}:{this.port}");

            Task.Run(this.ListenLoop).Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                var client = await this.listener.AcceptSocketAsync();
                var connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                await connectionHandler.ProcessRequestAsync();
            }
        }
    }
}
