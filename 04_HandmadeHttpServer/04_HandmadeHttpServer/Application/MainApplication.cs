namespace HandmadeHttpServer.Application
{
    using HandmadeHttpServer.Application.Controllers;
    using HandmadeHttpServer.Server.Contracts;
    using HandmadeHttpServer.Server.Handlers;
    using HandmadeHttpServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/", new GetHandler(request => new HomeController().Index()));
        }
    }
}
