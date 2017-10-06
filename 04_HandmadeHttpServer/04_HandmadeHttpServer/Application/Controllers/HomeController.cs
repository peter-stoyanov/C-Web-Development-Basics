namespace HandmadeHttpServer.Application.Controllers
{
    using HandmadeHttpServer.Application.Views.Home;
    using HandmadeHttpServer.Server.Enums;
    using HandmadeHttpServer.Server.Http.Contracts;
    using HandmadeHttpServer.Server.Http.Response;

    public class HomeController
    {
        // Get /
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new HomeIndexView());
        }
    }
}
