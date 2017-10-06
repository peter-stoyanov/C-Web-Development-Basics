using HandmadeHttpServer.Application;
using HandmadeHttpServer.Server;
using HandmadeHttpServer.Server.Routing;

namespace HandmadeHttpServer
{
    public class Launcher : IRunnable
    {
        private static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var appRouteConfig = new AppRouteConfig();

            var mainApplication = new MainApplication();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(1337, appRouteConfig);
            webServer.Run();
        }
    }
}
